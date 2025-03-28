using System;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	[Serializable]
	public class DelayTrigger : IPooledObject {
		static DelayTrigger() {
			PoolSystem.InitObjectPool<DelayTrigger>();
		}

		private Action _action;
		private int _delayTick;
		private int _tickCount;

		public void CleanBeforePush() {
			_action = null;
			EventSystem.RemoveListener((int)LogicEvt.Tick, AddTick);
		}
		public void InitAfterPop() {
			EventSystem.AddListener((int)LogicEvt.Tick, AddTick);
		}

		private void SetTrigger(Action action, int delayTick) {
			if (delayTick <= 0) {
				Debug.LogWarning("Do not need this delay trigger.");
				return;
			}
			_action = action;
			_delayTick = delayTick;
			_tickCount = 0;
		}

		private void AddTick() {
			++_tickCount;
			if (_tickCount == _delayTick) {
				_action?.Invoke();
				PoolSystem.PushObj(this);
			}
		}

		public static void Run(Action action, int delayTick) {
			PoolSystem.PopObj<DelayTrigger>().SetTrigger(action, delayTick);
		}

	}
}