using Godot;
using Networking;
using Riptide;

public partial class StoneGolem : Enemy {
    [Export] public PackedScene ProjectileScene;
    [Export] public Node2D ProjectileOrigin;

    public override void _Ready() {
        base._Ready();

        NetworkPoint.Register(nameof(SetRandomSeedRpc), SetRandomSeedRpc);
    }

    public override void AddStates() {
        base.AddStates();

        _stateMachine.Add(new Idle("idle", this));
        _stateMachine.Add(new DashAttack("attack", this) {
            OnDash = (direction, target) => {
                SquashAndStretch.Trigger(new Vector2(0.2f, 1.4f), 6f);

                float faceDirection = (target.X > GlobalPosition.X ? 1f : -1f) * (target.Y > GlobalPosition.Y ? 1f : -1f);
                Face(faceDirection >= 0);

                Projectile projectile = ProjectileScene.Instantiate<Projectile>();

                projectile.Source = this;

                AddChild(projectile);

                projectile.GlobalPosition = ProjectileOrigin.GlobalPosition;
                projectile.Position += direction * 5f;

                projectile.LookAt(projectile.GlobalPosition + direction);

                return projectile;
            },
            OnStop = () => {
                SquashAndStretch.Trigger(new Vector2(1.6f, 0.6f), 6f);

                Camera.Shake(0.2f);
            }
        });

        if (NetworkManager.IsHost) NetworkPoint.SendRpcToClients(nameof(SetRandomSeedRpc), message => message.AddULong(_stateMachine.GetState<DashAttack>("attack").Random.Seed));
    }

    public override void SyncPosition(float delta) {
        if (NetworkPoint.IsOwner) {
            _networkedPosition.Value = GlobalPosition;
        } else if (_stateMachine.CurrentState != "attack" && _networkedPosition.Synced) {
            if (_networkedPosition.Value.DistanceSquaredTo(GlobalPosition) > 64) GlobalPosition = _networkedPosition.Value;

            GlobalPosition = GlobalPosition.Lerp(_networkedPosition.Value, delta * 20.0f);
        }
    }

    private void SetRandomSeedRpc(Message message) {
        _stateMachine.GetState<DashAttack>("attack").Random.Seed = message.GetULong();
    }
}
