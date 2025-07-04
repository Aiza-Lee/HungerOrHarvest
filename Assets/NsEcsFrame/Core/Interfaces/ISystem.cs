namespace NsEcsFrame.Core {
	/// <summary>
	/// 系统接口，所有系统都应实现此接口
	/// </summary>
	public interface ISystem {
		/// <summary>
		/// 初始化系统
		/// </summary>
		void Initialize(IWorld world);

		/// <summary>
		/// 当系统被创建时调用
		/// </summary>
		void OnCreate();

		/// <summary>
		/// 当系统被销毁时调用
		/// </summary>
		void OnDestroy();

		/// <summary>
		/// 逻辑更新调用，通常用于处理游戏逻辑
		/// </summary>
		void OnLogicUpdate(float deltaTime);

		/// <summary>
		/// 每帧更新调用
		/// </summary>
		void OnRenderUpdate(float deltaTime);

		/// <summary>
		/// 系统优先级，数字越小优先级越高
		/// </summary>
		int Priority { get; }

		/// <summary>
		/// 系统是否启用
		/// </summary>
		bool Enabled { get; set; }
	}
}
