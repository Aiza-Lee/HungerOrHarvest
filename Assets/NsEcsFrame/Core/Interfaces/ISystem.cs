namespace NsEcsFrame.Core {
	/// <summary>
	/// 系统接口，所有系统都应实现此接口
	/// </summary>
	public interface ISystem {
		/// <summary>
		/// 当系统被创建时调用
		/// </summary>
		void OnCreate();

		/// <summary>
		/// 当系统被销毁时调用
		/// </summary>
		void OnDestroy();

		/// <summary>
		/// 每帧更新调用
		/// </summary>
		void OnUpdate(float deltaTime);

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
