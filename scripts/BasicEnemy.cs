using Godot;

public partial class BasicEnemy : Node2D
{
	public Health Health;

	public override void _Ready()
	{
		Health = GetNode<Health>("Health");

		Health.Destroyed += OnDestroyed;
	}

	private void OnDestroyed() =>
		QueueFree();
}
