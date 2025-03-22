using NSFrame;

namespace GameLogic
{
	public class StaMachine : ISaveable<StaMachineSave> {
		public StaMachine() {
			EventSystem.AddListener((int)LogicEvt.Tick, Execute);
		}

		private StaBase _curSta;
		private VillLogicBase _vill;


		public StaType CurSta => _curSta.StaType;
		public VillLogicBase AttachedVill => _vill;


		private void Execute() {
			_curSta.Execute();
		}


		#region PublicMethods
		public void SetOwner(VillLogicBase vill) { _vill = vill; }
		public void SetStaByType(StaType staType) {
			SetSta(LogicFctry.Inst.NewSta(staType));
		}
		public void SetSta(StaBase sta) {
			sta.SetStaMachine(this);
			_curSta?.Exit();
			_curSta = sta;
			_curSta.Enter();
		}
		#endregion


		#region  ISaveable
		public StaMachineSave GetSave() {
			return new StaMachineSave() {
				CurStaSave = _curSta.GetSave(),
			};
		}
		public void InitFromSave(StaMachineSave save) {
			if (save.CurStaSave != null) {
				SetSta(LogicFctry.Inst.LoadSta(save.CurStaSave));
			} else {
				SetSta(LogicFctry.Inst.NewSta(StaType.Spare));
			}
		}
		#endregion

	}
}