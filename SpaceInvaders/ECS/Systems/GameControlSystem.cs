using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SpaceInvaders.ECS.Components;
using SpaceInvaders.ECS.Entities;
using SpaceInvaders.ECS.Nodes;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Systems {
	public class GameControlSystem : NodeIteratingSystem<GameNode> {
		private readonly HashSet<Keys> pressedKeys;
		private readonly Size gameSize;
		private readonly int targetY;

		private List<SpaceshipControlNode> spaceships;
		private List<EnemyShootNode> enemies;

		public GameControlSystem(HashSet<Keys> pressedKeys, Size gameSize, int targetY) : base(SystemOrder.BEFORE) {
			this.pressedKeys = pressedKeys;
			this.gameSize = gameSize;
			this.targetY = targetY;
		}

		public override void Start(Engine engine) {
			base.Start(engine);
			this.spaceships = engine.GetNodes<SpaceshipControlNode>();
			this.enemies = engine.GetNodes<EnemyShootNode>();
		}

		public override void UpdateNode(Engine engine, GameNode node, double dt) {
			if (node.Game.IsInGame) {
				if (this.enemies.Count == 0) { // Victory
					this.GameEnd(engine, node.Game, true);
				} else if (this.spaceships.Count == 0 || this.enemies.Any(enemy => enemy.Position.Y + enemy.Size.Height >= this.targetY)) { // Defeat
					this.GameEnd(engine, node.Game, false);
				}
			} else if (this.pressedKeys.Remove(Config.StartControl)) { // Start
				// Create player spaceship
				Vector2D spaceshipPos = new Vector2D(this.gameSize.Width / 2d - Config.SpaceshipImage.Width / 2d, this.gameSize.Height - Config.SpaceshipElevation - Config.SpaceshipImage.Height);
				Entity spaceship = EntityCreator.NewSpaceship(spaceshipPos, Config.SpaceshipHealth, Config.SpaceshipImage);
				engine.AddEntity(spaceship);

				// Create bunkers
				int bunkerY = this.gameSize.Height - Config.BunkerElevation - Config.BunkerImage.Height;
				double spacing = (this.gameSize.Width - (Config.BunkerImage.Width * Config.BunkerCount)) / (Config.BunkerCount + 1d);
				for (double x = spacing; x < this.gameSize.Width; x += Config.BunkerImage.Width + spacing)
					engine.AddEntity(EntityCreator.NewBunker(new Vector2D(x, bunkerY), Config.BunkerImage));

				// Create enemies and block
				List<Entity> newEnemies = this.GenerateEnemies(
						new EnemyLine(Config.EnemyLargeImage, Config.EnemyLargeCount, Config.EnemyLargeHealth),
						new EnemyLine(Config.EnemyMediumImage, Config.EnemyMediumCount, Config.EnemyMediumHealth),
						new EnemyLine(Config.EnemySmallImage, Config.EnemySmallCount, Config.EnemySmallHealth),
						new EnemyLine(Config.EnemyBasicImage, Config.EnemyBasicCount, Config.EnemyBasicHealth)
				);
				newEnemies.ForEach(engine.AddEntity);
				engine.AddEntity(EntityCreator.NewEnemyBlock(new Vector2D(Config.EnemySpeed), newEnemies));

				// Re-init game node
				node.Game.GameState = GameState.PLAYING;
				node.Game.PreviousIsVictory = null;
				node.PlayerHealth = spaceship.Components.Get<HealthComponent>();
			}
		}

		private void GameEnd(Engine engine, GameComponent game, bool victory) {
			engine.CleanUpEntities(EntityType.SPACESHIP, EntityType.ENEMY, EntityType.MISSILE, EntityType.BUNKER, EntityType.ENEMY_BLOCK);
			game.GameState = GameState.MENU;
			game.PreviousIsVictory = victory;
		}

		private List<Entity> GenerateEnemies(params EnemyLine[] lines) {
			List<Entity> entities = new List<Entity>();
			double width = 0;
			foreach (EnemyLine line in lines)
				if (line.Width > width)
					width = line.Width;
			double x, y = 0;
			foreach (EnemyLine line in lines) {
				double spacing = (width - line.Count * line.EnemyWidth) / (line.Count - 1);
				for (x = 0; x < width; x += spacing + line.EnemyWidth)
					entities.Add(EntityCreator.NewEnemy(new Vector2D(x, y), line.Health, line.Image));
				y += line.Image.Height + Config.EnemySpacing;
			}
			return entities;
		}
	}

	public class EnemyLine {
		public Bitmap Image;
		public int Count;
		public int Health;

		public double EnemyWidth {
			get { return this.Image.Width; }
		}

		public double Width {
			get { return this.Count * this.EnemyWidth + (this.Count - 1) * Config.EnemySpacing; }
		}

		public EnemyLine(Bitmap image, int count, int health) {
			this.Image = image;
			this.Count = count;
			this.Health = health;
		}
	}
}
