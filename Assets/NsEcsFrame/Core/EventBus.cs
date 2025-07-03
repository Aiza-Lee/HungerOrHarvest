using System;
using System.Collections.Generic;

namespace NsEcsFrame.Core {
	public class EventBus : IEventBus {
		private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

		public EventSubscription Subscribe<T>(Action<T> handler) where T : class {
			Type eventType = typeof(T);

			if (!_subscribers.TryGetValue(eventType, out var handlers)) {
				handlers = new List<Delegate>();
				_subscribers[eventType] = handlers;
			}

			handlers.Add(handler);

			return new EventSubscription(this, eventType, handler);
		}

		public void Unsubscribe<T>(Action<T> handler) where T : class {
			Type eventType = typeof(T);

			if (_subscribers.TryGetValue(eventType, out var handlers)) {
				handlers.Remove(handler);
				if (handlers.Count == 0) {
					_subscribers.Remove(eventType);
				}
			}
		}

		public void Unsubscribe(EventSubscription subscription) {
			if (subscription == null) return;

			Type eventType = subscription.EventType;
			Delegate handler = subscription.Handler;

			if (_subscribers.TryGetValue(eventType, out var handlers)) {
				handlers.Remove(handler);
				if (handlers.Count == 0) {
					_subscribers.Remove(eventType);
				}
			}

			subscription.Invalidate();
		}

		public void Publish<T>(T eventData) where T : class {
			Type eventType = typeof(T);

			if (_subscribers.TryGetValue(eventType, out var handlers)) {
				// 创建副本，避免处理过程中修改集合
				var handlersCopy = new List<Delegate>(handlers);

				foreach (var handler in handlersCopy) {
					if (handler is Action<T> typedHandler) {
						typedHandler(eventData);
					}
				}
			}
		}

		public void Clear() {
			_subscribers.Clear();
		}
	}

	/// <summary>
	/// 事件订阅，表示一个活跃的事件订阅
	/// </summary>
	public class EventSubscription {
		private EventBus _eventBus;

		/// <summary>
		/// 事件类型
		/// </summary>
		public Type EventType { get; }

		/// <summary>
		/// 事件处理器
		/// </summary>
		public Delegate Handler { get; }

		/// <summary>
		/// 订阅是否有效
		/// </summary>
		public bool IsValid => _eventBus != null;

		internal EventSubscription(EventBus eventBus, Type eventType, Delegate handler) {
			_eventBus = eventBus;
			EventType = eventType;
			Handler = handler;
		}

		/// <summary>
		/// 取消订阅
		/// </summary>
		public void Unsubscribe() {
			if (IsValid) {
				_eventBus.Unsubscribe(this);
			}
		}

		/// <summary>
		/// 使订阅无效
		/// </summary>
		internal void Invalidate() {
			_eventBus = null;
		}
	}
}
