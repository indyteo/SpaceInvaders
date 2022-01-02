using System;
using SpaceInvaders.ECS.Entities;
using SpaceInvaders.ECS.Nodes;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Systems {
	public class EnemyShootSystem : NodeIteratingSystem<EnemyShootNode> {
		private readonly Random random = new Random();
		private readonly int targetY;

		public EnemyShootSystem(int targetY) : base(SystemOrder.POST_UPDATE) {
			this.targetY = targetY;
		}

		public override void UpdateNode(Engine engine, EnemyShootNode node, double dt) {
			if (node.CurrentMissile.HasNone && this.random.NextDouble() < this.EnemyShootChance(node) * dt) {
				Vector2D missilePos = new Vector2D(node.Position.X + node.Size.Width / 2d - Config.MissileImage.Width / 2d, node.Position.Y + node.Size.Height);
				Entity missile = EntityCreator.NewMissile(missilePos, Config.MissileHealth, Config.MissileImage, new Vector2D(y: Config.MissileSpeed), EntitySide.ENEMY);
				node.CurrentMissile.Add(missile);
				engine.AddEntity(missile);
			}
		}

		private double EnemyShootChance(EnemyShootNode node) {
			return Config.EnemyShootChance + Config.EnemyShootChanceBonus * (node.Position.Y / (this.targetY - node.Size.Height));
		}
	}
}
