using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace RossoSkies1.scripts.Components
{
    public partial class Bullet : Area2D
    {
        public Damage Damage = new();

        private Vector2 _direction;
        private double _timer;
        private float _speed = 300;
        private float _lifetime = 1;
        private float _initialScale = 0.05f;
        private Sprite2D _sprite = new Sprite2D();
        private CollisionShape2D _collisionShape2D = new CollisionShape2D();

        private HashSet<UInt64> _damaged = new();

        public override void _Ready()
        {
            AddChild(_sprite);

            _collisionShape2D.Shape = new RectangleShape2D() { Size = new Vector2(3, 0.5f) };

            AddChild(_collisionShape2D);

            Monitoring = true;
            AreaEntered += OnAreaEntered;

            _timer = _lifetime;

            // Put behind other objects, but in front of background.
            // We probably need to revisit this in future.
            ZIndex = 1;
            ZAsRelative = false;

            base._Ready();
        }

        public override void _Process(double delta)
        {
            Position += _direction * (float)(_speed * delta);

            if (_timer < 0)
                QueueFree();

            _timer -= delta;
        }

        public Bullet SetDirection(Vector2 direction)
        {
            this._direction = direction; 
            return this;
        }

        public Bullet SetSpeed(float speed)
        {
            this._speed = speed;
            return this;
        }

        public Bullet SetLifetime(float lifetime)
        {
            this._lifetime = lifetime;
            return this;
        }

        public Bullet SetTexture(Texture2D texture)
        {
            _sprite.Texture = texture;
            return this;
        }

        public Bullet SetSpriteRotationDegrees(float rotation)
        {
            _sprite.RotationDegrees = rotation;
            return this;
        }

        public Bullet SetScale(int multiplier)
        {
            float scale = _initialScale + (_initialScale * (multiplier * 0.5f));
            Scale = new Vector2(scale, scale);
            return this;
        }

        public Bullet SetCollisionLayers(IEnumerable<int> layers)
        {
            CollisionLayer = CollisionAggregation(layers);
            return this;
        }

        public Bullet SetCollisionMasks(IEnumerable<int> mask)
        {
            CollisionMask = CollisionAggregation(mask);
            return this;
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

        private uint CollisionAggregation(IEnumerable<int> layers) =>
            (uint)layers.Aggregate(0, (acc, l) => acc + (int)Math.Pow(2, (double)(l - 1)));
    }
}
