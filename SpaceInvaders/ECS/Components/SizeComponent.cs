namespace SpaceInvaders.ECS.Components {
	public class SizeComponent : Component {
		public double Width;
		public double Height;

		public SizeComponent(double width = 0, double height = 0) {
			this.Width = width;
			this.Height = height;
		}
	}
}
