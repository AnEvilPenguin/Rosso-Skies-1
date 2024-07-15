using Godot;
using System;

public partial class MainMenu : Control
{
	private Button _playButton;
	private Button _upgradeButton;
	private Button _statsButton;
	private Button _settingsButton;
	private Button _quitButton;

	public override void _Ready()
	{
		_quitButton = GetNode<Button>("%QuitButton");
	}

	public void OnQuit()
	{
		// FIXME move to propagation when logging implemented
        //GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);

        GetTree().Quit();
    }
}
