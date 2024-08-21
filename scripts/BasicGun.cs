using Godot;

public partial class BasicGun : Node2D
{
	[Export]
	public PackedScene Bullet;

	public void Shoot(Vector2 direction, float rotation, float speed)
	{
		var bullet = Bullet.Instantiate<BasicBullet>();

		bullet.Direction = direction;
		bullet.Position = GlobalPosition;
		bullet.Speed += speed;
		bullet.Rotation = rotation;
        GetNode("/root").AddChild(bullet);
	}
}
