using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceInvaders.ECS.Components;
using SpaceInvaders.ECS.Nodes;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS.Entities {
	public class EntityCreator {
		public static readonly Dictionary<EntityType, List<Type/* ? extends Node */>> NodeRequirements = new Dictionary<EntityType, List<Type/* ? extends Node */>>();

		static EntityCreator() {
			NodeRequirements[EntityType.GAME] = new List<Type/* ? extends Node */>(new[] {
				typeof(GameNode)
			});
			NodeRequirements[EntityType.SPACESHIP] = new List<Type/* ? extends Node */>(new[] {
				typeof(RenderNode),
				typeof(SpaceshipCollisionNode),
				typeof(SpaceshipControlNode),
				typeof(SoulNode),
				typeof(ParentNode)
			});
			NodeRequirements[EntityType.MISSILE] = new List<Type/* ? extends Node */>(new[] {
				typeof(RenderNode),
				typeof(LinearMovementNode),
				typeof(MissileCollisionNode),
				typeof(SoulNode)
			});
			NodeRequirements[EntityType.BUNKER] = new List<Type/* ? extends Node */>(new[] {
				typeof(RenderNode),
				typeof(BunkerCollisionNode),
				typeof(SoulNode)
			});
			NodeRequirements[EntityType.ENEMY] = new List<Type/* ? extends Node */>(new[] {
				typeof(RenderNode),
				typeof(SpaceshipCollisionNode),
				typeof(EnemyShootNode),
				typeof(SoulNode),
				typeof(ParentNode)
			});
			NodeRequirements[EntityType.ENEMY_BLOCK] = new List<Type/* ? extends Node */>(new[] {
				typeof(EnemyMovementNode),
				typeof(ParentNode)
			});
		}

		public static Entity NewGame() {
			return new Entity(EntityType.GAME,
					new GameComponent(GameState.MENU),
					new HealthComponent());
		}

		public static Entity NewSpaceship(Vector2D position, int health, Bitmap display) {
			return new Entity(EntityType.SPACESHIP,
					new PositionComponent(position),
					new HealthComponent(health),
					new SizeComponent(display.Width, display.Height),
					new DisplayComponent(display),
					new SideComponent(EntitySide.ALLY),
					new ChildrenComponent());
		}

		public static Entity NewMissile(Vector2D position, int health, Bitmap display, Vector2D motion, EntitySide side) {
			return new Entity(EntityType.MISSILE,
					new PositionComponent(position),
					new HealthComponent(health),
					new SizeComponent(display.Width, display.Height),
					new DisplayComponent(display),
					new MotionComponent(motion),
					new SideComponent(side));
		}

		public static Entity NewBunker(Vector2D position, Bitmap display) {
			int health = 0;
			for (int x = 0; x < display.Width; x++)
				for (int y = 0; y < display.Height; y++)
					if (Util.IsBlack(display.GetPixel(x, y)))
						health++;
			return new Entity(EntityType.BUNKER,
					new PositionComponent(position),
					new HealthComponent(health),
					new SizeComponent(display.Width, display.Height),
					new DisplayComponent(display));
		}

		public static Entity NewEnemy(Vector2D position, int health, Bitmap display) {
			return new Entity(EntityType.ENEMY,
					new PositionComponent(position),
					new HealthComponent(health),
					new SizeComponent(display.Width, display.Height),
					new DisplayComponent(display),
					new SideComponent(EntitySide.ENEMY),
					new ChildrenComponent());
		}

		public static Entity NewEnemyBlock(Vector2D motion, List<Entity> enemies) {
			PositionComponent position = new PositionComponent();
			SizeComponent size = new SizeComponent();
			Util.ComputePositionSize(position, size, enemies);
			return new Entity(EntityType.ENEMY_BLOCK,
					position,
					size,
					new MotionComponent(motion),
					new ChildrenComponent(enemies, () => Util.ComputePositionSize(position, size, enemies)));
		}
	}
}
