using System;
using NSFrame;
using UnityEngine;

namespace OldGameLogic.Utilities
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
			EventSystem.RemoveListener((int)ModelEvt.Tick_0, AddTick, NSFrame.EventType.Model);
		}
		public void InitAfterPop() {
			EventSystem.AddListener((int)ModelEvt.Tick_0, AddTick, NSFrame.EventType.Model);
		}

		private void SetTrigger(Action action, float delay, int sumTriggerTimes, Action onComplete = null) {
			if (delay <= 0) {
				Debug.LogWarning("Do not need this delay trigger.");
				return;
			}
			_action = action;
			_onComplete = onComplete;
			_gap = delay;
			_sumTriggerTimes = sumTriggerTimes;
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

		/// <summary>
		/// 开始循环触发action
		/// </summary>
		/// <param name="action"> 触发的action </param>
		/// <param name="gap"> 间隔逻辑帧数量 </param>
		/// <param name="triggerTimes"> 触发次数 </param>
		/// <param name="callback"> 完成callback </param>
		public static void Run(Action action, float gap, int triggerTimes, Action callback = null) {
			PoolSystem.PopObj<LoopTrigger>().SetTrigger(action, gap, triggerTimes, callback);
		}

	}
}