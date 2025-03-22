using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public abstract class SmoothChangeBase<T> : MonoBehaviour, IViewStoppable where T : struct {
		abstract public T GetCurVal();
		abstract protected void DerivedSetCurVal(T val);
		public void SetCurVal(T val) {
			DerivedSetCurVal(val);
			_target = val;
			_running = false;
		}
		public void TranslateCurVal(T val) {
			SetCurVal(Add(GetCurVal(), val));
		}
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
		private Action _endCallback;

		private void Update() {
			if (_updateable) {
				if (_running) {
					DealChange();
				}
			}
		}

		public void StopCur() {
			_running = false;
			_target = GetCurVal();
		}
		public SmoothChangeBase<T> SetMod(int modID) {
			_curModID = modID;
			return this;
		}
		public void Translate(T val, Action endCallback = null) => SetTarget(Add(_target, val), endCallback);
		public void SetTarget(T val, Action endCallback = null) {
			_oriVal = GetCurVal();
			_target = val;
			_distance = Sub(_target, GetCurVal());
			_elapsedTime = 0f;
			_endCallback = endCallback;
			_running = true;
		}

		private void DealChange() {
			var curve = Configs[_curModID].Curve;
			var time = Configs[_curModID].Time;
			_elapsedTime += TickMove ? Time.deltaTime : Time.unscaledDeltaTime;
			var newPrcs = Mathf.Clamp01(_elapsedTime / time);
			DerivedSetCurVal(Add(_oriVal, Mul(_distance, curve.Evaluate(newPrcs))));
			if (_elapsedTime >= time) {
				_endCallback?.Invoke();
				_endCallback = null;
				_running = false;
			}
		}

		#region IViewStoppable
			private bool _updateable = true;
			public void Pause() => _updateable = false;
			public void Run() => _updateable = true;
		#endregion
	}
}