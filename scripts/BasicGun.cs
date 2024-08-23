using Godot;

public partial class BasicGun : Node2D
{
	[Export]
	public PackedScene Bullet;

	[Export]
	public double RateOfFire = 10d;

	private double _timer;

    public override void _Ready() =>
        ResetTimer();

    public override void _Process(double delta) =>
		_timer -= delta;

    public void Shoot(Vector2 direction, float rotation, float speed)
	{
		if (_timer > 0)
			return;

		var bullet = Bullet.Instantiate<BasicBullet>();

		bullet.Direction = direction;
		bullet.Position = GlobalPosition;
		bullet.Speed += speed;
		bullet.Rotation = rotation;
        GetNode("/root").AddChild(bullet);

		ResetTimer();
	}

	private void ResetTimer() =>
		_timer = 1 / RateOfFire;
}
