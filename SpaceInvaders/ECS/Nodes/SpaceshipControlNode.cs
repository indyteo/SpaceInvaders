using SpaceInvaders.ECS.Components;

namespace SpaceInvaders.ECS.Nodes {
	public class SpaceshipControlNode : Node {
		public PositionComponent Position;
		public SizeComponent Size;
		public ChildrenComponent CurrentMissile;
	}
}
