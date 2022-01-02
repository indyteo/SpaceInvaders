using SpaceInvaders.ECS.Nodes;

namespace SpaceInvaders.ECS.Systems {
	public class ChildrenGraveyardSystem : NodeIteratingSystem<ParentNode> {
		public ChildrenGraveyardSystem() : base(SystemOrder.BEFORE) {}

		public override void UpdateNode(Engine engine, ParentNode node, double dt) {
			if (node.Children.Children.RemoveAll(entity => engine.GetEntity(entity.Id) == null) > 0)
				node.Children.OnRemove();
		}
	}
}
