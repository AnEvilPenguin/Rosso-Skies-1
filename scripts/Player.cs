using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float MinSpeed = 20f;
	[Export]
	public float MaxSpeed = 80f;
	[Export]
	public float Acceleration = 1f;

	private float _speed;

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
            IncreaseSpeed();
		else if (direction == Vector2.Down)
			DecreaseSpeed();

		Velocity = destination * _speed;

		MoveAndSlide();
	}

	/// <summary>
	/// Increase speed up to MaxSpeed.
	/// Allows for speed less than MinSpeed (e.g. TakeOff)
	/// </summary>
	private void IncreaseSpeed()
	{
		_speed += Acceleration;

		if (_speed > MaxSpeed)
		{
			_speed = MaxSpeed;
		}
	}

	/// <summary>
	/// Decrease speed down to MinSpeed.
	/// Allows for speed less than 
	/// </summary>
	private void DecreaseSpeed()
	{
		_speed -= Acceleration;

		if (_speed < MinSpeed)
		{
			_speed = MinSpeed;
		}
	}
}
