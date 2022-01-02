namespace SpaceInvaders.ECS.Components {
	public class GameComponent : Component {
		public GameState GameState;
		public bool? PreviousIsVictory;

		public bool IsInGame {
			get { return this.GameState != GameState.MENU; }
		}

		public bool HasPrevious {
			get { return this.PreviousIsVictory != null; }
		}

		public GameComponent(GameState gameState, bool? previousIsVictory = null) {
			this.GameState = gameState;
			this.PreviousIsVictory = previousIsVictory;
		}

		public void TogglePause() {
			if (this.GameState == GameState.PAUSED)
				this.GameState = GameState.PLAYING;
			else if (this.GameState == GameState.PLAYING)
				this.GameState = GameState.PAUSED;
		}
	}

	public enum GameState {
		MENU,
		PLAYING,
		PAUSED
	}
}
