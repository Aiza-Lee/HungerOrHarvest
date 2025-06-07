using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
	public class LowVitState : StateBase {
		public override State StaType => State.LowVit;

		public LowVitState() {
			Transitions.Add(new(ToWork, State.Work));
			Transitions.Add(new(ToMoving, State.Moving));
		}

		public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

		/// <summary>
		/// 缓存一帧内的体力检查结果，避免重复计算。
		/// </summary>
		private bool _cachedCheckFood;
		/// <summary>
		/// 缓存体力检查结果的有效性标志。
		/// </summary>
		private bool _cacheValid = false;

		private bool CheckFood() {
			if (_cacheValid) {
				return _cachedCheckFood;
			}
			_cacheValid = true;
			var vitDemand = ConfigMgr.Config.VitConfig.RecoverVitThreshold * ConfigMgr.Config.VitConfig.MaxVit - _impler.VitHelper.CurVit;
			var foodDemand = vitDemand / ConfigMgr.Config.VitConfig.VitPerFood;
			return _cachedCheckFood = RepoMgr.Inst.CheckRequest(RepoType.Food, foodDemand, _impler.RepoBuffHelper.ConsBuffs_F);
		}

		private bool ToWork() => !CheckFood();
		private bool ToMoving() {
			if (CheckFood()) {
				StateMachine.MoveToTarget = MoveToTargetType.Recover;
				return true;
			}
			return false;
		}

		public override void Execute() {
			_cacheValid = false;
		}

		protected override void LogicDestroy_Derived() {
			_cacheValid = false;
		}

		public override void OnEnd() {}

		public override void OnEnter() {
			_cacheValid = false;
		}
	}
}