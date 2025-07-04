using System;
using NSFrame;
using UnityEngine;

namespace OldGameLogic.Utilities
{
	public class DelayTrigger : IPooledObject {
		static DelayTrigger() {
			PoolSystem.InitObjectPool<DelayTrigger>();
		}

		private Action _action;
		private int _delayTick;
		private int _tickCount;

		/// <summary>
		/// 从开始到现在持续的总的tick
		/// </summary>
		public int LastedTicks => _tickCount;
		/// <summary>
		/// 余下的等待Tick量
		/// </summary>
		public int RestTicks => _delayTick - _tickCount;

		#region IPooledObject
		public void CleanBeforePush() {
			_action = null;
			EventSystem.RemoveListener((int) ModelEvt.Tick_0, AddTick, NSFrame.EventType.Model);
		}
		public void InitAfterPop() {
			EventSystem.AddListener((int)ModelEvt.Tick_0, AddTick, NSFrame.EventType.Model);
		}
		#endregion

		/// <summary>
		/// 中止当前任务，回收到对象池
		/// </summary>
		public void Stop() {
			PoolSystem.PushObj(this);
		}

		private DelayTrigger SetTrigger(Action action, int delayTick) {
			if (delayTick <= 0) {
				Debug.LogWarning("Do not need this delay trigger.");
				return this;
			}
			_action = action;
			_delayTick = delayTick;
			_tickCount = 0;
			return this;
		}

		private void AddTick() {
			++_tickCount;
			if (_tickCount == _delayTick) {
				_action?.Invoke();
				PoolSystem.PushObj(this);
			}
		}

		public static DelayTrigger Run(Action action, int delayTick) {
			return PoolSystem.PopObj<DelayTrigger>().SetTrigger(action, delayTick);
		}

	}
}