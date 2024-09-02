using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

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

	public Health Health;

	private float _speed;
	private Camera2D _camera;

	private List<BasicGun> _guns;

    public override void _Ready()
    {
        _camera = GetNode<Camera2D>("%Camera2D");

		_guns = GetChildren()
			.OfType<BasicGun>()
			.ToList();

		Health = GetNode<Health>("Health");
    }

    public override void _PhysicsProcess(double delta)
	{
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDirection = Input.GetVector("Turn Left", "Turn Right", "Decelerate", "Accelerate");

		if (Input.IsActionPressed("Turn Left") || Input.IsActionPressed("Turn Right"))
            Turn(inputDirection.X);

        var direction = new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));

        if (Input.IsActionPressed("Accelerate"))
            IncreaseSpeed();
		else if (Input.IsActionPressed("Decelerate"))
			DecreaseSpeed();

		if (Input.IsActionPressed("Shoot Primary"))
			_guns.ForEach(gun => gun.Shoot(direction, Rotation, _speed));

		Zoom();

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

	private void Zoom()
	{
        var diff = ((MaxSpeed - _speed) / MaxSpeed) + 1.5f;
        _camera.Zoom = new Vector2(diff, diff);
    }

	private void OnDestroyed()
	{
		GD.Print("Game Over");
	}
}
