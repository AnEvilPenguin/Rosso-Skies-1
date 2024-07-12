using Godot;
using System;

public partial class Player : CharacterBody2D
{
	private float _speed;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (direction == Vector2.Left || direction == Vector2.Right)
		{
			RotationDegrees += direction.X;
		}

        Vector2 destination = new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));

        if (direction == Vector2.Up)
            _speed = _speed + 1;
		else if (direction == Vector2.Down)
			_speed = _speed - 1;

		Velocity = destination * _speed;

		MoveAndSlide();
	}
}
