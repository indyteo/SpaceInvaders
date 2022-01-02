using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders {
	/// <summary>
	///	    Basic data used by the game
	/// </summary>
	public class Config {
		/// Key to start game
		public static readonly Keys StartControl = Keys.Space;
		/// Key to shoot missile
		public static readonly Keys ShootControl = Keys.Space;
		/// Key to move left
		public static readonly Keys LeftControl = Keys.Left;
		/// Key to move right
		public static readonly Keys RightControl = Keys.Right;
		/// Key to toggle pause
		public static readonly Keys PauseControl = Keys.P;

		/// Missile image
		public static readonly Bitmap MissileImage = Properties.Resources.shoot1;
		/// Missile health
		public static readonly int MissileHealth = 10;
		/// Missile pixel per second move speed
		public static readonly double MissileSpeed = 250;

		/// Player spaceship left / right pixel per second move speed
		public static readonly double MoveSpeed = 150;
		/// Space between enemies' target and bottom of window
		public static readonly int TargetElevation = 100;

		/// Average missile per second per enemy shoot chance
		public static readonly double EnemyShootChance = 0.01;
		/// Extra average missile per second per enemy shoot chance
		public static readonly double EnemyShootChanceBonus = 0.02;
		/// Enemy spaceship left / right pixel per second move speed
		public static readonly double EnemySpeed = 50;
		/// Extra enemy spaceship left / right pixel per second move speed
		public static readonly double EnemySpeedBonus = 100;
		/// Pixel move when enemy spaceship move forward
		public static readonly double EnemyDrop = 5;
		/// Minimum space between enemies in enemy lines
		public static readonly double EnemySpacing = 15;

		/// Enemy level 1 image
		public static readonly Bitmap EnemyBasicImage = Properties.Resources.ship3;
		/// Enemy level 1 count
		public static readonly int EnemyBasicCount = 10;
		/// Enemy level 1 health
		public static readonly int EnemyBasicHealth = 10;

		/// Enemy level 2 image
		public static readonly Bitmap EnemySmallImage = Properties.Resources.ship7;
		/// Enemy level 2 count
		public static readonly int EnemySmallCount = 7;
		/// Enemy level 2 health
		public static readonly int EnemySmallHealth = 25;

		/// Enemy level 3 image
		public static readonly Bitmap EnemyMediumImage = Properties.Resources.ship2;
		/// Enemy level 3 count
		public static readonly int EnemyMediumCount = 5;
		/// Enemy level 3 health
		public static readonly int EnemyMediumHealth = 50;

		/// Enemy level 4 image
		public static readonly Bitmap EnemyLargeImage = Properties.Resources.ship4;
		/// Enemy level 4 count
		public static readonly int EnemyLargeCount = 3;
		/// Enemy level 4 health
		public static readonly int EnemyLargeHealth = 75;

		/// Player spaceship image
		public static readonly Bitmap SpaceshipImage = Properties.Resources.ship1;
		/// Space between player spaceship and bottom of window
		public static readonly int SpaceshipElevation = 10;
		/// Player spaceship health
		public static readonly int SpaceshipHealth = 50;

		/// Bunker image
		public static readonly Bitmap BunkerImage = Properties.Resources.bunker;
		/// Bunker count
		public static readonly int BunkerCount = 3;
		/// Space between bunkers and bottom of window
		public static readonly int BunkerElevation = 50;
	}
}
