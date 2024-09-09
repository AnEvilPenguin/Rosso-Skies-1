using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PhysicsEnemy : CharacterBody2D
{
    [Export]
    public float Speed = 25.0f;

    public float CurrentSpeed;

    public Health Health;

    private Player _player;

    private RayCast2D _playerDetection;
    private IEnumerable<RayCast2D> _enemyDetection; // enemy being a player enemy
    private BasicGun _gun;

    private AnimatedSprite2D _sprite;

    private HashSet<PhysicsEnemy> _slow = new();

    public override void _Ready()
    {
        Health = GetNode<Health>("Health");

        _playerDetection = GetNode<RayCast2D>("%PlayerDetectionRayCast");

        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _sprite.Play("idle");

        var detectionNode = GetNode<Node>("%ForwardEnemyDetection");
        _enemyDetection = detectionNode.GetChildren().OfType<RayCast2D>();

        _gun = GetNode<BasicGun>("%BasicGun");
        _gun.Layer = new int[] { 2 };
        _gun.Mask = new int[] { 3 };

        CurrentSpeed = Speed;

        _player = GetParent().GetNode<Player>("%Player");

        Health.Destroyed += OnDestroyed;
    }

    public override void _Process(double delta)
    {
        if (_playerDetection.GetCollider() is Player)
        {
            // FIXME bullet layer
            _gun.Shoot(GlobalPosition.DirectionTo(_player.GlobalPosition), Rotation, Speed);
        }

        _slow.Clear();

        foreach (var ray in _enemyDetection)
        {
            if (ray.GetCollider() != null)
            {
                var collided = ray.GetCollider() as Node2D;
                if (collided.GetParent() is PhysicsEnemy enemy)
                {
                    _slow.Add(enemy);
                }
            }
        }

        SetCurrentSpeed();
    }

    public override void _PhysicsProcess(double delta)
	{
        ProcessRotation();

        var direction = new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));

        Velocity = direction * CurrentSpeed;

        MoveAndSlide();
	}

    private void OnDestroyed() =>
        QueueFree();

    private void ProcessRotation()
    {
        var direction = GlobalPosition.DirectionTo(_player.GlobalPosition);

        // TODO figure out how to set enemy agility
        var rotation = direction.Angle();

        // TODO shoot when within a certain angle/distance of player

        Rotation = rotation;
    }

    private void SetCurrentSpeed()
    {
        CurrentSpeed = Speed;

        if (_slow.Count == 0)
            return;

        var bottom = _slow.Aggregate(CurrentSpeed, (acc, enemy) => {
            return Mathf.Min(acc, enemy.CurrentSpeed);
        });

        CurrentSpeed = (float)Mathf.Max(0.2, bottom * 0.8f);
    }
}
