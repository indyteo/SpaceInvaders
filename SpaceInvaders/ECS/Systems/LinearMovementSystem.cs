using System.Drawing;
using SpaceInvaders.ECS.Nodes;

namespace SpaceInvaders.ECS.Systems {
	public class LinearMovementSystem : NodeIteratingSystem<LinearMovementNode> {
		private readonly Size gameSize;

		public LinearMovementSystem(Size gameSize) : base(SystemOrder.PRE_UPDATE) {
			this.gameSize = gameSize;
		}

		public override void UpdateNode(Engine engine, LinearMovementNode node, double dt) {
			node.Position.Position += node.Motion.Motion * dt;
			if (node.Position.X < -node.Size.Width || node.Position.X >= this.gameSize.Width
					|| node.Position.Y < -node.Size.Height || node.Position.Y >= this.gameSize.Height)
				engine.RemoveEntity(node.Source);
		}
	}
}
