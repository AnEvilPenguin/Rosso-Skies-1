using Godot;

namespace RossoSkies1.scripts.Enemies
{
    internal partial class BasicFormation : Node2D
    {
        [Export]
        public PackedScene EnemyPrefab;

        [Export]
        public Player Player;

        private BasicEnemy _leader;

        private BasicEnemy _wing1;

        public override void _Ready()
        {
            _leader = EnemyPrefab.Instantiate<BasicEnemy>();
            _leader.Name = "Leader";

            AddChild(_leader);

            _wing1 = EnemyPrefab.Instantiate<BasicEnemy>(); 
            _wing1.Name = "Wing 1";

            AddChild(_wing1);

            Player = GetParent().GetNode<Player>("%Player");
        }

        // TODO Generate more wingmates
        // TODO Calculate target positions / rotations
        // TODO feed positioning into planes
        // TODO signal to planes when to shoot
        // TODO when no leader and below 50% run away from player
    }
}
