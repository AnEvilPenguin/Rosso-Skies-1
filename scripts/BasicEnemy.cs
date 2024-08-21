using Godot;
using System;

public partial class BasicEnemy : Node2D
{
	public Health Health;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Health = GetNode<Health>("Health");

		Health.Destroyed += OnDestroyed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnDestroyed() =>
		QueueFree();
}
