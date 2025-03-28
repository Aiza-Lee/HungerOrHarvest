// using NSFrame;

// namespace GameLogic
// {
// 	public class StaMachine : ISaveable<StaMachineSave> {
// 		public StaMachine(VillLogicBase vill) {
// 			_vill = vill;
// 			EventSystem.AddListener((int)LogicEvt.Tick, Execute);
// 		}
// 		~StaMachine() {
// 			EventSystem.RemoveListener((int)LogicEvt.Tick, Execute);
// 		}
// 		private readonly VillLogicBase _vill;

// 		private StaBase _curSta;


// 		public StaType CurStaType => _curSta.StaType;
// 		public VillLogicBase AttachedVill => _vill;


// 		private void Execute() {
// 			_curSta.Execute();
// 		}
// 		private void PushCurStaToPool() {
// 			if (_curSta == null) return;
// 			switch (_curSta.StaType) {
// 				case StaType.Work: PoolSystem.PushObj(_curSta as WorkSta); break;
// 				case StaType.Sleep: PoolSystem.PushObj(_curSta as SleepSta); break;
// 				case StaType.Spare: PoolSystem.PushObj(_curSta as SpareSta); break;
// 			}
// 		}


// 		#region PublicMethods
// 		public void SetStaByType(StaType staType) {
// 			SetSta(LogicFctry.Inst.NewSta(staType));
// 		}
// 		public void SetSta(StaBase sta) {
// 			sta.SetStaMachine(this);
// 			_curSta?.Exit();
// 			PushCurStaToPool();
// 			_curSta = sta;
// 			if (!_curSta.Entered) { _curSta.Enter(); }
// 		}
// 		#endregion


// 		#region  ISaveable
// 		public StaMachineSave GetSave() {
// 			return new StaMachineSave() {
// 				CurStaSave = _curSta.GetSave(),
// 			};
// 		}
// 		public void InitFromSave(StaMachineSave save) {
// 			if (save != null && save.CurStaSave != null) {
// 				SetSta(LogicFctry.Inst.LoadSta(save.CurStaSave));
// 			} else {
// 				SetSta(LogicFctry.Inst.NewSta(StaType.Spare));
// 			}
// 		}
// 		#endregion

// 	}
// }