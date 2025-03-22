using System;
using System.Collections.Generic;
using UnityEngine;

namespace NSFrame {
	public class StateMachine : MonoBehaviour {

		private sealed class StateKeyEqCMP : EqualityComparer<Enum> {
			public override bool Equals(Enum x, Enum y) { return Convert.ToInt32(x) == Convert.ToInt32(y); }
			public override int GetHashCode(Enum obj) { return obj.GetHashCode(); }
		}
		private readonly StateKeyEqCMP _stateKeyEqCMP;
		private Dictionary<Enum, IState> _stateDic;
		
		private Animator _animator;
		private IState _curState;

		private void OnEnable() {
			_stateDic = new(_stateKeyEqCMP);
		}

		public void SetAnimator(Animator animator) {
			_animator = animator;
		}
		public void RegisterState<T>(T stateKey, IState state) where T : struct, Enum {
			if (_stateDic.ContainsKey(stateKey)) return ;
			_stateDic.Add(stateKey, state);
		}
		public void SetState<T>(string stateName, float transitionDuration = 0f) where T : struct, Enum {
			if (Enum.TryParse(stateName, false, out T stateKey)) 
				SetState(stateKey, transitionDuration);
			else 
				Debug.LogError($"NS: State \"{stateName}\" not found in state machine.");
		}
		public void SetState<T>(T stateKey, float transitionDuration = 0f) where T : struct, Enum {
			if (!_stateDic.ContainsKey(stateKey)) {
				Debug.LogError($"NS: State \"{stateKey}\" not found in state machine.");
				return;
			}
			if (_curState != null) {
				if (_animator != null) 
					_animator.CrossFade(stateKey.ToString(), transitionDuration);
				_curState.Exit();
			}
			_curState = _stateDic[stateKey];
			_curState.Enter();
		}

		private void Update() { _curState?.Update(); }
		private void LateUpdate() { _curState?.LateUpdate(); }
		private void FixedUpdate() { _curState?.FixedUpdate(); }
	} 
}