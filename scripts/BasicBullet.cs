using Godot;
using System;
using static Godot.TextServer;

public partial class BasicBullet : Node2D
{
	public RigidBody2D RigidBody2D;

	public Vector2 Direction;

	[Export]
	public float Speed = 300;
    [Export]
    public float Lifetime = 1;

	private double timer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		RigidBody2D = GetNode<RigidBody2D>("RigidBody2D");
		timer = Lifetime;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += Direction * (float)(Speed * delta);

		if (timer < 0)
			QueueFree();

		timer -= delta;
	}
}
