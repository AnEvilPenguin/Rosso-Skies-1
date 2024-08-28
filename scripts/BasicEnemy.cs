using Godot;
using System;

public partial class BasicEnemy : Node2D
{
    [Export]
    public float Speed = 25;

    public Health Health;

	private Player _player;

	public override void _Ready()
	{
		Health = GetNode<Health>("Health");

		_player = GetParent().GetNode<Player>("%Player");

		Health.Destroyed += OnDestroyed;
	}

    public override void _Process(double delta)
    {
        ProcessRotation();
        ProcessPosition(delta);
    }

    private void OnDestroyed() =>
	    QueueFree();

    private void ProcessPosition(double delta)
    {
        var direction = new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));

        Position += direction * (Speed * (float)delta);
    }

    private void ProcessRotation()
    {
        var direction = GlobalPosition.DirectionTo(_player.GlobalPosition);

        // TODO figure out how to set enemy agility
        var rotation = direction.Angle();

        // TODO shoot when within a certain angle/distance of player

        Rotation = rotation;
    }
}
