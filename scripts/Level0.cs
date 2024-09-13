using Godot;
using System;
using System.Linq;

public partial class Level0 : Node2D
{
	[Export]
	public PackedScene Enemy;
    [Export]
    public float TimerSeconds = 7.5f;
    [Export]
    public int Ceiling = 4;

    private PathFollow2D _spawnPoint;
    private double _timer;

    private Player _player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        ResetTimer();

        _spawnPoint = GetNode<PathFollow2D>("%EnemySpawnLocation");

        _player = GetNode<Player>("%Player");
        var gui = GetNode<GUI>("GUI");

        gui.UpdateHealth(_player.Health.MaxHealth);

        _player.Health.DamageTaken += (int damage, int currentHealth) => gui.UpdateHealth(currentHealth);
        _player.Health.Destroyed += () => gui.UpdateHealth("destroyed");

        gui.UpdateLayer(_player.Layer.CurrentLayer);

        _player.Layer.Ceiling = Ceiling;
        _player.Layer.Floor = 0;

        _player.Layer.LayerChanged += (int layer, int previousLayer) => gui.UpdateLayer(layer);
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
        // FIXME make an interface for this
        PhysicsEnemy mob = Enemy.Instantiate<PhysicsEnemy>();

        _spawnPoint.ProgressRatio = GD.Randf();

        mob.Name = $"PhysicsEnemy-{GD.Randi()}";

        // Set the mob's direction perpendicular to the path direction.
        float direction = _spawnPoint.Rotation + Mathf.Pi / 2;

        mob.Position = _spawnPoint.Position;

        // Add some randomness to the direction.
        direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
        mob.Rotation = direction;

        // If polling this into a manager we may want to consider adding this to the main scene instead?
        AddChild(mob);
        
        mob.Layer.CurrentLayer = _player.Layer.CurrentLayer;
        mob.Layer.Ceiling = Ceiling;
    }
}
