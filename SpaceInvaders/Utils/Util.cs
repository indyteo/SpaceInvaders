using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceInvaders.ECS;
using SpaceInvaders.ECS.Components;

namespace SpaceInvaders.Utils {
	public class Util {
		public static readonly Brush BLACK_BRUSH = new SolidBrush(Color.Black);
		public static readonly Font FONT = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);

		public static bool IsBlack(Color color) {
			return color.R == 0 && color.G == 0 && color.B == 0;
		}

		public static double MinMax(double min, double val, double max) {
			return Math.Max(min, Math.Min(val, max));
		}

		public static int Sign(double x) {
			return x < 0 ? -1 : x > 0 ? 1 : 0;
		}

		public static void ComputePositionSize(PositionComponent position, SizeComponent size, List<Entity> entities) {
			double minX = Double.PositiveInfinity, minY = Double.PositiveInfinity, maxX = Double.NegativeInfinity, maxY = Double.NegativeInfinity;
			foreach (Entity entity in entities) {
				PositionComponent pos = entity.Components.Get<PositionComponent>();
				SizeComponent s = entity.Components.Get<SizeComponent>();
				if (pos.X < minX)
					minX = pos.X;
				if (pos.X + s.Width > maxX)
					maxX = pos.X + s.Width;
				if (pos.Y < minY)
					minY = pos.Y;
				if (pos.Y + s.Height > maxY)
					maxY = pos.Y + s.Height;
			}
			position.Position = new Vector2D(minX, minY);
			size.Width = maxX - minX;
			size.Height = maxY - minY;
		}
	}
}
