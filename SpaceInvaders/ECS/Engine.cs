using System;
using System.Collections.Generic;
using SpaceInvaders.ECS.Entities;
using SpaceInvaders.Utils;

namespace SpaceInvaders.ECS {
	public class Engine {
		/// <summary>
		///     List of entities, indexed by their IDs
		/// </summary>
		private readonly Dictionary<int, Entity> entities = new Dictionary<int, Entity>();
		/// <summary>
		///     List of systems, sorted by their order
		/// </summary>
		private readonly SortedSet<System> systems = new SortedSet<System>(new SystemComparer());
		/// <summary>
		///     List of nodes, grouped by their types
		/// </summary>
		private readonly ClassListMap<Node> nodes = new ClassListMap<Node>();

		/// <summary>
		///     Entities to be added in the next update
		/// </summary>
		private readonly List<Entity> entitiesToAdd = new List<Entity>();
		/// <summary>
		///     Entities to be removed at the end of the update
		/// </summary>
		private readonly List<Entity> entitiesToRemove = new List<Entity>();

		/// <summary>
		///     Whether the engine is running or halted
		/// </summary>
		public bool IsRunning { get; private set; }
		private bool NextRunningState;

		/// <summary>
		///     Create a new empty running engine
		/// </summary>
		public Engine() {
			this.IsRunning = this.NextRunningState = true;
		}

		/// <summary>
		///     Add an entity to the engine, constructing and adding its nodes at the next update
		/// </summary>
		/// <param name="entity">The entity to add</param>
		public void AddEntity(Entity entity) {
			this.entities[entity.Id] = entity;
			this.entitiesToAdd.Add(entity);
		}

		/// <summary>
		///     Retrieve an entity, if it exists, null otherwise
		/// </summary>
		/// <param name="id">The ID of the entity</param>
		/// <returns>The entity if added to the engine, null otherwise</returns>
		public Entity GetEntity(int id) {
			try {
				return this.entities[id];
			} catch (KeyNotFoundException) {
				return null;
			}
		}

		/// <summary>
		///     Remove an entity from the engine, removing its nodes an the end of the update
		/// </summary>
		/// <param name="id">The ID of the entity to remove</param>
		public void RemoveEntity(int id) {
			this.RemoveEntity(this.GetEntity(id));
		}

		/// <summary>
		///     Remove an entity from the engine, removing its nodes an the end of the update
		/// </summary>
		/// <param name="entity">The entity to remove</param>
		public void RemoveEntity(Entity entity) {
			if (entity == null)
				return;
			this.entities.Remove(entity.Id);
			this.entitiesToRemove.Add(entity);
		}

		/// <summary>
		///     Remove all entities of given types
		/// </summary>
		/// <param name="types">The types of the entities to clean up</param>
		public void CleanUpEntities(params EntityType[] types) {
			foreach (EntityType type in types)
				foreach (Entity entity in new List<Entity>(this.entities.Values))
                	if (type == entity.Type)
                		this.RemoveEntity(entity);
		}

		/// <summary>
		///     Add a system and start it
		/// </summary>
		/// <param name="system">The system to add</param>
		/// <returns>this, to allow chained calls</returns>
		public Engine AddSystem(System system) {
			this.systems.Add(system);
			system.Start(this);
			return this;
		}

		/// <summary>
		///     Halt systems (pause engine)
		/// </summary>
		public void HaltSystems() {
			this.NextRunningState = false;
		}

		/// <summary>
		///     Toggle systems halt
		/// </summary>
		public void ToggleSystemsHalt() {
			this.NextRunningState = !this.IsRunning;
		}

		/// <summary>
		///     Remuse systems (un-pause engine)
		/// </summary>
		public void ResumeSystems() {
			this.NextRunningState = true;
		}

		/// <summary>
		///     End a system and remove it
		/// </summary>
		/// <param name="system">The system to remove</param>
		public void RemoveSystem(System system) {
			system.End(this);
			this.systems.Remove(system);
		}

		/// <summary>
		///     Construct and add nodes from an entity
		/// </summary>
		/// <param name="entity">The entity from which to add nodes</param>
		private void AddNodes(Entity entity) {
			foreach (Type/* ? extends Node */ nodeRequirement in EntityCreator.NodeRequirements[entity.Type]) {
	            Node node = Activator.CreateInstance(nodeRequirement) as Node;
	            if (node != null) {
            		node.Initialize(entity);
            		this.nodes.Add(node);
	            }
			}
		}

		/// <summary>
		///     Get a node list, linked to the engine list
		/// </summary>
		/// <typeparam name="T">The type of the nodes in the list</typeparam>
		/// <returns>The node list</returns>
		public List<T> GetNodes<T>() where T : Node {
			return this.nodes.Get<T>();
		}

		/// <summary>
		///     Remove nodes from an entity
		/// </summary>
		/// <param name="entity">The entity from which remove nodes</param>
		private void RemoveNodes(Entity entity) {
			foreach (Type/* ? extends Node */ nodeRequirement in EntityCreator.NodeRequirements[entity.Type])
				this.nodes.Remove(nodeRequirement, node => node.Source == entity.Id);
		}

		/// <summary>
		///     Update the engine and all systems
		/// </summary>
		/// <param name="dt">The elapsed time</param>
		public void Update(double dt) {
			// Add pending entities
			this.entitiesToAdd.ForEach(this.AddNodes);
			this.entitiesToAdd.Clear();

			// Run systems
			foreach (System system in this.systems)
				if (this.IsRunning || system.IgnoreEngineHalt)
					system.Update(this, dt);

			// Remove pending entities
			this.entitiesToRemove.ForEach(this.RemoveNodes);
			this.entitiesToRemove.Clear();

			// Validate running state
			this.IsRunning = this.NextRunningState;
		}
	}
}
