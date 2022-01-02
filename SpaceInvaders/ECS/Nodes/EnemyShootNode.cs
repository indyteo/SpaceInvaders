using SpaceInvaders.ECS.Components;

namespace SpaceInvaders.ECS.Nodes {
	public class EnemyShootNode : Node {
		public PositionComponent Position;
		public SizeComponent Size;
		public ChildrenComponent CurrentMissile;
	}
}
