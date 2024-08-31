using Godot;
using System;

public partial class PhysicsEnemy : CharacterBody2D
{
    [Export]
    public float Speed = 25.0f;

    public Health Health;

    private Player _player;

    public override void _Ready()
    {
        Health = GetNode<Health>("Health");

        _player = GetParent().GetNode<Player>("%Player");

        Health.Destroyed += OnDestroyed;
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
