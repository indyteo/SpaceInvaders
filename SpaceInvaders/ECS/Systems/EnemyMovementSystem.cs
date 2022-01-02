using System.Drawing;
using SpaceInvaders.ECS.Components;
using SpaceInvaders.ECS.Nodes;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Systems {
	public class EnemyMovementSystem : NodeIteratingSystem<EnemyMovementNode> {
		private readonly Size gameSize;
		private readonly int targetY;

		public EnemyMovementSystem(Size gameSize, int targetY) : base(SystemOrder.PRE_UPDATE) {
			this.gameSize = gameSize;
			this.targetY = targetY;
		}

		public override void UpdateNode(Engine engine, EnemyMovementNode node, double dt) {
			Vector2D before = node.Position.Position.Copy();
			node.Position.Position += node.Motion.Motion * dt;
			if (node.Position.X < 0 || node.Position.X + node.Size.Width >= this.gameSize.Width) {
				node.Position.X = Util.MinMax(0, node.Position.X, this.gameSize.Width - node.Size.Width - 1);
				node.Motion.Motion = new Vector2D(-Util.Sign(node.Motion.Motion.X) * this.EnemyMoveSpeed(node));
				node.Position.Y += Config.EnemyDrop;
			}
			Vector2D delta = node.Position.Position - before;
			foreach (Entity child in node.Children.Children)
				child.Components.Get<PositionComponent>().Position += delta;
		}

		private double EnemyMoveSpeed(EnemyMovementNode node) {
			return Config.EnemySpeed + Config.EnemySpeedBonus * (node.Position.Y / (this.targetY - node.Size.Height));
		}
	}
}
