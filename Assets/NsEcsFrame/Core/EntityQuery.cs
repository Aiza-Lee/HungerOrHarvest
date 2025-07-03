using System;
using System.Collections.Generic;

namespace NsEcsFrame.Core {
	/// <summary>
	/// Entity查询结果，用于执行对查询结果的操作
	/// </summary>
	public class EntityQuery {
		private readonly List<Entity> _entities;
		private readonly World _world;

		internal EntityQuery(World world, List<Entity> entities) {
			_world = world;
			_entities = entities;
		}

		/// <summary>
		/// 获取符合查询条件的Entity数量
		/// </summary>
		public int Count => _entities.Count;

		/// <summary>
		/// 获取所有符合查询条件的Entity
		/// </summary>
		/// <returns>Entity数组</returns>
		public Entity[] GetEntities() {
			return _entities.ToArray();
		}

		/// <summary>
		/// 对查询结果中的每个Entity执行操作
		/// </summary>
		/// <param name="action">要执行的操作</param>
		public void ForEach(Action<Entity> action) {
			foreach (var entity in _entities) {
				if (entity.IsEnabled && entity.IsValid()) {
					action(entity);
				}
			}
		}

		/// <summary>
		/// 对查询结果中的每个Entity及其特定Component执行操作
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <param name="action">要执行的操作</param>
		public void ForEach<T>(Action<Entity, T> action) where T : class, IComponent {
			foreach (var entity in _entities) {
				if (!entity.IsEnabled || !entity.IsValid())
					continue;

				T component = entity.GetComponent<T>();
				if (component != null) {
					action(entity, component);
				}
			}
		}

		/// <summary>
		/// 对查询结果中的每个Entity及其多个特定Component执行操作
		/// </summary>
		/// <typeparam name="T1">第一个Component类型</typeparam>
		/// <typeparam name="T2">第二个Component类型</typeparam>
		/// <param name="action">要执行的操作</param>
		public void ForEach<T1, T2>(Action<Entity, T1, T2> action)
			where T1 : class, IComponent
			where T2 : class, IComponent {
			foreach (var entity in _entities) {
				if (!entity.IsEnabled || !entity.IsValid())
					continue;

				T1 component1 = entity.GetComponent<T1>();
				if (component1 == null) continue;

				T2 component2 = entity.GetComponent<T2>();
				if (component2 == null) continue;

				action(entity, component1, component2);
			}
		}

		/// <summary>
		/// 对查询结果中的每个Entity及其多个特定Component执行操作
		/// </summary>
		/// <typeparam name="T1">第一个Component类型</typeparam>
		/// <typeparam name="T2">第二个Component类型</typeparam>
		/// <typeparam name="T3">第三个Component类型</typeparam>
		/// <param name="action">要执行的操作</param>
		public void ForEach<T1, T2, T3>(Action<Entity, T1, T2, T3> action)
			where T1 : class, IComponent
			where T2 : class, IComponent
			where T3 : class, IComponent {
			foreach (var entity in _entities) {
				if (!entity.IsEnabled || !entity.IsValid())
					continue;

				T1 component1 = entity.GetComponent<T1>();
				if (component1 == null) continue;

				T2 component2 = entity.GetComponent<T2>();
				if (component2 == null) continue;

				T3 component3 = entity.GetComponent<T3>();
				if (component3 == null) continue;

				action(entity, component1, component2, component3);
			}
		}
	}

	
}
