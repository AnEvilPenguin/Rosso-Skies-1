using Godot;
using System;

public partial class MainMenu : Control
{
    private MenuButton _playButton;
	private MenuButton _upgradeButton;
	private MenuButton _statsButton;
	private MenuButton _settingsButton;
	private MenuButton _quitButton;

	public override void _Ready()
	{
		_playButton = GetNode<MenuButton>("%PlayButton");
        _settingsButton = GetNode<MenuButton>("%SettingsButton");
        _quitButton = GetNode<MenuButton>("%QuitButton");
	}

	public void OnQuit()
	{
		// FIXME move to propagation when logging implemented
        //GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);

        GetTree().Quit();
    }

	public void OnSettings()
	{
        GetTree().ChangeSceneToFile("res://scenes/options_menu.tscn");
    }

	public void OnPlay()
	{
		GetTree().ChangeSceneToFile("res://scenes/tutorial_controls.tscn");
	}
}
