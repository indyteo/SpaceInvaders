using System;
using System.Reflection;

namespace SpaceInvaders.ECS {
	public class Node {
		public int Source { get; private set; }

		public void Initialize(Entity entity) {
			this.Source = entity.Id;
			Type/* ? extends Node */ type = this.GetType();
			FieldInfo[] fields = type.GetFields();
			foreach (FieldInfo field in fields) {
				Type/* ? extends Component */ componentType = field.FieldType;
				Component component = entity.Components.Get<Component>(componentType);
				field.SetValue(this, component);
			}
		}
	}
}
