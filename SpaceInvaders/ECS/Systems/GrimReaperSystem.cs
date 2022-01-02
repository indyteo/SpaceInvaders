using SpaceInvaders.ECS.Nodes;

namespace SpaceInvaders.ECS.Systems {
	public class GrimReaperSystem : NodeIteratingSystem<SoulNode> {
		public GrimReaperSystem() : base(SystemOrder.AFTER) {}

		public override void UpdateNode(Engine engine, SoulNode node, double dt) {
			if (!node.Health.IsAlive)
				engine.RemoveEntity(node.Source);
		}
	}
}
