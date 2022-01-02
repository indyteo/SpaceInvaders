using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceInvaders.ECS.Nodes;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Systems {
	public class CollisionSystem : NodeIteratingSystem<MissileCollisionNode> {
		private List<BunkerCollisionNode> bunkers;
		private List<SpaceshipCollisionNode> spaceships;

		public CollisionSystem() : base(SystemOrder.UPDATE) {}

		public override void Start(Engine engine) {
			base.Start(engine);
			this.bunkers = engine.GetNodes<BunkerCollisionNode>();
			this.spaceships = engine.GetNodes<SpaceshipCollisionNode>();
		}

		public override void UpdateNode(Engine engine, MissileCollisionNode missile, double dt) {
			foreach (MissileCollisionNode other in this.Nodes) {
            	if (other == missile || other.Side.Side == missile.Side.Side)
            		continue;
            	// Missile - Missile collision
            	if (BboxIntersect(missile, other)) {
            		missile.Health.Kill();
            		other.Health.Kill();
            	}
            }
            foreach (BunkerCollisionNode bunker in this.bunkers) {
            	// Missile - Bunker collision
            	if (BboxIntersect(missile, bunker)) {
            		int pixelCount = PixelIntersect(missile, bunker);
            		if (pixelCount > 0) {
            			missile.Health.Damage(pixelCount);
            			bunker.Health.Damage(pixelCount);
            		}
            	}
            }
            foreach (SpaceshipCollisionNode spaceship in this.spaceships) {
            	// Missile - Spaceship collision
            	if (missile.Side.Side != spaceship.Side.Side && BboxIntersect(missile, spaceship) && PixelIntersect(missile, spaceship) > 0) {
            		int damage = Math.Min(missile.Health.Health, spaceship.Health.Health);
            		missile.Health.Damage(damage);
            		spaceship.Health.Damage(damage);
            	}
            }
		}

		private bool BboxIntersect(CollisionNode first, CollisionNode second) {
			return BboxIntersect(
					first.Position.X, first.Position.Y, first.Position.X + first.Display.Width, first.Position.Y + first.Display.Height,
					second.Position.X, second.Position.Y, second.Position.X + second.Display.Width, second.Position.Y + second.Display.Height
			);
		}

		private bool BboxIntersect(double x1min, double y1min, double x1max, double y1max, double x2min, double y2min, double x2max, double y2max) {
			return x2max >= x1min && x2min < x1max && y2max >= y1min && y2min < y1max;
		}

		private int PixelIntersect(MissileCollisionNode missile, CollisionNode other) {
			int x = (int) (missile.Position.X - other.Position.X);
			int y = (int) (missile.Position.Y - other.Position.Y);
			return PixelIntersect(x, y, x + missile.Display.Width, y + missile.Display.Height, other.Display.Image);
		}

		private int PixelIntersect(int xmin, int ymin, int xmax, int ymax, Bitmap image) {
			int count = 0;
			for (int x = xmin; x < xmax; x++) {
				for (int y = ymin; y < ymax; y++) {
					if (x >= 0 && x < image.Width && y >= 0 && y < image.Height && Util.IsBlack(image.GetPixel(x, y))) {
						count++;
						image.SetPixel(x, y, Color.White);
					}
				}
			}
			return count;
		}
	}
}
