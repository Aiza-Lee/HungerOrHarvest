namespace NsEcsFrame.Core {
	/// <summary>
	/// System管理器，负责System的注册、排序和更新
	/// </summary>
	public interface ISystemManager {
		/// <summary>
		/// 注册System
		/// </summary>
		/// <typeparam name="T">System类型</typeparam>
		/// <returns>注册的System实例</returns>
		T RegisterSystem<T>() where T : class, ISystem, new();

		/// <summary>
		/// 注册System
		/// </summary>
		/// <typeparam name="T">System类型</typeparam>
		/// <param name="system">System实例</param>
		/// <returns>注册的System实例</returns>
		T RegisterSystem<T>(T system) where T : class, ISystem;

		/// <summary>
		/// 获取指定类型的System
		/// </summary>
		/// <typeparam name="T">System类型</typeparam>
		T GetSystem<T>() where T : class, ISystem;

		/// <summary>
		/// 设置System的启用状态
		/// </summary>
		/// <typeparam name="T">System类型</typeparam>
		/// <param name="enabled">是否启用</param>
		void SetSystemEnabled<T>(bool enabled) where T : class, ISystem;

		/// <summary>
		/// 触发所有启用的System的LogicUpdate
		/// </summary>
		void LogicUpdate(float deltaTime);

		/// <summary>
		/// 触发所有启用的System的RenderUpdate
		/// </summary>
		void RenderUpdate(float deltaTime);

		/// <summary>
		/// 销毁所有System
		/// </summary>
		void DestroyAllSystems();

	}
}