using System.Collections.Generic;
using GameLogic.Model.Mgr;
using GameLogic.Utilities;
using NSFrame.BehaviourTree;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// MyBlackBoard 类实现了 IBlackboard 接口，提供了行为树所需的黑板数据存储。
	/// 包含了与逻辑实现相关的各种帮助器，并且可以存储移动路线、恢复状态、死亡状态等信息。
	/// </summary>
	public class MyBlackboard : IBlackboard, ISaveable<MyBlackBoardSave> {
		public MyBlackboard(LogicImpler impler) {
			Impler = impler;
		}
		public LogicImpler Impler { get; }

		public IVitHelper VitHelper => Impler.VitHelper;
		public IBondArchHelper BondArchHelper => Impler.BondArchHelper;
		public IExpHelper ExpHelper => Impler.ExpHelper;
		public IRepoBuffHelper RepoBuffHelper => Impler.RepoBuffHelper;

		public List<Coord> MoveRoute { get; set; }
		public int CurMoveIndex { get; set; }
		public ulong LastMoveTime { get; set; }

		public int RecoverChance { get; set; }
		public bool RecoverMode { get; set; }

		public bool IsDying { get; set; }
		public bool Die { get; set; }

		#region LastTickInfo
		public ulong LastTickHomeID { get; set; }
		public ulong LastTickBondedWorkArchID { get; set; }
		public bool LastTickInDay { get; set; }
		#endregion

		#region Getters
		public bool IsHungry => VitHelper.IsHungry;
		public float CurVitProportion => VitHelper.CurVitProportion;
		public bool InDay => LogicTimeMgr.Inst.IsDay;


		public Coord Coord => Impler.Coord;
		public ulong LogicTime => LogicTimeMgr.Inst.TickSum;


		public int MOVE_INTERVAL_NORMAL => ConfigMgr.Config.VILL_ONE_MOVE_TICK_NORMAL;
		public int MOVE_INTERVAL_DYING => ConfigMgr.Config.VILL_ONE_MOVE_TICK_DYING;


		public bool NightToDay => LogicTimeMgr.Inst.IsDay && !LastTickInDay;
		public bool DayToNight => !LogicTimeMgr.Inst.IsDay && LastTickInDay;
		public bool WorkArchChanged => LastTickBondedWorkArchID != Impler.BondArchHelper.BondedWorkArchID;
		public bool HomeChanged => LastTickHomeID != Impler.BondArchHelper.HomeID;

		public float LowVitThreshold => ConfigMgr.Config.VitConfig.LowVitThreshold;
		public float HungryVitThreshold => ConfigMgr.Config.VitConfig.HungryVitThreshold;
		public float RecoverVitThreshold => ConfigMgr.Config.VitConfig.RecoverVitThreshold;
		public float HungryProdLoss => ConfigMgr.Config.VitConfig.HungryProdLoss;
		public float VitPerFood => ConfigMgr.Config.VitConfig.VitPerFood;
		public float MaxVit => ConfigMgr.Config.VitConfig.MaxVit;
		public float TickFoodCons => ConfigMgr.Config.VitConfig.TickFoodCons;
		public float TickDayVitCons => ConfigMgr.Config.VitConfig.TickDayVitCons;
		public float TickDyingVitCons => ConfigMgr.Config.VitConfig.TickDyingVitCons;
		#endregion


		public void Clear() { }

		public MyBlackBoardSave GetSave() {
			return new() {
				MoveRoute = MoveRoute,
				CurMoveIndex = CurMoveIndex,
				LastMoveTime = LastMoveTime,

				RecoverChance = RecoverChance,
				RecoverMode = RecoverMode,

				IsDying = IsDying,
				Die = Die,

				LastTickHomeID = LastTickHomeID,
				LastTickBondedWorkArchID = LastTickBondedWorkArchID,
				LastTickInDay = LastTickInDay
			};
		}
		public void InitFromSave(MyBlackBoardSave save) {
			MoveRoute = save.MoveRoute ?? new List<Coord>();
			CurMoveIndex = save.CurMoveIndex;
			LastMoveTime = save.LastMoveTime;

			RecoverChance = save.RecoverChance;
			RecoverMode = save.RecoverMode;

			IsDying = save.IsDying;
			Die = save.Die;

			LastTickHomeID = save.LastTickHomeID;
			LastTickBondedWorkArchID = save.LastTickBondedWorkArchID;
			LastTickInDay = save.LastTickInDay;
		}
	}
}