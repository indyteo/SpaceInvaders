using System.Collections.Generic;

namespace SpaceInvaders.ECS {
	public abstract class System {
		public readonly SystemOrder Order;
		public readonly bool IgnoreEngineHalt;

		public System(SystemOrder order, bool ignoreEngineHalt = false) {
			this.Order = order;
			this.IgnoreEngineHalt = ignoreEngineHalt;
		}

		public virtual void Start(Engine engine) {}
		
		public abstract void Update(Engine engine, double dt);

		public virtual void End(Engine engine) {}
	}

	public abstract class NodeIteratingSystem<T> : System where T : Node {
		protected List<T> Nodes { get; private set; }

		public NodeIteratingSystem(SystemOrder order, bool ignoreEngineHalt = false) : base(order, ignoreEngineHalt) {}

		public override void Start(Engine engine) {
			this.Nodes = engine.GetNodes<T>();
		}

		public override void Update(Engine engine, double dt) {
			foreach (T node in this.Nodes)
				this.UpdateNode(engine, node, dt);
		}

		public abstract void UpdateNode(Engine engine, T node, double dt);
	}

	public enum SystemOrder {
		BEFORE,
		PRE_UPDATE,
		UPDATE,
		POST_UPDATE,
		BETWEEN,
		PRE_RENDER,
		RENDER,
		POST_RENDER,
		AFTER
	}

	public class SystemComparer : IComparer<System> {
		public int Compare(System x, System y) {
			if (x == null)
				return y == null ? 0 : -1;
			if (y == null)
				return 1;
			return x.Order == y.Order ? 1 : x.Order - y.Order;
		}
	}
}
