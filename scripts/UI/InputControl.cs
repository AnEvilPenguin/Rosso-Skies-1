using Godot;
using RossoSkies1.scripts.Settings;
using RossoSkies1.scripts.UI;
public partial class InputControl : HBoxContainer
{
    public Label Label = new Label();
    public KeyDetectionButton KeyButton = new KeyDetectionButton();
    public ControllerDetectionButton ControllerButton = new ControllerDetectionButton();
    public InputEventAction Action;

    private InputEvent _keyboardInput;
    private InputEvent _controllerInput;

    public override void _Ready()
    {
        base._Ready();

        Label.Text = Name;

        AddChild(Label);
        
        AddChild(ControllerButton);
        AddChild(KeyButton);
    }

    public InputControl SetKeyboard(InputBinding binding)
    {
        _keyboardInput = binding.ToInputEvent();
        KeyButton.Text = _keyboardInput.AsText();

        return this;
    }

    public InputControl SetContoller(InputBinding binding)
    {
        _controllerInput = binding.ToInputEvent(); ;
        ControllerButton.Text = _controllerInput.AsText();

        return this;
    }
}
