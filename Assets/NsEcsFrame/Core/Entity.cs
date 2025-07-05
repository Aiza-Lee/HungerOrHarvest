namespace NsEcsFrame.Core {
	/// <summary>
	/// 实体类，作为组件的容器
	/// </summary>
	public class Entity {
		private readonly IComponentManager _componentManager;

		/// <summary>
		/// 实体唯一标识符
		/// </summary>
		public EntityId ID { get; }

		/// <summary>
		/// 实体是否启用
		/// </summary>
		public bool IsEnabled { get; set; } = true;

		/// <summary>
		/// 实体所属的世界
		/// </summary>
		public World World { get; }

		internal Entity(World world, EntityId id, IComponentManager componentManager) {
			World = world;
			ID = id;
			_componentManager = componentManager;
		}

		/// <summary>
		/// 添加组件
		/// </summary>
		/// <typeparam name="T">组件类型</typeparam>
		/// <returns>添加的组件</returns>
		public T AddComponent<T>() where T : class, IComponent, new() {
			return _componentManager.AddComponent<T>(ID);
		}

		/// <summary>
		/// 添加组件
		/// </summary>
		/// <typeparam name="T">组件类型</typeparam>
		/// <param name="component">组件实例</param>
		/// <returns>添加的组件</returns>
		public T AddComponent<T>(T component) where T : class, IComponent {
			return _componentManager.AddComponent(ID, component);
		}

		/// <summary>
		/// 移除组件
		/// </summary>
		/// <typeparam name="T">组件类型</typeparam>
		/// <returns>是否成功移除</returns>
		public bool RemoveComponent<T>() where T : class, IComponent {
			return _componentManager.RemoveComponent<T>(ID);
		}

		/// <summary>
		/// 获取组件
		/// </summary>
		/// <typeparam name="T">组件类型</typeparam>
		/// <returns>组件实例，如果不存在则返回null</returns>
		public T GetComponent<T>() where T : class, IComponent {
			return _componentManager.GetComponent<T>(ID);
		}

		/// <summary>
		/// 检查是否包含特定组件
		/// </summary>
		/// <typeparam name="T">组件类型</typeparam>
		/// <returns>是否包含该组件</returns>
		public bool HasComponent<T>() where T : class, IComponent {
			return _componentManager.HasComponent<T>(ID);
		}

		/// <summary>
		/// 检查实体是否有效
		/// </summary>
		/// <returns>实体是否有效</returns>
		public bool IsValid() {
			return ID.IsValid() && World.IsEntityAlive(ID);
		}

		/// <summary>
		/// 销毁实体
		/// </summary>
		public void Destroy() {
			World.DestroyEntity(ID);
		}

		public override string ToString() {
			return $"Entity({ID})";
		}
	}
}
