using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SpaceInvaders {
	public partial class GameForm : Form {
		/// <summary>
		///     The background image where everything is displayed (canvas)
		/// </summary>
		private Bitmap background;

		/// <summary>
		///     The graphics from the background image
		/// </summary>
		private Graphics graphics;

        /// <summary>
        ///     Create form, create game
        /// </summary>
        public GameForm() {
			this.InitializeComponent();
			this.background = new Bitmap(this.ClientSize.Width, this.ClientSize.Height, PixelFormat.Format32bppArgb);
			this.graphics = Graphics.FromImage(this.background);
			this.game = new SpaceInvaders(this.ClientSize, this.graphics);
			this.watch.Start();
			this.WorldClock.Start();
		}

        /// <summary>
        ///     Instance of the game
        /// </summary>
        private readonly SpaceInvaders game;

        /// <summary>
        ///     Game watch
        /// </summary>
        private readonly Stopwatch watch = new Stopwatch();

        /// <summary>
        ///     Last update time
        /// </summary>
        private long lastTime;

        /// <summary>
        ///     Paint event of the form, see msdn for help => paint game with double buffering
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_Paint(object sender, PaintEventArgs e) {
	        BufferedGraphics bg = BufferedGraphicsManager.Current.Allocate(e.Graphics, e.ClipRectangle);
	        Graphics g = bg.Graphics;
	        g.Clear(Color.White);

	        g.DrawImage(this.background, 0, 0, this.ClientSize.Width, this.ClientSize.Height);

	        bg.Render();
	        bg.Dispose();
        }

        /// <summary>
        ///     Tick event => update game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorldClock_Tick(object sender, EventArgs e) {
	        this.graphics.Clear(Color.White);
			// lets do 5 ms update to avoid quantum effects
			var maxDelta = 5;

			// get time with millisecond precision
			long nt = this.watch.ElapsedMilliseconds;
			// compute ellapsed time since last call to update
			double deltaT = nt - this.lastTime;

			for (; deltaT >= maxDelta; deltaT -= maxDelta)
				this.game.Update(maxDelta / 1000.0);
			this.game.Update(deltaT / 1000.0);

			// remember the time of this update
			this.lastTime = nt;

			this.Invalidate();
		}

        /// <summary>
        ///     Key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyDown(object sender, KeyEventArgs e) {
			this.game.PressKey(e.KeyCode);
		}

        /// <summary>
        ///     Key up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameForm_KeyUp(object sender, KeyEventArgs e) {
			this.game.ReleaseKey(e.KeyCode);
		}
	}
}
