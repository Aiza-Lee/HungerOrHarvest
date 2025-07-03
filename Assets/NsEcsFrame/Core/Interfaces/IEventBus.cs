using System;

namespace NsEcsFrame.Core {
	/// <summary>
	/// 事件总线接口，定义事件的发布和订阅操作
	/// </summary>
	public interface IEventBus {
		/// <summary>
		/// 订阅事件
		/// </summary>
		/// <typeparam name="T">事件类型</typeparam>
		/// <param name="handler">事件处理器</param>
		/// <returns>事件订阅，用于后续取消订阅</returns>
		EventSubscription Subscribe<T>(Action<T> handler) where T : class;

		/// <summary>
		/// 取消订阅事件
		/// </summary>
		/// <typeparam name="T">事件类型</typeparam>
		/// <param name="handler">事件处理器</param>
		void Unsubscribe<T>(Action<T> handler) where T : class;

		/// <summary>
		/// 取消订阅事件
		/// </summary>
		/// <param name="subscription">事件订阅</param>
		void Unsubscribe(EventSubscription subscription);

		/// <summary>
		/// 发布事件
		/// </summary>
		/// <typeparam name="T">事件类型</typeparam>
		/// <param name="eventData">事件数据</param>
		void Publish<T>(T eventData) where T : class;

		/// <summary>
		/// 清除所有订阅
		/// </summary>
		void Clear();
	}
}