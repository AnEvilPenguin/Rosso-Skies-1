using Godot;
using RossoSkies1.scripts.Settings;
using RossoSkies1.scripts.UI;

public partial class InputControl
{
    public string Name;
    public Label Label = new Label();
    public KeyDetectionButton KeyButton = new KeyDetectionButton();
    public ControllerDetectionButton ControllerButton = new ControllerDetectionButton();
    public InputEventAction Action;

    private InputEvent _keyboardInput;
    private InputEvent _controllerInput;

    public void ConfigureGrid(GridContainer container)
    {
        Label.Text = Name;

        container.AddChild(Label);

        container.AddChild(KeyButton);
        container.AddChild(ControllerButton);
    }


    public InputControl SetKeyboard(InputBinding binding)
    {
        _keyboardInput = binding.ToInputEvent();
        KeyButton.Text = _keyboardInput.AsText();

        return this;
    }

    public InputControl SetContoller(InputBinding binding)
    {
        _controllerInput = binding.ToInputEvent();

        ControllerButton.Text = binding.Type == InputType.JoypadAxis ?
            BuildAxisText(binding) : 
            StripJoypadText(_controllerInput.AsText());

       

        return this;
    }

    private string StripJoypadText(string input)
    {
        if (!input.Contains('('))
            return input;

        var startIndex = input.IndexOf('(') + 1;
        var endIndex = input.IndexOf(')');

        var length = endIndex - startIndex;

        return input.Substring(startIndex, length);
    }

    private string BuildAxisText(InputBinding binding)
    {
        return binding.JoypadAxis switch
        {
            JoyAxis.LeftX => $"Left Stick: {(binding.AxisValue > 0 ? "Right" : "Left")}",
            JoyAxis.LeftY => $"Left Stick: {(binding.AxisValue > 0 ? "Up" : "Down")}",
            JoyAxis.RightX => $"Right Stick: {(binding.AxisValue > 0 ? "Right" : "Left")}",
            JoyAxis.RightY => $"Right Stick: {(binding.AxisValue > 0 ? "Up" : "Down")}",
            _ => StripJoypadText(binding.ToInputEvent().AsText()),
        };
    }
}
