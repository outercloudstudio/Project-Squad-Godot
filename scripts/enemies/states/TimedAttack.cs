using System;
using System.Collections.Generic;
using Godot;

public class TimedAttack : EnemyState {
    public float Duration = 1f;
    public string ReturnState = "idle";

    public Action<List<float>> OnPrepare;
    public Action<Vector2> OnShoot;

    private float _timer;
    private List<float> _shootQueue = new List<float>();
    private Vector2 _direction;

    public TimedAttack(string name, Enemy enemy) : base(name, enemy) { }

    public override void Enter() {
        _enemy.AnimationPlayer.Play("telegraph_attack");

        _timer = Duration;

        Vector2 target = _enemy.GetWeightedTargets()[0].Player.GlobalPosition;

        _direction = (target - _enemy.GlobalPosition).Normalized();

        _enemy.Face(target);

        _shootQueue.Clear();

        OnPrepare(_shootQueue);
    }

    public override void PhsysicsUpdate(float delta) {
        _enemy.Velocity = _enemy.Knockback * (1f - _enemy.KnockbackResistance);

        _enemy.MoveAndSlide();
    }

    public override void Update(float delta) {
        for (int index = 0; index < _shootQueue.Count; index++) {
            _shootQueue[index] -= (float)delta;

            if (_shootQueue[index] > 0) continue;

            OnShoot(_direction);

            _shootQueue.RemoveAt(index);

            index--;
        }

        _timer -= delta;

        if (_timer <= 0f) GoToState(ReturnState);
    }
}
