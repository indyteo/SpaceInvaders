using SpaceInvaders.ECS.Components;

namespace SpaceInvaders.ECS.Nodes {
	public class EnemyMovementNode : Node {
		public PositionComponent Position;
		public SizeComponent Size;
		public MotionComponent Motion;
		public ChildrenComponent Children;
	}
}
