using Godot;
using RossoSkies1.scripts.Util;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class PhysicsEnemy : CharacterBody2D
{
    [Export]
    public float Speed = 25.0f;

    public float CurrentSpeed;

    public Health Health;
    public Layer Layer;

    private Player _player;

    private RayCast2D _playerDetection;
    private IEnumerable<RayCast2D> _enemyDetection; // enemy being a player enemy
    private BasicGun _gun;

    private AnimatedSprite2D _sprite;

    private HashSet<PhysicsEnemy> _slow = new();
    
    private SimpleTimer _layerTimer = new SimpleTimer();
    private int _targetLayer;

    private Area2D _hitBox;

    public override void _Ready()
    {
        Health = GetNode<Health>("Health");

        _playerDetection = GetNode<RayCast2D>("%PlayerDetectionRayCast");

        _sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _sprite.Play("idle");

        var detectionNode = GetNode<Node>("%ForwardEnemyDetection");
        _enemyDetection = detectionNode.GetChildren().OfType<RayCast2D>();

        _gun = GetNode<BasicGun>("%BasicGun");
        _gun.Layer = new int[] { 2 };
        _gun.Mask = new int[] { 3 };

        _hitBox = GetNode<Area2D>("HitBox");

        CurrentSpeed = Speed;

        _player = GetParent().GetNode<Player>("%Player");

        _player.Layer.LayerChanged += (int layer, int previousLayer) => OnPlayerLayerChanged(layer);
        _layerTimer.TimerElapsed += OnLayerTimerElapsed;
        AddChild(_layerTimer);

        Health.Destroyed += OnDestroyed;

        Layer = GetNode<Layer>("Layer");
        Layer.LayerChanged += OnLayerChange;
    }

    public override void _Process(double delta)
    {
        if (_playerDetection.GetCollider() is Player)
        {
            _gun.Shoot(GlobalPosition.DirectionTo(_player.GlobalPosition), Rotation, Speed);
        }

        _slow.Clear();

        foreach (var ray in _enemyDetection)
        {
            if (ray.GetCollider() != null)
            {
                var collided = ray.GetCollider() as Node2D;
                if (collided is PhysicsEnemy enemy)
                {
                    _slow.Add(enemy);
                }
            }
        }

        SetCurrentSpeed();
    }

    public override void _PhysicsProcess(double delta)
	{
        ProcessRotation();

        var direction = new Vector2(MathF.Cos(Rotation), MathF.Sin(Rotation));

        Velocity = direction * CurrentSpeed;

        MoveAndSlide();
	}

    private void OnDestroyed() =>
        QueueFree();

    private void ProcessRotation()
    {
        var direction = GlobalPosition.DirectionTo(_player.GlobalPosition);

        // TODO figure out how to set enemy agility
        var rotation = direction.Angle();

        // TODO shoot when within a certain angle/distance of player

        Rotation = rotation;
    }

    private void SetCurrentSpeed()
    {
        CurrentSpeed = Speed;

        if (_slow.Count == 0)
            return;

        var bottom = _slow.Aggregate(CurrentSpeed, (acc, enemy) => {
            return Mathf.Min(acc, enemy.CurrentSpeed);
        });

        CurrentSpeed = (float)Mathf.Max(0.2, bottom * 0.8f);
    }

    private void OnPlayerLayerChanged(int layer)
    {
        _targetLayer = layer;

        if (_layerTimer.IsRunning())
            return;

        StartTimer();
    }

    private void StartTimer()
    {
        _layerTimer.Time = GD.RandRange(5f, 15f);
        _layerTimer.Start();
    }

    private void OnLayerTimerElapsed()
    {
        if (_targetLayer == Layer.CurrentLayer)
            return;

        if (_targetLayer > Layer.CurrentLayer)
            Layer.RaiseLayer();
        else if (_targetLayer < Layer.CurrentLayer)
            Layer.LowerLayer();

        if (_targetLayer != Layer.CurrentLayer)
            StartTimer();
    }

    private void OnLayerChange(int layer, int previousLayer)
    {
        // TODO make this not terrible
        // Probably put it into the layer or something?
        // FIXME Ideally we should see if we can work out why setting via a bitmask doesn't work as expected?

        var newLayer = getLayer(layer);
        var newMask = getMask(layer);

        var oldLayer = getLayer(previousLayer);
        var oldMask = getMask(previousLayer);

        SetCollisionLayerValue(oldLayer, false);
        SetCollisionLayerValue(newLayer, true);

        _hitBox.SetCollisionLayerValue(oldLayer, false);
        _hitBox.SetCollisionLayerValue(newLayer, true);

        SetCollisionMaskValue(oldMask, false);
        SetCollisionMaskValue(newMask, true);

        _hitBox.SetCollisionMaskValue(oldMask, false);
        _hitBox.SetCollisionMaskValue(newMask, true);

        // Conveniently handles the player mask (which is our layer);
        SetCollisionMaskValue(oldLayer, false);
        SetCollisionMaskValue(newLayer, true);

        ZIndex = 3 + (layer * 10);

        _gun.Mask = new int[] { newMask };

        _playerDetection.SetCollisionMaskValue(oldMask, false);
        _playerDetection.SetCollisionMaskValue(newMask, true);

        foreach (RayCast2D ray in _enemyDetection)
        {
            ray.SetCollisionMaskValue(oldLayer, false);
            ray.SetCollisionMaskValue(newLayer, true);
        }

        SetScale(layer, previousLayer);
    }

    private int getLayer(int layer) =>
        2 * layer + 4;

    private int getMask(int layer) =>
        2 * layer + 3;

    private void SetScale(int layer, int previous)
    {
        var scale = Scale;
        var diff = layer - previous;
        var newScale = scale.X + (0.5f * diff);
        Scale = new Vector2(newScale, newScale);

        _gun.SetLayer(layer);
    }
}
