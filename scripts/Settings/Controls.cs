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

        public ControlGroup Decelerate = new ControlGroup()
        {
            Name = "Decelerate",
            KeyboardControl = new InputBinding { Type = InputType.Keyboard, Keyboard = Key.S },
            ControllerControl = new InputBinding { Type = InputType.JoypadAxis, JoypadAxis = JoyAxis.LeftY, AxisValue = -1.0f }
        };

        public ControlGroup Right = new ControlGroup()
        {
            Name = "Turn Right",
            KeyboardControl = new InputBinding { Type = InputType.Keyboard, Keyboard = Key.D },
            ControllerControl = new InputBinding { Type = InputType.JoypadAxis, JoypadAxis = JoyAxis.LeftX, AxisValue = 1.0f }
        };

        public ControlGroup Left = new ControlGroup()
        {
            Name = "Turn Left",
            KeyboardControl = new InputBinding { Type = InputType.Keyboard, Keyboard = Key.A },
            ControllerControl = new InputBinding { Type = InputType.JoypadAxis, JoypadAxis = JoyAxis.LeftX, AxisValue = -1.0f }
        };

        public ControlGroup Shoot = new ControlGroup()
        {
            Name = "Shoot Primary",
            KeyboardControl = new InputBinding { Type = InputType.Keyboard, Keyboard = Key.Space },
            ControllerControl = new InputBinding { Type = InputType.JoypadButton, JoypadButton = JoyButton.A }
        };

        public ControlGroup Up = new ControlGroup()
        {
            Name = "Fly Up",
            KeyboardControl = new InputBinding { Type = InputType.Keyboard, Keyboard = Key.R },
            ControllerControl = new InputBinding { Type = InputType.JoypadButton, JoypadButton = JoyButton.DpadUp }
        };

        public ControlGroup Down = new ControlGroup()
        {
            Name = "Fly Down",
            KeyboardControl = new InputBinding { Type = InputType.Keyboard, Keyboard = Key.F },
            ControllerControl = new InputBinding { Type = InputType.JoypadButton, JoypadButton = JoyButton.DpadDown }
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

        // TODO automate this?
        public List<ControlGroup> GetControls() =>
            new List<ControlGroup> {
                Accelerate,
                Decelerate,
                Right,
                Left,
                Shoot,
                Up,
                Down,
            };

        public override List<Label> GetHeaders() => new List<Label> {
            new Label { Text = "Action", Name = "Action Header" },
            new Label { Text = "Keyboard", Name = "Keyboard Header" },
            new Label { Text = "Controller", Name = "Controller Header" }
        };
    }
}
