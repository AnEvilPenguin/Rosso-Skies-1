using Godot;
using RossoSkies1.scripts.Managers;
using System;

public partial class OptionsMenu : Control
{
    public void OnApplyClicked() =>
        OptionManager.Save();

    public void OnBackClicked() =>
         GetTree()
        .ChangeSceneToFile("res://scenes/main_menu.tscn");
}
