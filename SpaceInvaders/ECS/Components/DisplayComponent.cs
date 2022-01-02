using System.Drawing;

namespace SpaceInvaders.ECS.Components {
	public class DisplayComponent : Component {
		public Bitmap Image;

		public int Width {
			get { return this.Image.Width; }
		}

		public int Height {
			get { return this.Image.Height; }
		}

		public DisplayComponent(Bitmap image) {
			this.Image = (Bitmap) image.Clone();
		}
	}
}
