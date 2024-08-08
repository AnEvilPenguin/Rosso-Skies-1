using Godot;
using RossoSkies1.scripts.Settings;
using RossoSkies1.scripts.Managers;
using System.Collections.Generic;

public partial class OptionTabs : TabContainer
{
	private List<OptionTab> Tabs = new List<OptionTab>();

	public override void _Ready()
	{
		OptionManager.GetOptionsCategories()
			.ForEach(GenerateNewTab);
	}

	private void GenerateNewTab(Options category)
	{
        var tab = new OptionTab()
		{ 
			Name = category.Name,
			Settings = category
		};

        Tabs.Add(tab);
        AddChild(tab);
    }
}
