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
			Transitions.Add(new(ToMoving, State.Arrive));
			Transitions.Add(new(ToLowVit, State.LowVit));
		}
		public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

		private bool _enterFailed = false;

		private bool ToLowVit() {
			if (VitHelper.VitPercentage < ConfigMgr.Config.VitConfig.LowVitThreshold && StateMachine.RecoverChance > 0) {
				StateMachine.RecoverChance--;
				return true;
			}
			return false;
		}
		private bool ToMoving() {
			if (_enterFailed) {
				// 如果进入工作状态失败，直接移动到新的工作地点
				StateMachine.MoveToTarget = MoveToTargetType.Random;
				return true;
			}
			if (_arch.ID != BondArchHelper.BondedWorkArchID) {
				StateMachine.MoveToTarget = MoveToTargetType.Random;
				_arch.VillLeave(_impler.ID);
				_arch = null;
				return true; // 需要移动到新的工作地点
			}
			return false;
		}

		public override void Execute() {
			if (VitHelper.IsHungry) {
				var consBuff = new RTList<float>(ConfigMgr.Config.VitConfig.HungryProdLoss);
				var prodBuff = new RTList<float>(-ConfigMgr.Config.VitConfig.HungryProdLoss);
				if (RepoMgr.Inst.TryCons(_arch.Lconfig.ExtraConsVelsPerOne, _arch.ConsBuffs_F, RepoBuffHelper.ConsBuffs_F, consBuff)) {
					RepoMgr.Inst.Prod(_arch.Lconfig.ExtraConsVelsPerOne, _arch.ProdBuffs_F, RepoBuffHelper.ProdBuffs_F, prodBuff);
					VitHelper.TryConsVit(_arch.Lconfig.VitConsRate);
					ExpHelper.AddExp(_arch.Lconfig.ExpAdds);
				}
				return;
			}
			// 执行非饥饿状态工作逻辑
			if (RepoMgr.Inst.TryCons(_arch.Lconfig.ExtraConsVelsPerOne, _arch.ConsBuffs_F, RepoBuffHelper.ConsBuffs_F)) {
				RepoMgr.Inst.Prod(_arch.Lconfig.ExtraConsVelsPerOne, _arch.ProdBuffs_F, RepoBuffHelper.ProdBuffs_F);
				VitHelper.TryConsVit(_arch.Lconfig.VitConsRate);
				ExpHelper.AddExp(_arch.Lconfig.ExpAdds);
			}
		}

		protected override void LogicDestroy_Derived() {
			_arch?.VillLeave(_impler.ID);
			_arch = null;
			_enterFailed = false;
		}

		public override void OnEnd() {
			_arch?.VillLeave(_impler.ID);
			_arch = null;
			_enterFailed = false;
		}

		public override void OnEnter() {
			var workID = BondArchHelper.BondedWorkArchID;
			if (workID == 0) {
				_enterFailed = true;
				return;
			}
			_arch = WorldMgr.Inst.FindArch(workID);
			if (_arch.Coord != _impler.Coord) {
				// 如果工作地点和当前坐标不一致，说明在移动期间更换了工作
				StateMachine.MoveToTarget = MoveToTargetType.Random;
				_enterFailed = true;
				return;
			}
			_arch.VillArrive(_impler.ID);
			_enterFailed = false;

			if (workID != 0) {
				_arch = WorldMgr.Inst.FindArch(workID);
				_arch.VillArrive(_impler.ID);
			} else {
				Debug.LogError($"Villager {_impler.ID} has no work arch bonded, cannot enter WorkState.");
			}
		}
	}
}