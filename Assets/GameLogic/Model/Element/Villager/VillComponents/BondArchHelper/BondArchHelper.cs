using GameLogic.Model.Element.Arch;
using NSFrame;

namespace GameLogic.Model.Element.Vill {
	public class BondArchHelper : ISaveable<BondArchHelperSave>, IVillBondArch {
		readonly LogicImpler _impler;
		public BondArchHelper(LogicImpler impler) {
			_impler = impler;
		}
		public void LogicDestroy() {
			if (BondedWorkArchID != 0) DisBondArchImpl(WorldMgr.Inst.FindArch(BondedWorkArchID));
			if (HomeID != 0) DisBondArchImpl(WorldMgr.Inst.FindArch(HomeID));
		}

		private TaskRunner TaskRnr => _impler.TaskRunner;
		private void OnBondedArchDestroyed(ArchLogicBase arch) {
			DisBondArchImpl(arch);
		}
		private void DisBondArchImpl(ArchLogicBase arch) {
			if (arch == null) { return; }
			arch.OnArchDestroyed -= OnBondedArchDestroyed;
			if (arch is CottageLogic) {
				arch.DisBondVill(_impler.ID);
				HomeID = 0;
				return;
			}

			arch.DisBondVill(_impler.ID);
			BondedWorkArchID = 0;
			EventSystem.Invoke<ulong, ulong, ulong>((int) ModelEvt.VillChengeWork_VuAuAu_3, _impler.ID, arch.ID, 0, NSFrame.EventType.Model);

			if (TaskRnr.CurTaskType == TaskType.Work) {
				// 如果正在工作, 重置任务
				TaskRnr.ResetTasks();
			} else if (TaskRnr.CurTaskType == TaskType.MoveTo && TaskRnr.CurMoveToTargetType == MoveToTargetType.WorkArch) {
				// 如果正在移动到工作建筑, 重置任务
				TaskRnr.ResetTasks();
			} else if (TaskRnr.CurTaskType == null) {
				// 如果在同一个逻辑帧中经行了建筑的绑定和解绑
				// taskRunner还没有来得及把队列中的任务添加到正在执行的任务
				TaskRnr.ResetTasks();
			}
		}

		/// <summary>
		/// 前往工作，根据维护的建筑ID: _bondedWorkArchID
		/// </summary>
		private bool GoWork() {
			if (BondedWorkArchID == 0) { return false; }
			var arch = WorldMgr.Inst.FindArch(BondedWorkArchID);
			return TaskRnr.SetGoWorkTasks(arch);
		}


		#region IVillBondArch
		public ulong HomeID { get; private set; }
		public ulong BondedWorkArchID { get; private set; }
		public bool IsHomeless => HomeID == 0;
		public bool IsWorkless => BondedWorkArchID == 0;

		public void BondArch(ArchLogicBase arch) {
			if (arch is CottageLogic) {
				HomeID = arch.ID;
			} else {
				BondedWorkArchID = arch.ID;
			}
			arch.OnArchDestroyed += OnBondedArchDestroyed;
			arch.BondVill(_impler.ID);
			if (arch is not CottageLogic) {
				// 绑定建筑后，可能需要直接触发去这个建筑工作
				if (TaskRnr.CurTaskType == TaskType.MoveTo) {
					var curTar = TaskRnr.CurMoveToTargetType;
					if (curTar != MoveToTargetType.HomeEat
						&& curTar != MoveToTargetType.HomeSleep
						&& curTar != MoveToTargetType.Outer) {
						GoWork();
					}
				}
			}
		}
		public void DisBondHome() => DisBondArchImpl(WorldMgr.Inst.FindArch(HomeID));
		public void DisBondWorkArch() => DisBondArchImpl(WorldMgr.Inst.FindArch(BondedWorkArchID));

		#endregion

		#region ISaveable
		public BondArchHelperSave GetSave() {
			return new() {
				HomeID = HomeID,
				BondedWorkArchID = BondedWorkArchID
			};
		}
		public void InitFromSave(BondArchHelperSave save) {
			HomeID = save.HomeID;
			BondedWorkArchID = save.BondedWorkArchID;
		}

		#endregion
	}
}