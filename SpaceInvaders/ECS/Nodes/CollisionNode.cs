using SpaceInvaders.ECS.Components;

namespace SpaceInvaders.ECS.Nodes {
	public abstract class CollisionNode : Node {
		public PositionComponent Position;
		public DisplayComponent Display;
		public HealthComponent Health;
	}

	public abstract class SidedCollisionNode : CollisionNode {
		public SideComponent Side;
	}

	public class MissileCollisionNode : SidedCollisionNode {}

	public class SpaceshipCollisionNode : SidedCollisionNode {}

	public class BunkerCollisionNode : CollisionNode {}
}
