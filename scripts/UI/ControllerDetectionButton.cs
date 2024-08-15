using Godot;

namespace RossoSkies1.scripts.UI
{
    public partial class ControllerDetectionButton : InputDetectionButton
    {
        public override void UpdateText(InputEvent @event)
        {
            if (@event is InputEventJoypadButton joypadButton)
            {
                Text = joypadButton.AsText();
                EmitSignal(SignalName.BindingChanged, @event);
                AcceptEvent();
            }
            else if (@event is InputEventJoypadMotion joypadMotion)
            {
                Text = joypadMotion.AsText();
                EmitSignal(SignalName.BindingChanged, @event);
                AcceptEvent();
            }
        }
    }
}
