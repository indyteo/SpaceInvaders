using System.Collections.Generic;
using System.Windows.Forms;
using SpaceInvaders.ECS.Nodes;

namespace SpaceInvaders.ECS.Systems {
	public class PauseControlSystem : NodeIteratingSystem<GameNode> {
		private readonly HashSet<Keys> pressedKeys;

		public PauseControlSystem(HashSet<Keys> pressedKeys) : base(SystemOrder.AFTER, true) {
			this.pressedKeys = pressedKeys;
		}

		public override void UpdateNode(Engine engine, GameNode node, double dt) {
			if (node.Game.IsInGame && this.pressedKeys.Remove(Config.PauseControl)) {
				engine.ToggleSystemsHalt();
				node.Game.TogglePause();
			}
		}
	}
}
