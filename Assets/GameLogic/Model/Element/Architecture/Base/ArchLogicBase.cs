using System.Collections.Generic;
using NSFrame;

namespace GameLogic
{
	public abstract class ArchLogicBase : ISaveable<ArchSaveBase> {

		public ArchLogicBase() {
			EventSystem.AddListener((int)LogicEvt.Tick_0, UpdateRepo);
		}
		private OL _ol;
		private ulong _id;
		private int _level;
		private List<ulong> _inVillIDs;
		private List<ulong> _bondedVillIDs;
		private RTList<float> _consBuffs_F;
		private RTList<float> _prodBuffs_F;


		public abstract ArchType ArchType { get; }
		public OL OL => _ol;
		public ulong ID => _id;
		public int Level => _level;
		public List<ulong> InVills => _inVillIDs;
		public List<ulong> BondedVills => _bondedVillIDs;
		public RTList<float> ConsBuffs_F => _consBuffs_F;
		public RTList<float> ProdBuffs_F => _prodBuffs_F;
		public Coord Coord => _ol.ToCoord();
		public int BondedVillCount => _bondedVillIDs.Count;

		public ArchConfigBase Config { get; private set; }
		public ArchLevelConfigBase Lconfig => Config.LevelConfigs[_level];

		private void UpdateRepo() {
			if (RepoMgr.Inst.TryArchCons(Lconfig.InherentConsVels, _consBuffs_F)) {
				RepoMgr.Inst.ArchProd(Lconfig.InherentProdVels, _prodBuffs_F);
			}
		}
		private bool CheckCapacity() {
			return _bondedVillIDs.Count < Lconfig.MaxContain;
		}

		#region PublicMethods

		protected abstract void Destroy_Derived();
		public void Destroy() {
			foreach (var vID in _bondedVillIDs) {
				WorldMgr.Inst.FindVill(vID).OnBondedArchDestroyed();
			}
			Destroy_Derived();
			EventSystem.RemoveListener((int)LogicEvt.Tick_0, UpdateRepo);
			EventSystem.Invoke<ArchLogicBase>((int)LogicEvt.ArchDestroyed_A_1, this);
		}

		public bool TryBondVill(ulong vID) {
			if (_bondedVillIDs.Exists(v => v == vID)) { return true; }
			if (CheckCapacity()) {
				_bondedVillIDs.Add(vID);
				return true;
			}
			return false;
		}
		public bool TryDisbondVill(ulong vID) {
			if (_bondedVillIDs.Remove(vID)) {
				return true;
			} else {
				return false;
			}
		}
		public bool VillArrive(ulong vID) {
			if (_bondedVillIDs.Contains(vID)) {
				_inVillIDs.Add(vID);
				EventSystem.Invoke<ulong, ulong>((int)LogicEvt.VillArriveArch_VuAu_2, vID, _id);
				return true;
			} else {
				return false;
			}
		}
		public bool VillLeave(ulong vID) {
			if (_inVillIDs.Remove(vID)) {
				EventSystem.Invoke<ulong, ulong>((int)LogicEvt.VillLeaveArch_VuAu_2, vID, _id);
				return true;
			} else {
				return false;
			}
		}
		public void LevelUp() {
			_level++;
		}
		#endregion


		#region ISaveable
		protected abstract ArchSaveBase GetSave_Derived();
		public ArchSaveBase GetSave() {
			var save = GetSave_Derived();
				save.ArchType 		= ArchType;
				save.ID 			= _id;
				save.OL 			= _ol;
				save.Level 			= _level;
				save.ConsBuffs 		= _consBuffs_F.Clone();
				save.ProdBuffs 		= _prodBuffs_F.Clone();
				save.BondedVillIDs 	= new(_bondedVillIDs);
				save.InVillIDs 		= new(_inVillIDs);
			return save;
		}

		protected abstract void InitFromSave_Derived(ArchSaveBase save);
		public void InitFromSave(ArchSaveBase save) {
			InitFromSave_Derived(save);
			Config 			= ConstMgr.Inst.Config.FindConfig(save.ArchType);
			_ol 			= save.OL;
			_id 			= save.ID;
			_level 			= save.Level;
			_consBuffs_F 	= save.ConsBuffs.ConvertToFull();
			_prodBuffs_F 	= save.ProdBuffs.ConvertToFull();
			_bondedVillIDs 	= save.BondedVillIDs;
			_inVillIDs 		= save.InVillIDs;
		}
		#endregion
	}
}