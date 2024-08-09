using Godot;
using Newtonsoft.Json.Linq;

namespace RossoSkies1.scripts.Settings
{
    internal class ControlGroup
    {
        public string Name;
        public InputEvent KeyboardControl;
        public InputEvent ControllerControl;
    }

    internal class Controls : Options
    {
        public ControlGroup Accelerate = new ControlGroup()
        {
            Name = "Accelerate",
            KeyboardControl = new InputEventKey() { Keycode = Key.W },
            ControllerControl = new InputEventJoypadMotion { Axis = JoyAxis.LeftY, AxisValue = 1.0f }
        };

        public Controls()
        {
            Name = "Controls";
            _defaultSettings = JObject.FromObject(this);
        }
    }
}
