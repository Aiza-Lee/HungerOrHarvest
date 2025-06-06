using System;
using System.Collections.Generic;
using GameLogic.Model.Element.Arch;
using GameLogic.Model.Mgr;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	public class WorkState : StateBase {

		private ArchLogicBase _arch;

		public override State StaType => State.Work;
		public WorkState() {
			Transitions.Add(new(ToLowVit, State.LowVit));
			Transitions.Add(new(ToMove, State.Arrive));
		}
		public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

		private bool ToLowVit() {
			if (VitHelper.VitPercentage < ConfigMgr.Config.VitConfig.LowVitThreshold && StateMachine.RecoverChance > 0) {
				StateMachine.RecoverChance--;
				return true;
			}
			return false;
		}
		private bool ToMove() {
			if (_arch.ID != BondArchHelper.BondedWorkArchID) {
				StateMachine.MoveToTarget = MoveToTargetType.Random;
				_arch.VillLeave(_impler.ID);
				_arch = null;
				return true; // 需要移动到新的工作地点
			}
			return false;
		}

		public override void Execute() {
			if (RepoMgr.Inst.TryCons(_arch.Lconfig.ExtraConsVelsPerOne, _arch.ConsBuffs_F, RepoBuffHelper.ConsBuffs_F)) {
				RepoMgr.Inst.Prod(_arch.Lconfig.ExtraConsVelsPerOne, _arch.ProdBuffs_F, RepoBuffHelper.ProdBuffs_F);
				VitHelper.TryConsVit(_arch.Lconfig.VitConsRate);
				ExpHelper.AddExp(_arch.Lconfig.ExpAdds);
			}
		}

		public override void LogicDestroy() {
			_arch?.VillLeave(_impler.ID);
			_arch = null;
		}

		public override void OnEnd() {
			_arch?.VillLeave(_impler.ID);
			_arch = null;
		}

		public override void OnEnter() {
			var workID = BondArchHelper.BondedWorkArchID;
			if (workID != 0) {
				_arch = WorldMgr.Inst.FindArch(workID);
				_arch.VillArrive(_impler.ID);
			} else {
				Debug.LogError($"Villager {_impler.ID} has no work arch bonded, cannot enter WorkState.");
			}
		}
	}
}