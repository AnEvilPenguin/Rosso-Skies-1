using Godot;
using System.Linq;
using System.Reflection;

namespace RossoSkies1.scripts.Settings
{
    internal partial class OptionTab : CenterContainer
    {
        // FIXME Make child classes for each type. Should make this less mental.

        public bool HasUnsavedChanges { get; protected set; } = false;

        public Options Settings;

        private GridContainer _box;

        public override void _Ready()
        {
            // May need to consider a scrollbox in future.
            // CenterContainer will squash it, so a CustomMinimum sizing seems to be a thing?
            _box = new GridContainer
            {
                Columns = Settings.Columns,
                CustomMinimumSize = new Vector2(300, 300)
            };

            AddChild(_box);

            var type = Settings.GetType();
            
            Settings.GetHeaders()
                .ForEach(label => _box.AddChild(label));

            type.GetProperties()
                .ToList()
                .ForEach(GeneratePropertyControl);

            type.GetFields()
                .ToList()
                .ForEach(GenerateFieldControl);
        }

        private void GeneratePropertyControl(PropertyInfo property)
        {
            switch (property.PropertyType.Name)
            {
                case "Boolean":
                    GenerateBooleanControl(property);
                    return;
            }
        }

        private void GenerateFieldControl(FieldInfo field)
        {
            switch (field.FieldType.Name)
            {
                case "ControlGroup":
                    GenerateControlGroupControl(field);
                    return;
            }
        }

        private void GenerateBooleanControl(PropertyInfo property)
        {
            var control = new BooleanControl();
            _box.AddChild(control);

            control.Label.Text = property.Name;

            control.Slider.Toggled += (state) => {
                property.SetValue(Settings, state);

                Settings.HandleOptionsModified();
            };
            control.Slider.ButtonPressed = (bool)property.GetValue(Settings);
        }

        private void GenerateControlGroupControl(FieldInfo field)
        {
            var controlGroup = field.GetValue(Settings) as ControlGroup;

            if (controlGroup == null)
            {
                GD.PushError($"No Control Group for {field.Name}");
                return;
            }
            
            var control = new InputControl();

            control.Name = controlGroup.Name;

            control.ConfigureGrid(_box);

            control.SetKeyboard(controlGroup.KeyboardControl)
                .SetContoller(controlGroup.ControllerControl);

            control.KeyButton.BindingChanged += (InputEvent @event) =>
            {
                InputBinding binding = new InputBinding();

                if (@event is InputEventKey eventKey)
                {
                    binding.Type = InputType.Keyboard;
                    binding.Keyboard = eventKey.Keycode;
                }
                else if (@event is InputEventMouseButton eventMouse)
                {
                    binding.Type = InputType.MouseButton;
                    binding.MouseButton = eventMouse.ButtonIndex;
                }

                controlGroup.KeyboardControl = binding;

                control.SetKeyboard(binding);

                Settings.HandleOptionsModified();
            };

            control.ControllerButton.BindingChanged += (InputEvent @event) =>
            {
                InputBinding binding = new InputBinding();

                if (@event is InputEventJoypadMotion joyMotion)
                {
                    binding.Type = InputType.JoypadAxis;
                    binding.JoypadAxis = joyMotion.Axis;
                    binding.AxisValue = joyMotion.AxisValue;
                }
                else if (@event is InputEventJoypadButton joyButton)
                {
                    binding.Type = InputType.JoypadButton;
                    binding.JoypadButton = joyButton.ButtonIndex;
                }

                controlGroup.ControllerControl = binding;

                control.SetContoller(binding);

                Settings.HandleOptionsModified();
            };
        }
    }
}
