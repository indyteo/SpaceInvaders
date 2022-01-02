using SpaceInvaders.ECS.Entities;

namespace SpaceInvaders.ECS.Components {
	public class SideComponent : Component {
		public EntitySide Side;

		public SideComponent(EntitySide side) {
			this.Side = side;
		}
	}
}
