using System;
using GameLogic.Model.Factory;
using NSFrame;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// 村民基础逻辑的具体实现类，也是各个Helper类的通信中介
	/// </summary>
	public class LogicImpler : ISaveable<LogicImplerSave>, IVillBasicInfo {
		private readonly VillLogicBase _vill;
		public LogicImpler(VillLogicBase vill) {
			_vill = vill;
			EventSystem.AddListener((int) ModelEvt.Tick_0, InvokeTickUpdate, EventType.Model);
			EventSystem.AddListener((int) ModelEvt.DayStart_0, InvokeOnDayStart, EventType.Model);
			EventSystem.AddListener((int) ModelEvt.NightStart_0, InvokeOnNightStart, EventType.Model);
		}

		public TaskRunner TaskRunner { get; private set; }
		public ExpHelper ExpHelper { get; private set; }
		public RepoBuffHelper RepoBuffHelper { get; private set; }
		public BondArchHelper BondArchHelper { get; private set; }
		public VitHelper VitHelper { get; private set; }

		public event Action TickUpdate;
		public event Action OnNightStart;
		public event Action OnDayStart;

		private void InvokeTickUpdate() => TickUpdate?.Invoke();
		private void InvokeOnNightStart() => OnNightStart?.Invoke();
		private void InvokeOnDayStart() => OnDayStart?.Invoke();



		#region IVillBasicInfo
		public ulong ID { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public Coord Coord { get; private set; }
		
		public event Action<Coord> OnCoordChange;
		public void Move(Coord dltCoord) {
			Coord += dltCoord;
			OnCoordChange?.Invoke(dltCoord);
		}
		#endregion

		public void LogicDestroy() {
			EventSystem.RemoveListener((int) ModelEvt.Tick_0, InvokeTickUpdate, EventType.Model);
			EventSystem.RemoveListener((int) ModelEvt.DayStart_0, InvokeOnDayStart, EventType.Model);
			EventSystem.RemoveListener((int) ModelEvt.NightStart_0, InvokeOnNightStart, EventType.Model);
			TaskRunner.LogicDestroy();
			ExpHelper.LogicDestroy();
			RepoBuffHelper.LogicDestroy();
			VitHelper.LogicDestroy();
			BondArchHelper.LogicDestroy();

			EventSystem.Invoke((int) ModelEvt.VillDestroyed_V_1, _vill, EventType.Model);
		}

		#region ISaveable
		public LogicImplerSave GetSave() {
			return new() {
				ID = ID,
				FirstName = FirstName,
				LastName = LastName,
				Coord = Coord,
				TaskRunnerSave = TaskRunner.GetSave(),
				ExpHelperSave = ExpHelper.GetSave(),
				VitHelperSave = VitHelper.GetSave(),
				BondArchHelperSave = BondArchHelper.GetSave()
			};
		}
		public void InitFromSave(LogicImplerSave save) {
			ID = save.ID;
			FirstName = save.FirstName;
			LastName = save.LastName;
			Coord = save.Coord;
			TaskRunner = LogicFctry.Inst.LoadVillTaskRunner(this, save.TaskRunnerSave);
			ExpHelper = LogicFctry.Inst.LoadVillExpHelper(this, save.ExpHelperSave);
			RepoBuffHelper = LogicFctry.Inst.LoadVillRepoBuffHelper(this, save.RepoBuffHelperSave);
			VitHelper = LogicFctry.Inst.LoadVillVitHelper(this, save.VitHelperSave);
			BondArchHelper = LogicFctry.Inst.LoadBondArchHelper(this, save.BondArchHelperSave);
		}
		#endregion
	}
}