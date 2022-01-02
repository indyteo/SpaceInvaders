using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.ECS.Entities;
using SpaceInvaders.ECS.Nodes;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Systems {
	public class SpaceshipControlSystem : NodeIteratingSystem<SpaceshipControlNode> {
		private readonly HashSet<Keys> pressedKeys;
		private readonly Size gameSize;

		public SpaceshipControlSystem(HashSet<Keys> pressedKeys, Size gameSize) : base(SystemOrder.PRE_UPDATE) {
			this.pressedKeys = pressedKeys;
			this.gameSize = gameSize;
		}

		public override void UpdateNode(Engine engine, SpaceshipControlNode node, double dt) {
			if (node.CurrentMissile.HasNone && this.pressedKeys.Contains(Config.ShootControl)) {
				Vector2D missilePos = new Vector2D(node.Position.X + node.Size.Width / 2d - Config.MissileImage.Width / 2d, node.Position.Y - Config.MissileImage.Height);
				Entity missile = EntityCreator.NewMissile(missilePos, Config.MissileHealth, Config.MissileImage, new Vector2D(y: -Config.MissileSpeed), EntitySide.ALLY);
				node.CurrentMissile.Add(missile);
				engine.AddEntity(missile);
			}

			if (this.pressedKeys.Contains(Config.LeftControl))
				node.Position.X = Util.MinMax(0, node.Position.X - Config.MoveSpeed * dt, this.gameSize.Width - node.Size.Width);
			if (this.pressedKeys.Contains(Config.RightControl))
				node.Position.X = Util.MinMax(0, node.Position.X + Config.MoveSpeed * dt, this.gameSize.Width - node.Size.Width);
		}
	}
}
