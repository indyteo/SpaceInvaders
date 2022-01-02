namespace SpaceInvaders.ECS.Components {
	public class HealthComponent : Component {
		public int Health;

		public bool IsAlive {
			get { return this.Health > 0; }
		}

		public HealthComponent(int health = 0) {
			this.Health = health;
		}

		public void Damage(int amout) {
			this.Health -= amout;
		}

		public void Kill() {
			this.Health = 0;
		}
	}
}
