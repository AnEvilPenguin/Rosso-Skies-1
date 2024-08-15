using Godot;

namespace RossoSkies1.scripts.UI
{
    public partial class KeyDetectionButton : InputDetectionButton
    {
        public override void UpdateText(InputEvent @event)
        {
            if (@event is InputEventKey eventKey)
            {
                Text = eventKey.AsText();
                EmitSignal(SignalName.BindingChanged, @event);
                AcceptEvent();
            }  
            else if (@event is InputEventMouseButton eventMouseButton)
            {
                Text = eventMouseButton.AsText();
                EmitSignal(SignalName.BindingChanged, @event);
                AcceptEvent();
            }       
        }
    }
}
