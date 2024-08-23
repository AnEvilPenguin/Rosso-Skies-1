using Godot;
using System;

public partial class Level0 : Node2D
{
	[Export]
	public PackedScene Enemy;
    [Export]
    public int TimerSeconds = 10;

    private PathFollow2D _spawnPoint;
    private double _timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        ResetTimer();
        _spawnPoint = GetNode<PathFollow2D>("%EnemySpawnLocation");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        _timer -= delta;

        if (_timer > 0)
            return;

        ResetTimer();

        SpawnMob();
    }

    private void ResetTimer() =>
        _timer = TimerSeconds;

    private void SpawnMob()
    {
        BasicEnemy mob = Enemy.Instantiate<BasicEnemy>();

        _spawnPoint.ProgressRatio = GD.Randf();

        // Set the mob's direction perpendicular to the path direction.
        float direction = _spawnPoint.Rotation + Mathf.Pi / 2;

        mob.Position = _spawnPoint.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        // If polling this into a manager we may want to consider adding this to the main scene instead?
        AddChild(mob);
    }
}
