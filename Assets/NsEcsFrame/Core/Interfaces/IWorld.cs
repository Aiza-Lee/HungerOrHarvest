using System.Collections.Generic;

namespace NsEcsFrame.Core {
	/// <summary>
	/// ECS世界，是整个ECS框架的入口点
	/// </summary>
	public interface IWorld {
		/// <summary>
		/// 世界的名称
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 是否启用调试日志
		/// </summary>
		bool EnableDebugLogs { get; set; }

		/// <summary>
		/// 获取Component管理器
		/// </summary>
		IComponentManager ComponentManager { get; }
		/// <summary>
		/// 获取System管理器
		/// </summary>
		ISystemManager SystemManager { get; }
		/// <summary>
		/// 获取事件总线
		/// </summary>
		IEventBus EventBus { get; }

		/// <summary>
		/// 创建新的实体
		/// </summary>
		/// <returns>创建的实体</returns>
		Entity CreateEntity();

		/// <summary>
		/// 销毁实体
		/// </summary>
		/// <param name="entityId">要销毁的实体ID</param>
		void DestroyEntity(EntityId entityId);

		/// <summary>
		/// 检查实体是否存在
		/// </summary>
		/// <param name="entityId">实体ID</param>
		/// <returns>实体是否存在</returns>
		bool IsEntityAlive(EntityId entityId);

		/// <summary>
		/// 根据ID获取实体
		/// </summary>
		/// <param name="entityId">实体ID</param>
		/// <returns>实体对象，如果不存在则返回null</returns>
		Entity GetEntity(EntityId entityId);

		/// <summary>
		/// 移除所有实体
		/// </summary>
		void DestroyAllEntities();

		/// <summary>
		/// 获取所有实体
		/// </summary>
		/// <returns>实体集合</returns>
		IReadOnlyCollection<Entity> GetAllEntities();

		/// <summary>
		/// 获取实体数量
		/// </summary>
		int EntityCount { get; }

		/// <summary>
		/// 逻辑更新世界，调用所有System的逻辑更新方法
		/// </summary>
		/// <param name="deltaTime"></param>
		void LogicUpdate(float deltaTime);

		/// <summary>
		/// 渲染更新世界，调用所有System的更新方法
		/// </summary>
		/// <param name="deltaTime">时间增量</param>
		void RenderUpdate(float deltaTime);

		/// <summary>
		/// 销毁世界，清理所有资源
		/// </summary>
		void Destroy();

		/// <summary>
		/// 创建查询构建器
		/// </summary>
		/// <returns>查询构建器</returns>
		EntityQueryBuilder CreateQueryBuilder();

		/// <summary>
		/// 添加/覆盖单例组件
		/// </summary>
		IWorld InsertResource<T>(T resource) where T : class, IResource;

		/// <summary>
		/// 删除单例组件
		/// </summary>
		bool RemoveResource<T>() where T : class, IResource;

		/// <summary>
		/// 获取单例组件
		/// </summary>
		T GetResource<T>() where T : class, IResource;

		/// <summary>
		/// 尝试获取单例组件,
		/// 如果不存在则返回false，resource为null
		/// </summary>
		bool TryGetResource<T>(out T resource) where T : class, IResource;

		/// <summary>
		/// 检查是否存在单例组件
		/// </summary>
		bool HasResource<T>() where T : class, IResource;

		/// <summary>
		/// 获取所有已注册的单例组件（Resource）对象
		/// </summary>
		IEnumerable<IResource> GetAllResources();
	}
}