using Godot;
using System;

public partial class MenuButton : MarginContainer
{
	[Signal]
	public delegate void OnClickEventHandler();

	[Export]
	public string ButtonText;

	private Button _button;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_button = GetNode<Button>("%Button");

		_button.Text = ButtonText;

		_button.ButtonDown += () => EmitSignal(SignalName.OnClick);
	}
}
