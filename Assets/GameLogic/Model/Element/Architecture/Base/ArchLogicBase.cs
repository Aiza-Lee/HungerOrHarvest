using System.Collections.Generic;
using NSFrame;

namespace GameLogic
{
	public abstract class ArchLogicBase : ISaveable<ArchSaveBase> {

		public ArchLogicBase() {
			EventSystem.AddListener((int)LogicEvt.Tick, UpdateRepo);
		}
		~ArchLogicBase() {
			EventSystem.RemoveListener((int)LogicEvt.Tick, UpdateRepo);
		}

		private OL _ol;
		private ulong _id;
		private int _level;
		private List<VillLogicBase> _inVills;
		private RTList<float> _consBuffs_F;
		private RTList<float> _prodBuffs_F;


		public abstract ArchType ArchType { get; }
		public OL OL => _ol;
		public ulong ID => _id;
		public int Level => _level;
		public List<VillLogicBase> InVills => _inVills;
		public RTList<float> ConsBuffs_F => _consBuffs_F;
		public RTList<float> ProdBuffs_F => _prodBuffs_F;

		public ArchConfigBase Config { get; private set; }
		private ArchLevelConfigBase Lconfig => Config.LevelConfigs[_level];


		#region Public Methods
		public virtual void AddVill(VillLogicBase vill) {
			_inVills.Add(vill);
		}
		public virtual void RemoveVill(VillLogicBase vill) {
		}
		public virtual void Destroy() {}
		public virtual void LevelUp() {}
		#endregion


		private void UpdateRepo() {
			if (RepoMgr.Inst.TryArchCons(Lconfig.InherentConsVels, _consBuffs_F)) {
				RepoMgr.Inst.ArchProd(Lconfig.InherentProdVels, _prodBuffs_F);
			}
			foreach (var vill in _inVills) {
				if (RepoMgr.Inst.TryVillCons(Lconfig.ExtraConsVelsPerOne, _consBuffs_F, vill.ConsBuffs)) {
					RepoMgr.Inst.VillProd(Lconfig.ExtraProdVelsPerOne, _prodBuffs_F, vill.ProdBuffs);
					vill.AddExp(Lconfig.ExpAdds);
				}
			}
		}

		protected abstract ArchSaveBase GetDerivedSave();
		public ArchSaveBase GetSave() {
			var save = GetDerivedSave();
				save.ArchType = ArchType;
				save.ID = _id;
				save.OL = _ol;
				save.Level = _level;
				save.ConsBuffs = _consBuffs_F.Clone();
				save.ProdBuffs = _prodBuffs_F.Clone();
				save.InVillIDs = new();
			foreach (var v in _inVills) {
				save.InVillIDs.Add(v.ID);
			}
			return save;
		}

		protected abstract void DerivedInitFromSave(ArchSaveBase save);
		public void InitFromSave(ArchSaveBase save) {
			DerivedInitFromSave(save);
			Config = ConstMgr.Inst.Config.FindConfig(save.ArchType);
			_ol = save.OL;
			_id = save.ID;
			_level = save.Level;
			_consBuffs_F = save.ConsBuffs.ConvertToFull();
			_prodBuffs_F = save.ProdBuffs.ConvertToFull();
			_inVills = new();
			foreach (var id in save.InVillIDs) {
				_inVills.Add(WorldMgr.Inst.FindVill(id));
			}
		}
	}
}