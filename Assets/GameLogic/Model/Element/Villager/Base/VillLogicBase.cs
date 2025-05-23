using System;
using GameLogic.Model.Element.Arch;
using GameLogic.Model.Factory;
using NSFrame;

namespace GameLogic.Model.Element.Vill
{
	public abstract class VillLogicBase : ISaveable<VillSaveBase> {
		public abstract VillType VillType { get; }
		public VillLogicBase() {
			EventSystem.AddListener((int)ModelEvt.DayStart_0, OnDayStart, NSFrame.EventType.Model);
			EventSystem.AddListener((int)ModelEvt.NightStart_0, OnNightStart, NSFrame.EventType.Model);
		}

		private TaskRunner _taskRunner;
		private ExpHelper _expHelper;
		private ulong _homeID;
		private ulong _bondedWorkArchID;

		public ulong ID { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public Coord Coord { get; private set; }
		public RTList<float> ConsBuffs_F => _expHelper.ConsBuffs_F;
		public RTList<float> ProdBuffs_F => _expHelper.ProdBuffs_F;
		public TaskType CurTaskType => _taskRunner.CurTaskType;

		public bool IsHomeless => _homeID == 0;
		public bool IsWorkless => _bondedWorkArchID == 0;

		private void DestroyImpl() {
			_taskRunner.Destroy();
			if (_bondedWorkArchID != 0) DisBondArchImpl(WorldMgr.Inst.FindArch(_bondedWorkArchID));
			if (_homeID != 0) DisBondArchImpl(WorldMgr.Inst.FindArch(_homeID));
			EventSystem.RemoveListener((int)ModelEvt.DayStart_0, OnDayStart, NSFrame.EventType.Model);
			EventSystem.RemoveListener((int)ModelEvt.NightStart_0, OnNightStart, NSFrame.EventType.Model);
			EventSystem.Invoke((int)ModelEvt.VillDestroyed_V_1, this, NSFrame.EventType.Model);
		}

		/// <summary>
		/// 前往工作，根据维护的建筑ID: _bondedWorkArchID
		/// </summary>
		private bool GoWork() {
			if (_bondedWorkArchID == 0) { return false; }
			var arch = WorldMgr.Inst.FindArch(_bondedWorkArchID);
			return _taskRunner.SetGoWorkTasks(arch);
		}
		private void OnDayStart() {
			if (!GoWork()) { _taskRunner.ResetTasks(); }
		}

		/// <summary>
		/// 离开世界
		/// </summary>
		private void LeaveWorldImpl() {
			// todo:
			DestroyImpl();
		}
		private void OnNightStart() {
			if (!_taskRunner.SetGoSleepTasks(_homeID)) { LeaveWorldImpl(); }
		}

		private void OnBondedArchDestroyed(ArchLogicBase arch) {
			DisBondArchImpl(arch);
		}
		/// <summary>
		/// 解绑建筑 Impl
		/// </summary>
		private void DisBondArchImpl(ArchLogicBase arch) {
			if (arch == null) { return; }
			arch.OnArchDestroyed -= OnBondedArchDestroyed;
			if (arch is CottageLogic) {
				_homeID = 0;
				return;
			}

			arch.DisBondVill(this);
			_bondedWorkArchID = 0;
			EventSystem.Invoke<ulong, ulong, ulong>((int) ModelEvt.VillChengeWork_VuAuAu_3, ID, arch.ID, 0, NSFrame.EventType.Model);

			if (_taskRunner.CurTaskType == TaskType.Work) {
				// 如果正在工作, 重置任务
				_taskRunner.ResetTasks();
			} else if (_taskRunner.CurTaskType == TaskType.MoveTo && _taskRunner.CurMoveToTargetType == MoveToTargetType.WorkArch) {
				// 如果正在移动到工作建筑, 重置任务
				_taskRunner.ResetTasks();
			}
		}

		#region Event
		public event Action<Coord> OnCoordChange;
		#endregion

		#region PublicMethods

		/// <summary>
		/// 销毁村民
		/// </summary>
		public void Destroy() => DestroyImpl();

		/// <summary>
		/// 绑定到建筑，绑定home和工作建筑都调用这个方法
		/// </summary>
		public void BondArch(ArchLogicBase arch) {
			if (arch is CottageLogic) {
				_homeID = arch.ID;
			} else {
				_bondedWorkArchID = arch.ID;
			}
			arch.OnArchDestroyed += OnBondedArchDestroyed;
			arch.BondVill(this);
			if (arch is not CottageLogic) {
				// 绑定建筑后，可能需要直接触发去这个建筑工作
				if (_taskRunner.CurTaskType == TaskType.MoveTo) {
					var curTar = _taskRunner.CurMoveToTargetType;
					if (curTar != MoveToTargetType.HomeEat
						&& curTar != MoveToTargetType.HomeSleep
						&& curTar != MoveToTargetType.Outer) {
						GoWork();
					}
				}
			}
		}
		/// <summary>
		/// 与工作的建筑解绑
		/// </summary>
		public void DisBondWorkArch() => DisBondArchImpl(WorldMgr.Inst.FindArch(_bondedWorkArchID));
		/// <summary>
		/// 与 home 解绑
		/// </summary>
		public void DisBondHome() => DisBondArchImpl(WorldMgr.Inst.FindArch(_homeID));

		public void Move(Coord dltCoord) {
			Coord += dltCoord;
			OnCoordChange?.Invoke(dltCoord);
		}

		/// <summary>
		/// 返回按照经验值从大到小排序的职业等级
		/// </summary>
		public JTList<int> GetSortedJobLevels() => _expHelper.GetSortedJobLevelsImpl();
		/// <summary>
		/// 添加经验值，如果当前经验值满了而没有下一级的 Config，那经验值不会再增加
		/// </summary>
		public void AddExp(JTList<float> exps) => _expHelper.AddExpImpl(exps);
		/// <summary>
		/// 返回某个职业的经验值占升级所需要的总经验值的比例
		/// </summary>
		public float GetJobExpProportion(JobType jobType) => _expHelper.GetJobExpProportionImpl(jobType);
		public int GetJobLevel(JobType jobType) => _expHelper.GetJobLevelImpl(jobType);

		#endregion

		#region ISaveable
		protected abstract VillSaveBase GetDerivedSave();
		public VillSaveBase GetSave() {
			var save = GetDerivedSave();
				save.TypeName 			= VillType.ToString();
				save.ID 				= ID;
				save.FirstName 			= FirstName;
				save.LastName 			= LastName;
				save.Coord 				= Coord;
				save.TaskRunner 		= _taskRunner.GetSave();
				save.ExpHelper 			= _expHelper.GetSave();
				save.HomeID 			= _homeID;
				save.AttachedWorkArchID = _bondedWorkArchID;
			return save;
		}

		protected abstract void DerivedInitFromSave(VillSaveBase save);
		public virtual void InitFromSave(VillSaveBase save) {
			DerivedInitFromSave(save);
			ID 					= save.ID;
			FirstName 			= save.FirstName;
			LastName 			= save.LastName;
			Coord 				= save.Coord;
			_taskRunner 		= LogicFctry.Inst.LoadVillTaskRunner(this, save.TaskRunner);
			_expHelper			= LogicFctry.Inst.LoadVillExpHelper(this, save.ExpHelper);
			_homeID 			= save.HomeID;
			_bondedWorkArchID 	= save.AttachedWorkArchID;
		}
		#endregion
	}
}