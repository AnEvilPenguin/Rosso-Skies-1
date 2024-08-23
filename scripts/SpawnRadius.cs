using Godot;

public partial class SpawnRadius : Path2D
{
	// TODO we probably just want a dedicated enemy spawn manager and this should be a part of that.
	[Export]
	public int TimerSeconds = 5;
	[Export]
	public float Radius = 500;
	[Export]
	public int Points = 360;

	private Player _player;

	private double _timer;

	public override void _Ready()
	{
		_player = GetNode<Player>("Player");
		ResetTimer();
	}

	public override void _Process(double delta)
	{
		_timer -= delta;

		if (_timer > 0)
			return;

		ResetTimer();
		
		Curve.ClearPoints();

		SetCircularPath(_player.Position, Radius, Points);
	}

	private void SetCircularPath(Vector2 center, float radius, int points)
	{
		// Some (All)? of this circle calculation is also available in the Geometry2D class (class not node)
		for (int i = 0; i < points; i++)
		{
			var angle = (i / (float)points) * Mathf.Tau;

			var x = center.X + radius * Mathf.Cos(angle);
			var y = center.Y + radius * Mathf.Sin(angle);

			Curve.AddPoint(new Vector2 (x, y));
		}
	}

	private void ResetTimer() =>
        _timer = TimerSeconds;
}
