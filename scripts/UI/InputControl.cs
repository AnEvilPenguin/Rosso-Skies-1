using Godot;
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

    public InputControl SetKeyboard(InputEvent @event)
    {
        _keyboardInput = @event;
        KeyButton.Text = @event.AsText();

        return this;
    }

    public InputControl SetContoller(InputEvent @event)
    {
        _controllerInput = @event;
        ControllerButton.Text = @event.AsText();

        return this;
    }
}
