using System;
using Godot;

public class JumpAttack : EnemyState {
    public float Speed = 30f;
    public float Duration = 0.75f;
    public float Height = 16f;
    public string ReturnState = "idle";

    public Action<Vector2> OnJump;
    public Action<Vector2> OnLand;

    private Vector2 _target;
    private float _jumpTimer;

    public JumpAttack(string name, Enemy enemy) : base(name, enemy) { }

    public override void Enter() {
        if (Player.AlivePlayers.Count == 0) {
            GoToState(ReturnState);

            return;
        }

        _jumpTimer = 0f;

        _target = _enemy.GetWeightedTargets()[0].Player.GlobalPosition;

        _enemy.Face(_target);

        OnJump?.Invoke((_target - _enemy.GlobalPosition).Normalized());
    }

    public override void Update(float delta) {
        if (!_enemy.Hurt) _enemy.AnimationPlayer.Play("jump");

        _jumpTimer += delta;

        float height = Mathf.Pow(Mathf.Sin(_jumpTimer / Duration * Mathf.Pi), 0.75f) * Height;

        _enemy.VerticalTransform.Position = Vector2.Up * height;

        if (_jumpTimer < Duration) return;

        OnLand((_target - _enemy.GlobalPosition).Normalized());

        GoToState(ReturnState);
    }

    public override void PhsysicsUpdate(float delta) {
        _enemy.Velocity = (_target - _enemy.GlobalPosition).Normalized() * Speed + _enemy.Knockback;

        _enemy.MoveAndSlide();
    }

    public override void Exit() {
        _enemy.VerticalTransform.Position = Vector2.Zero;
    }
}
