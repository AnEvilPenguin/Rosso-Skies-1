using Godot;

public partial class MenuButton : MarginContainer
{
	[Signal]
	public delegate void OnClickEventHandler();

	[Export]
	public string ButtonText;

	private Button _button;

	public override void _Ready()
	{
		_button = GetNode<Button>("%Button");

		_button.Text = ButtonText;

		_button.ButtonDown += () => EmitSignal(SignalName.OnClick);
	}
}
