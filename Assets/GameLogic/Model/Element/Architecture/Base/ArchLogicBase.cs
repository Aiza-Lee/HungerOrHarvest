using System;
using System.Collections.Generic;
using GameLogic.Model.Element.Vill;
using NSFrame;

namespace GameLogic.Model.Element.Arch
{
	public abstract class ArchLogicBase : ISaveable<ArchSaveBase>, IBondVill {
		public abstract ArchType ArchType { get; }

		public ArchLogicBase() {
			EventSystem.AddListener((int)ModelEvt.Tick_0, UpdateRepo, EventType.Model);
		}

		public OL OL { get; private set; }
		public ulong ID { get; private set; }
		public int Level { get; private set; }
		public List<ulong> InVillIDs { get; private set; }
		public List<ulong> BondedVillIDs { get; private set; }
		public RTList<float> ConsBuffs_F { get; private set; }
		public RTList<float> ProdBuffs_F { get; private set; }
		public Coord Coord => OL.ToCoord();
		public int BondedVillCount => BondedVillIDs.Count;

		public ArchConfigBase Config { get; private set; }
		public ArchLevelConfigBase Lconfig => Config.LevelConfigs[Level];

		private void UpdateRepo() {
			if (RepoMgr.Inst.TryArchCons(Lconfig.InherentConsVels, ConsBuffs_F)) {
				RepoMgr.Inst.ArchProd(Lconfig.InherentProdVels, ProdBuffs_F);
			}
		}

		#region Event
		/// <summary>
		/// 当建筑被销毁时触发，且在建筑内部实现的消除逻辑之前
		/// </summary>
		public event Action<ArchLogicBase> OnArchDestroyed;
		#endregion

		#region IBondVill
		public bool CheckBondVill() => BondedVillIDs.Count < Lconfig.MaxContain;
		public bool HasBondedVill(ulong vID) => BondedVillIDs.Contains(vID);
		public bool BondVill(VillLogicBase vill) {
			if (!CheckBondVill()) return false;
			BondedVillIDs.Add(vill.ID);
			return true;
		}
		public bool DisBondVill(VillLogicBase vill) {
			if (!HasBondedVill(vill.ID)) { return false; }
			BondedVillIDs.Remove(vill.ID);
			return true;
		}
		#endregion

		#region PublicMethods
		protected abstract void Destroy_Derived();
		public void Destroy() {
			OnArchDestroyed?.Invoke(this);
			OnArchDestroyed = null;
			Destroy_Derived();
			EventSystem.RemoveListener((int) ModelEvt.Tick_0, UpdateRepo, EventType.Model);
			EventSystem.Invoke<ArchLogicBase>((int) ModelEvt.ArchDestroyed_A_1, this, EventType.Model);
		}
		public bool VillArrive(ulong vID) {
			if (!BondedVillIDs.Contains(vID)) { return false; }
			if (InVillIDs.Contains(vID)) { return true; }
			InVillIDs.Add(vID);
			EventSystem.Invoke<ulong, ulong>((int) ModelEvt.VillArriveArch_VuAu_2, vID, ID, EventType.Model);
			return true;
		}
		public bool VillLeave(ulong vID) {
			if (InVillIDs.Remove(vID)) {
				EventSystem.Invoke<ulong, ulong>((int) ModelEvt.VillLeaveArch_VuAu_2, vID, ID, EventType.Model);
				return true;
			} else {
				return false;
			}
		}
		#endregion


		#region ISaveable
		protected abstract ArchSaveBase GetSave_Derived();
		public ArchSaveBase GetSave() {
			var save = GetSave_Derived();
				save.ArchType 		= ArchType;
				save.ID 			= ID;
				save.OL 			= OL;
				save.Level 			= Level;
				save.ConsBuffs 		= ConsBuffs_F.Clone();
				save.ProdBuffs 		= ProdBuffs_F.Clone();
				save.BondedVillIDs 	= new(BondedVillIDs);
				save.InVillIDs 		= new(InVillIDs);
			return save;
		}

		protected abstract void InitFromSave_Derived(ArchSaveBase save);
		public void InitFromSave(ArchSaveBase save) {
			InitFromSave_Derived(save);
			Config 			= ConstMgr.Inst.Config.FindArchConfig(save.ArchType);
			OL 			= save.OL;
			ID 			= save.ID;
			Level 			= save.Level;
			ConsBuffs_F 	= save.ConsBuffs.ConvertToFull();
			ProdBuffs_F 	= save.ProdBuffs.ConvertToFull();
			BondedVillIDs 	= save.BondedVillIDs;
			InVillIDs 		= save.InVillIDs;
		}
		#endregion
	}
}