using Godot;

public partial class Tutorial : Node
{
    private void OnEnterHeld()
    {       
        GetTree().ChangeSceneToFile("res://scenes/level_0.tscn");
    }
}
