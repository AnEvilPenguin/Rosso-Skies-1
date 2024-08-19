using Godot;
using System;

public partial class BasicGun : Node2D
{
	[Export]
	public PackedScene Bullet;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
    }

	public void Shoot(Vector2 direction, float speed)
	{
		var bullet = Bullet.Instantiate<BasicBullet>();

		bullet.Direction = direction;
		bullet.Position = GlobalPosition;
		bullet.Speed += speed;

        GetNode("/root").AddChild(bullet);
	}
}
