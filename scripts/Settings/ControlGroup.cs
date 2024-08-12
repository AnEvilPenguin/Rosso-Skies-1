using Godot;
using Newtonsoft.Json.Linq;
using System;

namespace RossoSkies1.scripts.Settings
{
    public enum InputType
    {
        Keyboard,
        JoypadAxis,
        JoypadButton,
        MouseButton
    }
    public class InputBinding : IEquatable<InputBinding>
    {
        public InputType Type;

        public Key Keyboard;
        public JoyAxis JoypadAxis;
        public float AxisValue;
        public JoyButton JoypadButton;
        public MouseButton MouseButton;

        public JObject ToJObject()
        {
            var output = new JObject();

            output.Add("Type", (int)Type);

            switch (Type)
            {
                case InputType.Keyboard:
                    output.Add("Keyboard", (int)Keyboard);
                    break;

                case InputType.JoypadAxis:
                    output.Add("JoypadAxis", (int)JoypadAxis);
                    output.Add("AxisValue", (int)AxisValue);
                    break;

                case InputType.MouseButton:
                    output.Add("MouseButton", (int)MouseButton);
                    break;

                case InputType.JoypadButton:
                    output.Add("JoypadButton", (int)JoypadButton);
                    break;
            }

            return output;
        }

        public InputEvent ToInputEvent() =>
            Type switch
            {
                InputType.MouseButton => new InputEventMouseButton { ButtonIndex = MouseButton },
                InputType.JoypadAxis => new InputEventJoypadMotion { Axis = JoypadAxis, AxisValue = AxisValue },
                InputType.JoypadButton => new InputEventJoypadButton { ButtonIndex = JoypadButton },
                _ => new InputEventKey { Keycode = Keyboard }
            };

        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                
                hash *= 23 + Type.GetHashCode();
                
                switch (Type)
                {
                    case InputType.Keyboard:
                        hash *= 23 + Keyboard.GetHashCode();
                        break;

                    case InputType.JoypadAxis:
                        hash *= 23 + JoypadAxis.GetHashCode();
                        hash *= 23 + AxisValue.GetHashCode();
                        break;

                    case InputType.JoypadButton:
                        hash *= -23 + JoypadButton.GetHashCode();
                        break;

                    case InputType.MouseButton:
                        hash *= -23 + MouseButton.GetHashCode();
                        break;
                }

                return hash;
            }
        }

        public bool Equals(InputBinding other)
        {
            if (other.Type != Type) return false;

            return Type switch
            {
                InputType.Keyboard => other.Keyboard == Keyboard,
                InputType.JoypadAxis => other.JoypadAxis == JoypadAxis && other.AxisValue == AxisValue,
                InputType.JoypadButton => other.JoypadButton == JoypadButton,
                _ => other.MouseButton == MouseButton,
            };
        }
    }

    public class ControlGroup : IEquatable<ControlGroup>
    {
        public string Name;
        public InputBinding KeyboardControl;
        public InputBinding ControllerControl;

        public bool Equals(ControlGroup other) =>
            other.Name == Name && 
            other.KeyboardControl.Equals(KeyboardControl) && 
            other.ControllerControl.Equals(ControllerControl);

        public JObject ToJObject()
        {
            var output = new JObject();

            output.Add("KeyboardControl", KeyboardControl.ToJObject());
            output.Add("ControllerControl", ControllerControl.ToJObject());

            return output;
        }

        public JObject GetDifferences(ControlGroup other)
        {
            var output = new JObject();

            if (!other.KeyboardControl.Equals(KeyboardControl))
                output.Add("KeyboardControl", KeyboardControl.ToJObject());

            if (!other.ControllerControl.Equals(ControllerControl))
                output.Add("ControllerControl", ControllerControl.ToJObject());

            return output;
        }
    }
}
