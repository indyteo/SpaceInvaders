using System;
using System.Collections.Generic;

namespace SpaceInvaders.Utils {
	public class ClassMap<T> where T : class {
		private Dictionary<Type/* ? extends T */, T> values = new Dictionary<Type/* ? extends T */, T>();

		public U Get<U>(Type/* U */ type = null) where U : class, T {
			return this.values[type == null ? typeof(U) : type] as U;
		}

		public void Add(T value) {
			this.values[value.GetType()] = value;
		}

		public void Remove<U>(Type/* U */ type = null) where U : T {
			this.values.Remove(type == null ? typeof(U) : type);
		}
	}
}
