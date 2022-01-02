using System.Collections.Generic;

namespace SpaceInvaders.ECS.Components {
	public class ChildrenComponent : Component {
		public List<Entity> Children;
		public VoidCallback OnRemove;

		public bool HasAny {
			get { return this.Children.Count > 0; }
		}

		public bool HasNone {
			get { return this.Children.Count == 0; }
		}

		public ChildrenComponent(List<Entity> children = null, VoidCallback onRemove = null) {
			this.Children = children == null ? new List<Entity>() : children;
			this.OnRemove = onRemove == null ? () => {} : onRemove;
		}

		public void Add(Entity entity) {
			this.Children.Add(entity);
		}
	}
	
	public delegate void VoidCallback();
}
