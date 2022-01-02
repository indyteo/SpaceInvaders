using System;

namespace SpaceInvaders.Utils {
	public class Vector2D {
		public Vector2D(double x = 0, double y = 0) {
			this.X = x;
			this.Y = y;
		}

		public double X { get; private set; }
		public double Y { get; private set; }

		public double LengthSqr {
			get { return this.X * this.X + this.Y * this.Y; }
		}

		public double Length {
			get { return Math.Sqrt(this.LengthSqr); }
		}

		public Vector2D Normalize() {
			return this / this.Length;
		}

		public Vector2D Copy() {
			return new Vector2D(this.X, this.Y);
		}

		public static Vector2D operator+(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X + v2.X, v1.Y + v2.Y);
		}

		public static Vector2D operator-(Vector2D v1, Vector2D v2) {
			return new Vector2D(v1.X - v2.X, v1.Y - v2.Y);
		}

		public static Vector2D operator-(Vector2D v) {
			return new Vector2D(-v.X, -v.Y);
		}

		public static Vector2D operator*(Vector2D v, double d) {
			return new Vector2D(d * v.X, d * v.Y);
		}

		public static Vector2D operator*(double d, Vector2D v) {
			return v * d;
		}

		public static Vector2D operator/(Vector2D v, double d) {
			if (d == 0)
				throw new ArithmeticException("/ by 0");
			return new Vector2D(v.X / d, v.Y / d);
		}
	}
}
