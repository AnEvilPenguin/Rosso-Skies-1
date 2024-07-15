using Godot;
using System;

public partial class EnterToContinue : Node2D
{
	[Signal]
	public delegate void ButtonHoldCompleteEventHandler();

	private AnimatedSprite2D _sprite;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("Continue"))
		{
			if (_sprite.Animation == "default")
				_sprite.Play("hold");

			return;
		}

		_sprite?.Play("default");
	}

	public void OnAnimationFinished()
	{	
		if (_sprite.Animation == "hold")
		{
			EmitSignal(SignalName.ButtonHoldComplete);
		}
	}
}
