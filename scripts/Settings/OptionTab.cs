using Godot;

namespace RossoSkies1.scripts.Settings
{
    internal partial class OptionTab : MarginContainer
    {
        public bool HasUnsavedChanges { get; protected set; } = false;
    }
}
