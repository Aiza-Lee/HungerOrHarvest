using NsEcsFrame.Core;

namespace NsEcsFrame.Systems {
	/// <summary>
	/// 系统的基础实现，提供常用功能
	/// </summary>
	public abstract class BaseSystem : ISystem {
		/// <summary>
		/// 系统所属世界
		/// </summary>
		protected IWorld World { get; private set; }

		/// <summary>
		/// 系统优先级，数字越小优先级越高
		/// </summary>
		public virtual int Priority { get; protected set; } = 0;

		/// <summary>
		/// 系统是否启用
		/// </summary>
		public bool Enabled { get; set; } = true;

		/// <summary>
		/// 初始化系统
		/// </summary>
		/// <param name="world">所属世界</param>
		public void Initialize(IWorld world) {
			World = world;
		}

		/// <summary>
		/// 当系统被创建时调用
		/// </summary>
		public virtual void OnCreate() {
			// 默认为空，子类可以覆盖此方法
		}

		/// <summary>
		/// 当系统被销毁时调用
		/// </summary>
		public virtual void OnDestroy() {
			// 默认为空，子类可以覆盖此方法
		}

		/// <summary>
		/// 每帧更新调用
		/// </summary>
		public abstract void OnUpdate(float deltaTime);
	}
}
