using Godot;
using System;

public partial class Clouds : Node2D
{
	public Player Player;

    private float _x;
    private float _y;

    public override void _Ready()
    {
        _x = Position.X;
        _y = Position.Y;
    }

    public override void _PhysicsProcess(double delta)
    {
        var x = Player.GlobalPosition.X + _x;
        var y = Player.GlobalPosition.Y + _y;

        GlobalPosition = new Vector2(x, y);
    }
}
