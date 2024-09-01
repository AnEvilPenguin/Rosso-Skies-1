using Godot;
using System;

public partial class PhysicsEnemy : CharacterBody2D
{
    [Export]
    public float Speed = 25.0f;

    public Health Health;

    private Player _player;

    private RayCast2D _playerDetection;
    private BasicGun _gun;

    public override void _Ready()
    {
        Health = GetNode<Health>("Health");
        _playerDetection = GetNode<RayCast2D>("%PlayerDetectionRayCast");
        _gun = GetNode<BasicGun>("%BasicGun");
        _gun.Layer = new int[] { 4 };
        _gun.Mask = new int[] { 1 };

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
    }

    public override void _PhysicsProcess(double delta)
	{
        ProcessRotation();

        var direction = new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));

        Velocity = direction * Speed;

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
}
