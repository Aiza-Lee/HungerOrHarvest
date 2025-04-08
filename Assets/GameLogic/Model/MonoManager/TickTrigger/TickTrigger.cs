using System;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public sealed class TickTrigger : MonoSingleton<TickTrigger> {


		[Header("Readonly")]
		[Range(0.01f, 0.1f)] public float TickTime = 0.01f;
		[SerializeField] private ulong _tickSum;
		[SerializeField] private float _speed = 1f;
		[SerializeField] private bool _pause = false;

		private float _realTimeSum = 0f;
		private float RealTickTime => TickTime / _speed;

		public Action BeforeTick;
		public Action AfterTick;

		public ulong TickSum {
			get => _tickSum;
			private set {
				while (_tickSum < value) {
					BeforeTick?.Invoke();
					EventSystem.Invoke((int)LogicEvt.Tick_0);
					AfterTick?.Invoke();
					++_tickSum;
				}
			} 
		}

		public bool Pause { 
			get => _pause;
			set {
				if (value == _pause) return;

				if (value == true) {
					EventSystem.Invoke((int)LogicEvt.GamePause_0);
					Time.timeScale = 0f;
				} else {
					_realTimeSum = Time.unscaledTime;
					Time.timeScale = _speed;
				}
				_pause = value;
			}
		}

		public float Speed {
			get => _speed;
			set {
				if (value == _speed) return;

				if (value == 0f) {
					Pause = true;
					return;
				}

				Time.timeScale = value;
				_speed = value;
				EventSystem.Invoke<float>((int)LogicEvt.TickSpeedChange_f_1, value);
			}
		}


		void Update() {
			if (_pause) { return; }
			while (_realTimeSum + RealTickTime < Time.unscaledTime) {
				++TickSum;
				_realTimeSum += RealTickTime;
			}
		}
		
	}
}