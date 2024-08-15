using Godot;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RossoSkies1.scripts.Settings
{
    internal class Controls : Options
    {
        private static Controls _default = new Controls();

        public ControlGroup Accelerate = new ControlGroup()
        {
            Name = "Accelerate",
            KeyboardControl = new InputBinding { Type = InputType.Keyboard, Keyboard = Key.W },
            ControllerControl = new InputBinding { Type = InputType.JoypadAxis, JoypadAxis = JoyAxis.LeftY, AxisValue = 1.0f }
        };

        public ControlGroup Shoot = new ControlGroup()
        {
            Name = "Shoot Primary",
            KeyboardControl = new InputBinding { Type = InputType.Keyboard, Keyboard = Key.Space },
            ControllerControl = new InputBinding { Type = InputType.JoypadButton, JoypadButton = JoyButton.A }
        };

        public Controls()
        {
            Name = "Controls";
            _defaultSettings = _default;
            Columns = 3;
        }

        public override JObject ToJObject()
        {
            var output = new JObject();

            if (!_default.Accelerate.Equals(Accelerate))
                output.Add("Accelerate", Accelerate.GetDifferences(_default.Accelerate));

            if (!_default.Shoot.Equals(Shoot))
                output.Add("Shoot", Shoot.GetDifferences(_default.Shoot));

            return output;
        }

        public List<ControlGroup> GetControls() =>
            new List<ControlGroup> {
                Accelerate,
                Shoot,
            };

        public override List<Label> GetHeaders() => new List<Label> {
            new Label { Text = "Action", Name = "Action Header" },
            new Label { Text = "Keyboard", Name = "Keyboard Header" },
            new Label { Text = "Controller", Name = "Controller Header" }
        };
    }
}
