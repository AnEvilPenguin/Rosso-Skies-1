using Godot;

namespace RossoSkies1.scripts.UI
{
    public partial class InputDetectionButton : Button
    {
        [Signal]
        public delegate void BindingChangedEventHandler(InputEvent @event);

        protected bool _waitingForInput = false;

        public override void _Ready()
        {
            Pressed += OnClick;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (!_waitingForInput)
                return;

            UpdateText(@event);

            _waitingForInput = false;
        }

        public void OnClick()
        {
            AcceptEvent();
            _waitingForInput = true;
        }

        public virtual void UpdateText(InputEvent @event)
        {
            Text = @event.AsText();
            EmitSignal(SignalName.BindingChanged, @event);
        }
            
    }
}
