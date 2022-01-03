using System;
using System.Collections.Generic;
using System.Reflection;

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

		public void Add(T value) {
			Type type = value.GetType();
			Type listType = typeof(List<>).MakeGenericType(type);
			object list;
			if (!this.values.TryGetValue(type, out list))
				this.values[type] = list = Activator.CreateInstance(listType);

			MethodInfo method = listType.GetMethod("Add", new [] { type });
			if (method != null)
				method.Invoke(list, new object[] { value });
		}

		public void Remove(T value) {
			Type type = value.GetType();
			Type listType = typeof(List<>).MakeGenericType(type);
			object list;
			if (this.values.TryGetValue(type, out list)) {
				MethodInfo method = listType.GetMethod("Remove", new [] { type });
                if (method != null)
                	method.Invoke(list, new object[] { value });
			}
		}

		public void Remove(Type/* ? extends T */ type, Predicate<T> condition) {
			Type listType = typeof(List<>).MakeGenericType(type);
			object list;
			if (this.values.TryGetValue(type, out list)) {
				MethodInfo method = listType.GetMethod("RemoveAll", new [] { typeof(Predicate<>).MakeGenericType(type) });
				if (method != null)
					method.Invoke(list, new object[] { condition });
			}
		}
	}
}
