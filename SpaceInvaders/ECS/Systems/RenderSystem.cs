using System.Drawing;
using SpaceInvaders.ECS.Nodes;

namespace SpaceInvaders.ECS.Systems {
	public class RenderSystem : NodeIteratingSystem<RenderNode> {
		private readonly Graphics graphics;

		public RenderSystem(Graphics graphics) : base(SystemOrder.RENDER, true) {
			this.graphics = graphics;
		}

		public override void UpdateNode(Engine engine, RenderNode node, double dt) {
			this.graphics.DrawImage(node.Display.Image, (float) node.Position.X, (float) node.Position.Y, node.Display.Width, node.Display.Height);
		}
	}
}
