using Godot;

public partial class BasicEnemy : Node2D
{
    [Export]
    public float Speed = 25;

    public Health Health;

	private Player _player;

	public override void _Ready()
	{
		Health = GetNode<Health>("Health");

		_player = GetParent().GetNode<Player>("%Player");

		Health.Destroyed += OnDestroyed;
	}

    public override void _Process(double delta)
    {
        // TODO bring rotation into the mix?
        var direction = GlobalPosition.DirectionTo(_player.GlobalPosition);

        
        
        Position += direction * (Speed * (float)delta);
    }

    private void OnDestroyed()
	{
        QueueFree();
    }
}
