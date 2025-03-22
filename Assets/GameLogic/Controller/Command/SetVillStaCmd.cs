namespace GameLogic
{
	public class SetVillStaCmd : ICommand {
		
		private ulong _villID;
		private StaType _sta;
	
		public ICommand Init(ICmdData data) {
			var d = (SetVillStaCmdData)data;
			_villID = d.VillID;
			_sta = d.Sta;
			return this;
		}
		public bool Check() {
			var vill = WorldMgr.Inst.FindVill(_villID);
			if (vill == null || vill.CurSta == _sta) return false;
			return true;
		}
		public void Execute() {
			var vill = WorldMgr.Inst.FindVill(_villID);
			vill.CurSta = _sta;
		}
	}

	public class SetVillStaCmdData : ICmdData {
		public ulong VillID;
		public StaType Sta;
	}
}