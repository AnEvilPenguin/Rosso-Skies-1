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
	[Export]
	public int Maneuverability = 50;

	private float _speed;

	public override void _PhysicsProcess(double delta)
	{
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (inputDirection == Vector2.Left || inputDirection == Vector2.Right)
			Turn(inputDirection.X);

        Vector2 direction = new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));

        if (inputDirection == Vector2.Up)
            IncreaseSpeed();
		else if (inputDirection == Vector2.Down)
			DecreaseSpeed();

		Velocity = direction * _speed;

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
	/// </summary>
	private void DecreaseSpeed()
	{
		_speed -= Acceleration;

		if (_speed < MinSpeed)
		{
			_speed = MinSpeed;
		}
	}

	/// <summary>
	/// Turns the player when above minimum speed.
	/// </summary>
	/// <param name="x">The direction to turn in.</param>
	private void Turn(float x)
	{
		if (_speed < MinSpeed)
			return;

        var rotationFactor = Maneuverability / _speed;
		RotationDegrees += x * rotationFactor;
    }
}
