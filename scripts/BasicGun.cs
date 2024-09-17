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
	public int[] Layer = new int [] { 1 };
	[Export]
	public int[] Mask = new int[] { 4 };

	private double _timer;
	private float _initialScale;
	private int _currentLayer;

    public override void _Ready()
	{
        ResetTimer();

		_initialScale = Scale.X;
    }
        

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
			Name = GetParent().Name + $"-Bullet-{GD.Randi()}",
			Position = GetNode<Marker2D>("%Marker2D").GlobalPosition,
			Rotation = rotation,
		};

        bullet.SetDirection(direction) // Consider having bullets converge slightly?
		    .SetSpeed(speed + MuzzleVelocity)
			.SetLifetime((Range / MuzzleVelocity) * ((_currentLayer + 1) * 0.5f))
			.SetTexture(BulletTexture)
			.SetSpriteRotationDegrees(90)
			.SetScale(_currentLayer)
			.SetCollisionLayers(Layer)
			.SetCollisionMasks(Mask);

        GetNode("/root").AddChild(bullet);

		ResetTimer();
	}

	public void SetLayer(int layer) =>
        _currentLayer = layer;

	private void ResetTimer() =>
		_timer = 1 / RateOfFire;
}
