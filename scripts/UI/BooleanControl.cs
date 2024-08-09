using Godot;

public partial class BooleanControl : HBoxContainer
{
	public Label Label;
	public CheckButton Slider;

	public override void _Ready()
	{
		base._Ready();

		Label = new Label();
		AddChild(Label);

		Slider = new CheckButton();
		AddChild(Slider);
	}
}
