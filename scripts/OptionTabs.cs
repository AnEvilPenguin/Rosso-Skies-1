using Godot;
using RossoSkies1.scripts.Settings;
using RossoSkies1.scripts.Managers;
using System.Collections.Generic;

public partial class OptionTabs : TabContainer
{
	private List<OptionTab> _tabs = new List<OptionTab>();

	public override void _Ready()
	{
		OptionManager.GetOptionsCategories()
			.ForEach(GenerateNewTab);
	}

	internal List<OptionTab> GetTabs() =>
		new List<OptionTab>(_tabs);

	private void GenerateNewTab(Options category)
	{
        var tab = new OptionTab()
		{ 
			Name = category.Name,
			Settings = category
		};

        _tabs.Add(tab);
        AddChild(tab);
    }
}
