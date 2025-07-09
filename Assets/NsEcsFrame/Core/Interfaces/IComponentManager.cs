using System;
using System.Collections.Generic;

namespace NsEcsFrame.Core {
	/// <summary>
	/// Component管理器，负责Component的存储、添加、删除和获取
	/// </summary>
	public interface IComponentManager {
		/// <summary>
		/// 注册Component类型
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		void RegisterComponentType<T>() where T : class, IComponent, new();

		/// <summary>
		/// 添加Component
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <param name="entityId">EntityID</param>
		/// <returns>添加的Component</returns>
		T AddComponent<T>(EntityId entityId) where T : class, IComponent, new();

		/// <summary>
		/// 添加Component
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <param name="entityId">EntityID</param>
		/// <param name="component">Component实例</param>
		/// <returns>添加的Component</returns>
		T AddComponent<T>(EntityId entityId, T component) where T : class, IComponent;

		/// <summary>
		/// 移除Component
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <param name="entityId">EntityID</param>
		/// <returns>是否成功移除</returns>
		bool RemoveComponent<T>(EntityId entityId) where T : class, IComponent;

		/// <summary>
		/// 获取Component
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <param name="entityId">EntityID</param>
		/// <returns>Component实例，如果不存在则返回null</returns>
		T GetComponent<T>(EntityId entityId) where T : class, IComponent;

		/// <summary>
		/// 检查Entity是否包含特定Component
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <param name="entityId">EntityID</param>
		/// <returns>是否包含该Component</returns>
		bool HasComponent<T>(EntityId entityId) where T : class, IComponent;

		/// <summary>
		/// 检查Entity是否包含特定Component（通过Type参数）
		/// </summary>
		/// <param name="entityId">EntityID</param>
		/// <param name="componentType">Component类型</param>
		/// <returns>是否包含该Component</returns>
		bool HasComponent(EntityId entityId, Type componentType);

		/// <summary>
		/// 获取Entity的所有Component类型
		/// </summary>
		/// <param name="entityId">EntityID</param>
		/// <returns>Component类型集合</returns>
		IReadOnlyCollection<Type> GetComponentTypes(EntityId entityId);

		/// <summary>
		/// 清除Entity的所有Component
		/// </summary>
		/// <param name="entityId">EntityID</param>
		void RemoveAllComponents(EntityId entityId);

		/// <summary>
		/// 获取拥有特定Component类型的所有Entity
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <returns>EntityID集合</returns>
		IReadOnlyCollection<EntityId> GetEntitiesWith<T>() where T : class, IComponent;

		/// <summary>
		/// 获取拥有特定Component类型的所有EntityId（通过Type参数）
		/// </summary>
		/// <param name="componentType">Component类型</param>
		/// <returns>EntityID集合</returns>
		IReadOnlyCollection<EntityId> GetEntitiesWith(Type componentType);

		/// <summary>
		/// 获取所有指定类型的Component实例集合
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <returns>Component实例集合</returns>
		IReadOnlyCollection<T> GetAllComponents<T>() where T : class, IComponent;

		/// <summary>
		/// 获取所有指定类型的Component实例集合（通过Type参数）
		/// </summary>
		/// <param name="componentType">Component类型</param>
		/// <returns>Component实例集合</returns>
		IReadOnlyCollection<IComponent> GetAllComponents(Type componentType);

		/// <summary>
		/// 获取Entity的所有Component实例集合
		/// <para>性能较差，慎用</para>
		/// </summary>
		/// <param name="ID">实例的ID</param>
		/// <returns>Component集合</returns>
		IReadOnlyCollection<IComponent> GetAllComponents(EntityId ID);
	}
}