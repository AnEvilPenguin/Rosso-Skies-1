using Godot;
using System;
using System.Collections.Generic;
using static Godot.TextServer;

public partial class BasicBullet : Node2D
{
	public Vector2 Direction;
	public Damage Damage;

	[Export]
	public float Speed = 300;
    [Export]
    public float Lifetime = 1;

	private double _timer;

	private HashSet<UInt64> _damaged = new HashSet<UInt64>();

	public override void _Ready()
	{
		_timer = Lifetime;
		Damage = new Damage();
	}

	public override void _Process(double delta)
	{
		Position += Direction * (float)(Speed * delta);

		if (_timer < 0)
			QueueFree();

		_timer -= delta;
	}

	private void OnAreaEntered(Area2D area)
	{
		UInt64 targetAreaId = area.GetInstanceId();


		if (_damaged.Contains(targetAreaId))
			return;

		_damaged.Add(targetAreaId);

		var target = area.GetParent();

		var health = target.GetNode<Health>("Health");

		if (health == null)
			return;

		health.TakeDamage(Damage);

		if (_damaged.Count > Damage.Piercing)
			QueueFree();
    }
}
