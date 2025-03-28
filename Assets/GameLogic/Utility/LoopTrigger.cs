using System;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	[Serializable]
	public class LoopTrigger : IPooledObject {
		static LoopTrigger() {
			PoolSystem.InitObjectPool<LoopTrigger>();
		}

		private Action _action;
		private Action _onComplete;
		private float _gap;
		private int _tickCount;
		private int _sumTriggerTimes;
		private int _triggerCnt;

		public void CleanBeforePush() {
			_action = null;
			_onComplete = null;
			EventSystem.RemoveListener((int)LogicEvt.Tick, AddTick);
		}
		public void InitAfterPop() {
			EventSystem.AddListener((int)LogicEvt.Tick, AddTick);
		}

		private void SetTrigger(Action action, float delay, int SumTriggerTimes, Action onComplete = null) {
			if (delay <= 0) {
				Debug.LogWarning("Do not need this delay trigger.");
				return;
			}
			_action = action;
			_onComplete = onComplete;
			_gap = delay;
			_sumTriggerTimes = SumTriggerTimes;
			_triggerCnt = 0;
			_tickCount = 0;
		}

		private void AddTick() {
			++_tickCount;
			while (_tickCount >= _gap * _triggerCnt) {
				++_triggerCnt;
				_action.Invoke();
				if (_triggerCnt == _sumTriggerTimes) {
					_onComplete?.Invoke();
					PoolSystem.PushObj(this);
				}
			}
		}

		public static void Run(Action action, float gap, int triggerTimes, Action onComplete = null) {
			PoolSystem.PopObj<LoopTrigger>().SetTrigger(action, gap, triggerTimes, onComplete);
		}

	}
}