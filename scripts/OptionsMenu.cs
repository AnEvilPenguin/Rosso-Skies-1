using Godot;
using RossoSkies1.scripts.Managers;
using System;

public partial class OptionsMenu : Control
{
    private Button _applyButton;
    private OptionTabs _tabContainer;

    public override void _Ready()
    {
        _applyButton = GetNode<Button>("%ApplyButton");
        _tabContainer = GetNode<OptionTabs>("%TabContainer");

        _tabContainer.GetTabs()
            .ForEach(tab => tab.Settings.OptionsModified += OnOptionsModified);
    }

    public void OnApplyClicked()
    {
        OptionManager.Save();
        _applyButton.Disabled = true;
    }
        

    public void OnBackClicked() =>
         GetTree().ChangeSceneToFile("res://scenes/main_menu.tscn");

    public void OnOptionsModified(object sender, EventArgs e) =>
        _applyButton.Disabled = false;
}
