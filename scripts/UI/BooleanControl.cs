using Godot;
using System;

public partial class BooleanControl : HBoxContainer
{
	public Label Label;
	public CheckButton Slider;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();

		Label = new Label();
		AddChild(Label);

		Slider = new CheckButton();
		AddChild(Slider);
	}
}
