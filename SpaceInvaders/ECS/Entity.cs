using SpaceInvaders.ECS.Entities;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS {
	public class Entity {
		public readonly int Id;
		public readonly EntityType Type;
		public readonly ClassMap<Component> Components = new ClassMap<Component>();

		private static int ids;

		public Entity(EntityType type, params Component[] components) {
			this.Id = ++ids;
			this.Type = type;
			foreach (Component component in components)
				this.Components.Add(component);
		}
	}
}
