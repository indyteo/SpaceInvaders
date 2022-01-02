using System;
using System.Collections.Generic;

namespace SpaceInvaders.Utils {
	public class ClassListMap<T> {
		private Dictionary<Type/* ? extends T */, object /* List<? extends T> */> values = new Dictionary<Type/* ? extends T */, object /* List<? extends T> */>();

		public List<U> Get<U>(Type type = null) where U : T {
			if (type == null)
				type = typeof(U);
			List<U> list;
			object value;
			if (this.values.TryGetValue(type, out value))
				list = (List<U>) value;
			else {
				list = (List<U>) Activator.CreateInstance(typeof(List<>).MakeGenericType(type));
				this.values[type] = list;
			}
			return list;
		}

		public void Add<U>(U value) where U : T {
			this.Get<U>(value.GetType()).Add(value);
		}

		public void Remove<U>(U value) where U : T {
			((List<U>) this.values[value.GetType()]).Remove(value);
		}
	}
}
