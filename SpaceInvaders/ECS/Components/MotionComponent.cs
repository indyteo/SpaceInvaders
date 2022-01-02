using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Components {
	public class MotionComponent : Component {
		public Vector2D Motion;

		public MotionComponent(Vector2D motion) {
			this.Motion = motion;
		}
	}
}
