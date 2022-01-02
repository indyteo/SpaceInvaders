using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SpaceInvaders.ECS;
using SpaceInvaders.ECS.Entities;
using SpaceInvaders.ECS.Systems;

namespace SpaceInvaders {
	public class SpaceInvaders {
		/// <summary>
		///     The game engine (ECS pattern)
		/// </summary>
		private Engine engine;

		/// <summary>
		///     State of the keyboard
		/// </summary>
		private HashSet<Keys> pressedKeys = new HashSet<Keys>();

		/// <summary>
		///     Create the game engine, systems and game entity
		/// </summary>
		/// <param name="gameSize">The window size</param>
		/// <param name="graphics">The graphics used to draw</param>
		public SpaceInvaders(Size gameSize, Graphics graphics) {
			int targetY = gameSize.Height - Config.TargetElevation;
			this.engine = new Engine()
					.AddSystem(new GameControlSystem(this.pressedKeys, gameSize, targetY))
					.AddSystem(new ChildrenGraveyardSystem())
					.AddSystem(new SpaceshipControlSystem(this.pressedKeys, gameSize))
					.AddSystem(new LinearMovementSystem(gameSize))
					.AddSystem(new EnemyMovementSystem(gameSize, targetY))
					.AddSystem(new CollisionSystem())
					.AddSystem(new EnemyShootSystem(targetY))
					.AddSystem(new RenderSystem(graphics))
					.AddSystem(new GameOverlaySystem(graphics, gameSize))
					.AddSystem(new GrimReaperSystem())
					.AddSystem(new PauseControlSystem(this.pressedKeys));
			this.engine.AddEntity(EntityCreator.NewGame());
		}

		/// <summary>
		///	    Update the game engine
		/// </summary>
		/// <param name="dt">Elapsed time</param>
		public void Update(double dt) {
			this.engine.Update(dt);
		}

		/// <summary>
		///	    Add a key to the pressed keys list
		/// </summary>
		/// <param name="key">The pressed key</param>
		public void PressKey(Keys key) {
			this.pressedKeys.Add(key);
		}

		/// <summary>
		///     Remove a key from the pressed keys list
		/// </summary>
		/// <param name="key">The released key</param>
		public void ReleaseKey(Keys key) {
			this.pressedKeys.Remove(key);
		}
	}
}
