using Godot;
using RossoSkies1.scripts.Components;

public partial class BasicGun : Node2D
{
    [Export]
	public Texture2D BulletTexture;

	[Export]
	public float RateOfFire = 10f;
	[Export]
	public float MuzzleVelocity = 400;
	[Export]
	public float Range = 400;
	[Export]
	public int[] Layer = new int [] { 3 };
	[Export]
	public int[] Mask = new int[] { 2 };

	private double _timer;

    public override void _Ready() =>
        ResetTimer();

    public override void _Process(double delta) =>
		_timer -= delta;

    public void Shoot(Vector2 direction, float rotation, float speed)
	{
		if (_timer > 0)
			return;


		// TODO consider the builder pattern
		// Should help with upgrades?
		var bullet = new Bullet()
		{
			Position = GetNode<Marker2D>("%Marker2D").GlobalPosition,
			Rotation = rotation,
		};

		bullet.SetDirection(direction) // Consider having bullets converge slightly?
		    .SetSpeed(speed + MuzzleVelocity)
			.SetLifetime(Range / MuzzleVelocity)
			.SetTexture(BulletTexture)
			.SetSpriteRotationDegrees(90)
			.SetSpriteScale(new Vector2(0.05f, 0.05f))
			.SetCollisionLayers(Layer)
			.SetCollisionMasks(Mask);

        GetNode("/root").AddChild(bullet);

		ResetTimer();
	}

	private void ResetTimer() =>
		_timer = 1 / RateOfFire;
}
