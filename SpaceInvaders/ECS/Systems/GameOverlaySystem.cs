using System.Drawing;
using SpaceInvaders.ECS.Components;
using SpaceInvaders.ECS.Nodes;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Systems {
	public class GameOverlaySystem : NodeIteratingSystem<GameNode> {
		private readonly Graphics graphics;
		private readonly Size gameSize;

		public GameOverlaySystem(Graphics graphics, Size gameSize) : base(SystemOrder.POST_RENDER, true) {
			this.graphics = graphics;
			this.gameSize = gameSize;
		}

		public override void UpdateNode(Engine engine, GameNode node, double dt) {
			if (node.Game.IsInGame) {
				int health = node.PlayerHealth.Health;
				this.graphics.DrawString("Health : " + health, Util.FONT, Util.BLACK_BRUSH, 10, this.gameSize.Height - 34);
				
				if (node.Game.GameState == GameState.PAUSED) {
					string pause = "Paused";
					this.graphics.DrawString(pause, Util.FONT, Util.BLACK_BRUSH, this.gameSize.Width / 2f - TextWidth(pause) / 2, 10);
				}
			} else {
				string title = "Space Invaders";
				this.graphics.DrawString(title, Util.FONT, Util.BLACK_BRUSH, this.gameSize.Width / 2f - TextWidth(title) / 2, this.gameSize.Height / 2f - 12);

				this.graphics.DrawString("Press Space to play", Util.FONT, Util.BLACK_BRUSH, 10, this.gameSize.Height - 34);

				string credits = "By Théo SZANTO";
				this.graphics.DrawString(credits, Util.FONT, Util.BLACK_BRUSH, this.gameSize.Width - TextWidth(credits) - 10, this.gameSize.Height - 34);

				if (node.Game.HasPrevious) {
					string result = node.Game.PreviousIsVictory == true ? "Victory" : "Defeat";
					this.graphics.DrawString(result, Util.FONT, Util.BLACK_BRUSH, this.gameSize.Width / 2f - TextWidth(result) / 2, 10);
				}
			}
		}

		private float TextWidth(string text) {
			return this.graphics.MeasureString(text, Util.FONT).Width;
		}
	}
}
