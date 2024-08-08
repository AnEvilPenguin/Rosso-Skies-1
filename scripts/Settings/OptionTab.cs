using Godot;
using System.Linq;
using System.Reflection;

namespace RossoSkies1.scripts.Settings
{
    internal partial class OptionTab : MarginContainer
    {
        public bool HasUnsavedChanges { get; protected set; } = false;

        public Options Settings;

        public override void _Ready()
        {
            Settings.GetType()
                .GetProperties()
                .ToList()
                .ForEach(GeneratePropertyControl);
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

        private void GenerateBooleanControl(PropertyInfo property)
        {
            var control = new BooleanControl();
            AddChild(control);

            control.Label.Text = property.Name;

            control.Slider.Toggled += (state) => property.SetValue(Settings, state);
            control.Slider.ButtonPressed = (bool)property.GetValue(Settings);
        }
    }
}
