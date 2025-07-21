using System;
using System.Collections.Generic;
using GameLogic.Common.View;
using UnityEngine;

namespace GameLogic.UI.Common.UiComponents.SmoothChange {
	public abstract class SmoothChangeBase<T> : MonoBehaviour where T : struct {
		abstract public T GetCurVal();
		abstract protected void SetCurVal_Derived(T val);
		abstract protected T Add(T lhv, T rhv);
		abstract protected T Sub(T lhv, T rhv);
		abstract protected T Mul(T lhv, float rhv);

		private void Awake() {
			_target = GetCurVal();
		}

		public List<ChangeInfo> ChangeInfos;

		private T _target;
		private T _oriVal;
		private float _elapsedTime;
		private bool _running;

		private Action<T> _onChanged;
		private Action _stopCallback;
		private Action _doneCallback;

		/// <summary>
		/// 如果某次设置没有 set StopCallback 那再SetTarget的时候需要触发 StopCallback
		/// 如果某次设置 set 了 StopCallback，那原本的callback的触发在设置的时候触发，然后替换为新的callback，就不能再在 SetTarget 中触发
		/// </summary>
		private bool _newSetedStopCallback = false;
		/// <summary>
		/// 如果某次设置没有 set DoneCallback 那再SetTarget的时候需要清空 DoneCallback，防止上次的被打断的任务的残余 callback
		/// 如果某次设置 set 了 DoneCallback，那SetTarget就不该清空
		/// </summary>
		private bool _newSetedDoneCallback = false;

		/// <summary> 当前的变化配置 </summary>
		private ChangeInfo _curConfig;
		/// <summary> 当前变化的曲线，缓存从字典查找的结果 </summary>
		private Func<float, float> _curve;

		private void Update() {
			if (_running) {
				DealChange();
			}
		}
		private void DealChange() {
			var totalTime = _curConfig.TotalTime;

			_elapsedTime += _curConfig.UseLogicTime ? Time.deltaTime : Time.unscaledDeltaTime;

			var newVal = Add(_oriVal, Mul(Sub(_target, _oriVal), _curve(Mathf.Clamp01(_elapsedTime / totalTime))));
			SetCurVal_Derived(newVal);
			_onChanged?.Invoke(newVal);

			if (_elapsedTime >= totalTime) {
				_onChanged = null;
				_curve = null;
				_running = false;

				// 这里可能需要再回调中修改stopCallBack的值，就不做延后处理了，在这里用个临时变量搞一下
				if (_stopCallback != null) {
					var _tmpStopCallback = new Action(_stopCallback);
					_stopCallback = null;
					_tmpStopCallback?.Invoke();
				}
				if (_doneCallback != null) {
					var _tmpDoneCallback = new Action(_doneCallback);
					_doneCallback = null;
					_tmpDoneCallback?.Invoke();
				}
			}
		}
		private void OnInterprete() {
			if (_stopCallback == null) return;
			var _tmpStopCallback = _stopCallback;
			_stopCallback = null;
			_running = false;
			_tmpStopCallback?.Invoke();
		}

		#region PublicProperties
		public T CurVal => GetCurVal();
		public T Target => _target;
		#endregion

		#region PublicMethods
		public void SetCurVal(T val) {
			OnInterprete();
			SetCurVal_Derived(val);
			_target = val;
			_running = false;
		}
		public void TranslateCurVal(T val) {
			OnInterprete();
			SetCurVal(Add(GetCurVal(), val));
		}
		public void EndCurChange() {
			OnInterprete();
			_running = false;
			_target = GetCurVal();
		}

		public SmoothChangeBase<T> SetChangeInfoIndex(int index) {
			return SetChangeInfo(ChangeInfos[index]);
		}
		public SmoothChangeBase<T> SetChangeInfo(ChangeInfo config) {
			OnInterprete();
			_curConfig = config;
			_curve = ChangeCurves.GetCurve(_curConfig.CurveType);
			return this;
		}

		/// <summary>
		/// 每次值改变的时候触发, 方法参数为新值
		/// </summary>
		public SmoothChangeBase<T> SetOnChanged(Action<T> onChanged) {
			OnInterprete();
			_onChanged = onChanged;
			return this;
		}

		/// <summary>
		/// 只有在当前任务主动完成才能触发
		/// </summary>
		public SmoothChangeBase<T> SetDoneCallback(Action callback) {
			OnInterprete();
			_newSetedDoneCallback = true;
			_doneCallback = callback;
			return this;
		}

		/// <summary>
		/// 当前任务截止就会触发，包括主动的任务完成和被另一个任务打断
		/// </summary>
		public SmoothChangeBase<T> SetStopCallback(Action callback) {
			OnInterprete();
			_newSetedStopCallback = true;
			_stopCallback = callback;
			return this;
		}


		public void Translate(T val) => SetTarget(Add(_target, val));
		public void SetTarget(T val) {
			if (!_newSetedStopCallback) {
				OnInterprete();
			} else {
				_newSetedStopCallback = false;
			}

			if (!_newSetedDoneCallback) {
				_doneCallback = null;
			} else {
				_newSetedDoneCallback = false;
			}

			_oriVal = GetCurVal();
			_target = val;
			_elapsedTime = 0f;
			_running = true;
			if (_curve == null) {
				_curConfig = ChangeInfos[0];
				_curve = ChangeCurves.GetCurve(_curConfig.CurveType);
			}
		}
		#endregion
	}
}