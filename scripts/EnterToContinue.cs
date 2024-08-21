using Godot;

public partial class EnterToContinue : Node2D
{
	[Signal]
	public delegate void ButtonHoldCompleteEventHandler();

	private AnimatedSprite2D _sprite;

	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

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
