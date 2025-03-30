using System.Collections.Generic;
using NSFrame;

namespace GameLogic
{
	public abstract class ArchLogicBase : ISaveable<ArchSaveBase> {

		public ArchLogicBase() {
			EventSystem.AddListener((int)LogicEvt.Tick, UpdateRepo);
		}
		private OL _ol;
		private ulong _id;
		private int _level;
		private List<ulong> _bookedPosVillIDs;
		private List<ulong> _inVillIDs;
		private RTList<float> _consBuffs_F;
		private RTList<float> _prodBuffs_F;


		public abstract ArchType ArchType { get; }
		public OL OL => _ol;
		public ulong ID => _id;
		public int Level => _level;
		public List<ulong> BookedPosVills => _bookedPosVillIDs;
		public List<ulong> InVills => _inVillIDs;
		public Coord Coord => _ol.ToCoord();

		public ArchConfigBase Config { get; private set; }
		public ArchLevelConfigBase Lconfig => Config.LevelConfigs[_level];

		private void UpdateRepo() {
			if (RepoMgr.Inst.TryArchCons(Lconfig.InherentConsVels, _consBuffs_F)) {
				RepoMgr.Inst.ArchProd(Lconfig.InherentProdVels, _prodBuffs_F);
			}
		}

		#region PublicMethods
		public virtual void Destroy() {
			foreach (var vID in _inVillIDs) {
				WorldMgr.Inst.FindVill(vID)?.GoSpare();
			}
			EventSystem.RemoveListener((int)LogicEvt.Tick, UpdateRepo);
			EventSystem.Invoke<ArchLogicBase>((int)LogicEvt.ArchDestroyed_A, this);
		}

		public bool CheckCapacity() {
			return _inVillIDs.Count + _bookedPosVillIDs.Count < Lconfig.MaxContain;
		}
		public bool TryBookPos(ulong vID) {
			if (CheckCapacity()) {
				_bookedPosVillIDs.Add(vID);
				return true;
			}
			return false;
		}
		public bool VillDisbook(ulong vID) {
			if (_bookedPosVillIDs.Remove(vID)) {
				return true;
			} else {
				return false;
			}
		}
		public bool VillArrive(ulong vID) {
			if (_bookedPosVillIDs.Remove(vID)) {
				_inVillIDs.Add(vID);
				return true;
			} else {
				return false;
			}
		}
		public virtual bool VillLeave(ulong vID) {
			return _inVillIDs.Remove(vID);
		}
		public virtual void LevelUp() {
			_level++;
		}
		#endregion


		#region ISaveable
		protected abstract ArchSaveBase GetDerivedSave();
		public ArchSaveBase GetSave() {
			var save = GetDerivedSave();
				save.ArchType 			= ArchType;
				save.ID 				= _id;
				save.OL 				= _ol;
				save.Level 				= _level;
				save.ConsBuffs 			= _consBuffs_F.Clone();
				save.ProdBuffs 			= _prodBuffs_F.Clone();
				save.BookedPosVillIDs 	= new(_bookedPosVillIDs);
				save.InVillIDs 			= new(_inVillIDs);
			return save;
		}

		protected abstract void DerivedInitFromSave(ArchSaveBase save);
		public void InitFromSave(ArchSaveBase save) {
			DerivedInitFromSave(save);
			Config 				= ConstMgr.Inst.Config.FindConfig(save.ArchType);
			_ol 				= save.OL;
			_id 				= save.ID;
			_level 				= save.Level;
			_consBuffs_F 		= save.ConsBuffs.ConvertToFull();
			_prodBuffs_F 		= save.ProdBuffs.ConvertToFull();
			_bookedPosVillIDs 	= save.BookedPosVillIDs;
			_inVillIDs 			= save.InVillIDs;
		}
		#endregion
	}
}