using Godot;
using RossoSkies1.scripts.Settings;
using RossoSkies1.scripts.Managers;

public partial class OptionTabs : TabContainer
{
	public override void _Ready()
	{
		OptionManager.GetOptionsCategories()
			.ForEach(c => AddChild(new OptionTab() { Name = c.Name }));
	}
}
