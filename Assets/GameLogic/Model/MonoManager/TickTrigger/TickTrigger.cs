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

		public event Action BeforeTick;
		public event Action AfterTick;
		public float TickPerSec => 1f / TickTime;

		private void Start() {
			Pause = true;
		}

		public ulong TickSum {
			get => _tickSum;
			private set {
				while (_tickSum < value) {
					BeforeTick?.Invoke();
					EventSystem.Invoke((int)ModelEvt.Tick_0, NSFrame.EventType.Model);
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
					Time.timeScale = 0f;
					EventSystem.Invoke((int)ModelEvt.GamePause_0, NSFrame.EventType.Model);
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
				EventSystem.Invoke<float>((int)ModelEvt.TickSpeedChange_f_1, value, NSFrame.EventType.Model);
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