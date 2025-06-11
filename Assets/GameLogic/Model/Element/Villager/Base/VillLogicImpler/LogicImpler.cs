using System;
using GameLogic.Model.Factory;
using GameLogic.Utilities;
using NSFrame;
using NSFrame.BehaviourTree;

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
			BehaviourHelper = BehaviourHelperFactory.CreateBehaviourHelper(this);
			ExpHelper = LogicFctry.Inst.NewVillExpHelper(this);
			RepoBuffHelper = LogicFctry.Inst.NewVillRepoBuffHelper(this);
			VitHelper = LogicFctry.Inst.NewVillVitHelper(this);
			BondArchHelper = LogicFctry.Inst.NewBondArchHelper(this);
		}

		public BehaviourHelper BehaviourHelper { get; }
		public ExpHelper ExpHelper { get; }
		public RepoBuffHelper RepoBuffHelper { get; }
		public BondArchHelper BondArchHelper { get; }
		public VitHelper VitHelper { get; }

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
				ExpHelperSave = ExpHelper.GetSave(),
				VitHelperSave = VitHelper.GetSave(),
				BondArchHelperSave = BondArchHelper.GetSave(),
				RepoBuffHelperSave = RepoBuffHelper.GetSave(),
				BehaviourHelperSave = BehaviourHelper.GetSave()
			};
		}
		public void InitFromSave(LogicImplerSave save) {
			ID = save.ID;
			FirstName = save.FirstName;
			LastName = save.LastName;
			Coord = save.Coord;
			BehaviourHelper.InitFromSave(save.BehaviourHelperSave);
			ExpHelper.InitFromSave(save.ExpHelperSave);
			RepoBuffHelper.InitFromSave(save.RepoBuffHelperSave);
			VitHelper.InitFromSave(save.VitHelperSave);
			BondArchHelper.InitFromSave(save.BondArchHelperSave);
		}
		#endregion
	}
}