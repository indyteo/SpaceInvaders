using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Components {
	public class PositionComponent : Component {
		public Vector2D Position;

		public double X {
			get { return this.Position.X; }
			set { this.Position = new Vector2D(value, this.Y); }
		}

		public double Y {
			get { return this.Position.Y; }
			set { this.Position = new Vector2D(this.X, value); }
		}

		public PositionComponent(Vector2D position = null) {
			this.Position = position == null ? new Vector2D() : position;
		}
	}
}
