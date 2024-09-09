using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Player : CharacterBody2D
{
	// TODO layer to own component
	[Signal]
	public delegate void LayerChangedEventHandler(int layer);

	[Export]
	public float MinSpeed = 20f;
	[Export]
	public float MaxSpeed = 80f;
	[Export]
	public float Acceleration = 1f;
	[Export]
	public int Maneuverability = 50;

	public Health Health;

    public Layer Layer;

	private float _speed;
	private CameraZoom _zoom;

	private List<BasicGun> _guns;

	private Area2D _hitBox;

	private AnimatedSprite2D _sprite;

    public override void _Ready()
	{		
		_zoom = GetNode<CameraZoom>("CameraZoom");

		_guns = GetChildren()
			.OfType<BasicGun>()
			.ToList();

		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_sprite.Play("idle");

		Health = GetNode<Health>("Health");
		Layer = GetNode<Layer>("Layer");
		
		_hitBox = GetNode<Area2D>("HitBox");

        Layer.LayerChanged += (int layer, int previousLayer) => {
			var collision = getLayer(layer);
			var mask = getMask(layer);

            _guns.ForEach(gun => {
				gun.Layer = new int[] { collision };
				gun.Mask = new int[] { mask };
            });

			// TODO make this not terrible
			// Probably put it into te layer or something

			_zoom.SetLayer(layer, previousLayer);

			SetScale(layer, previousLayer);

			ZIndex += layer - previousLayer; 

			SetCollisionLayerValue(getLayer(previousLayer), false);
            SetCollisionLayerValue(collision, true);

			_hitBox.SetCollisionLayerValue(getLayer(previousLayer), false);
			_hitBox.SetCollisionLayerValue(collision, true);

            SetCollisionMaskValue(getMask(previousLayer), false);
			SetCollisionMaskValue(mask, true);

            _hitBox.SetCollisionMaskValue(getMask(previousLayer), false);
            _hitBox.SetCollisionMaskValue(mask, true);
        };
	}

	private int getLayer(int layer) =>
		2 * layer + 1;

	private int getMask(int layer) =>
		2 * layer + 2;

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

		if (Input.IsActionJustPressed("Fly Up"))
			Layer.RaiseLayer();
		else if (Input.IsActionJustPressed("Fly Down"))
			Layer.LowerLayer();

        // TODO consider some sort of takeoff automation.

        var speedPercent = _speed / MaxSpeed * 100f;
		_zoom.ZoomFromSpeedPercent(speedPercent);

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

	private void OnDestroyed()
	{
		GD.Print("Game Over");
	}

	private void SetScale(int layer, int previous)
	{
        var scale = Scale;
        var diff = layer - previous;
        var newScale = scale.X + (0.5f * diff);
        Scale = new Vector2(newScale, newScale);
    }
}
