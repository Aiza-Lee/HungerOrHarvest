using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.View
{
	public abstract class SmoothChangeBase<T> : MonoBehaviour where T : struct {
		abstract public T GetCurVal();
		abstract protected void SetCurVal_Derived(T val);
		abstract protected T Add(T lhv, T rhv);
		abstract protected T Sub(T lhv, T rhv);
		abstract protected T Mul(T lhv, float rhv);

		private void Awake() {
			_target = GetCurVal();
		}

		public List<ChangeConfig> Configs;
		public bool TickMove;

		private T _target;
		private T _distance;
		private T _oriVal;
		private float _elapsedTime;
		private int _curModID;
		private bool _running;
		
		private Action<T> OnChanged;
		private Action _stopCallback;
		private Action _doneCallback;

		/// <summary>
		/// 如果某次设置没有 set StopCallback 那再SetTarget的时候需要触发 StopCallback
		/// 如果某次设置 set 了 StopCallback，那原本的callback的触发在设置的时候触发，然后替换为新的callback，就不能再在 SetTarget 中触发
		/// </summary>
		private bool _triggerStopCallbackFlag = true;
		/// <summary>
		/// 如果某次设置没有 set DoneCallback 那再SetTarget的时候需要清空 DoneCallback，防止上次的被打断的任务的残余 callback
		/// 如果某次设置 set 了 DoneCallback，那SetTarget就不该清空
		/// </summary>
		private bool _clearDoneCallbackFlag = true;


		private void Update() {
			if (_running) {
				DealChange();
			}
		}
		private void DealChange() {

			var curve = Configs[_curModID].Curve;
			var time = Configs[_curModID].Time;
			_elapsedTime += TickMove ? Time.deltaTime : Time.unscaledDeltaTime;
			var newPrcs = Mathf.Clamp01(_elapsedTime / time);
			var newVal = Add(_oriVal, Mul(_distance, curve.Evaluate(newPrcs)));
			SetCurVal_Derived(newVal);
			OnChanged?.Invoke(newVal);

			if (_elapsedTime >= time) {
				OnChanged = null;
				_curModID = 0;
				_running = false;

				_stopCallback?.Invoke();
				_stopCallback = null;
				_doneCallback?.Invoke();
				_doneCallback = null;
			}
		}
		private void OnInterprete() {
			_stopCallback?.Invoke();
			_stopCallback = null;
			_running = false;
		}

		#region PublicProperties
		public T CurVal => GetCurVal();
		public T Target => _target;
		#endregion

		#region PublicMethods
		public void SetCurVal(T val) {
			SetCurVal_Derived(val);
			_target = val;
			_running = false;
		}
		public void TranslateCurVal(T val) {
			SetCurVal(Add(GetCurVal(), val));
		}
		public void EndCurChange() {
			OnInterprete();
			_running = false;
			_target = GetCurVal();
		}

		public SmoothChangeBase<T> SetMod(int modID) {
			OnInterprete();
			_curModID = modID;
			return this;
		}

		/// <summary>
		/// 每次值改变的时候触发, 方法参数为新值
		/// </summary>
		public SmoothChangeBase<T> SetOnChanged(Action<T> onChanged) {
			OnInterprete();
			OnChanged = onChanged;
			return this;
		}

		/// <summary>
		/// 只有在当前任务主动完成才能触发
		/// </summary>
		public SmoothChangeBase<T> SetDoneCallback(Action callback) {
			OnInterprete();
			_clearDoneCallbackFlag = false;
			_doneCallback = callback;
			return this;
		}

		/// <summary>
		/// 当前任务截止就会触发，包括主动的任务完成和被另一个任务打断
		/// </summary>
		public SmoothChangeBase<T> SetStopCallback(Action callback) {
			OnInterprete();
			_triggerStopCallbackFlag = false;
			_stopCallback = callback;
			return this;
		}


		public void Translate(T val) => SetTarget(Add(_target, val));
		public void SetTarget(T val) {
			if (_triggerStopCallbackFlag) {
				OnInterprete();
			} else {
				_triggerStopCallbackFlag = true;
			}

			if (_clearDoneCallbackFlag) {
				_doneCallback = null;
			} else {
				_clearDoneCallbackFlag = true;
			}

			_oriVal = GetCurVal();
			_target = val;
			_distance = Sub(_target, GetCurVal());
			_elapsedTime = 0f;
			_running = true;
		}
		#endregion
	}
}