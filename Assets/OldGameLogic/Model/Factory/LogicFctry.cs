using OldGameLogic.Model.Element.Arch;
using OldGameLogic.Model.Element.Layer;
using OldGameLogic.Model.Element.Vill;
using NSFrame;

namespace OldGameLogic.Model.Factory {
	/// <summary>
	/// 逻辑层工厂，用于创建逻辑层对象。包括 村民，村民任务，村民任务执行器，建筑，层 等对象
	/// </summary>
	public class LogicFctry : MonoSingleton<LogicFctry> {

		protected override void Awake() {
			base.Awake();
		}

		public LogicFctryConfig Config;

		#region Vill

		/// <summary>
		/// 根据保存数据创建一个新的Vill，并初始化为保存数据的值
		/// </summary>
		public VillLogicBase LoadVill(VillSaveBase save) {
			var vill = NewEmptyVill(save.VillType);
			vill.InitFromSave(save);

			EventSystem.Invoke<VillLogicBase>((int) ModelEvt.VillAdded_V_1, vill, NSFrame.EventType.Model);

			return vill;
		}
		/// <summary>
		/// 根据类型创建一个新的Vill，并初始化为默认值
		/// </summary>
		public VillLogicBase NewVill(VillType type, OL ol) {
			var save = Config.GetDefaultVillSave(type).Clone();
			save.LogicImpler.ID = IDMgr.Inst.GetID();
			save.LogicImpler.Coord = ol.ToCoord();
			return LoadVill(save);
		}

		private VillLogicBase NewEmptyVill(VillType type) {
			return type switch {
				VillType.Normal => new NormalVillLogic(),
				_ => throw new System.NotImplementedException(),
			};
		}
		#endregion
			#region VillExpHelper
		public ExpHelper NewVillExpHelper(LogicImpler logicImpler) {
			var helper = new ExpHelper(logicImpler);
			helper.InitFromSave(Config.DefaultVillExpHelperSave.Clone());
			return helper;
		}
		#endregion
		#region VillVitHelper
		public VitHelper NewVillVitHelper(LogicImpler logicImpler) {
			var helper = new VitHelper(logicImpler);
			helper.InitFromSave(Config.DefaultVillVitHelperSave.Clone());
			return helper;
		}
		#endregion
		#region BondArchHelper
		public BondArchHelper NewBondArchHelper(LogicImpler logicImpler) {
			var helper = new BondArchHelper(logicImpler);
			helper.InitFromSave(Config.DefaultBondArchHelperSave.Clone());
			return helper;
		}
		#endregion
		#region RepoBuffHelper
		public RepoBuffHelper NewVillRepoBuffHelper(LogicImpler logicImpler) { 
			var helper = new RepoBuffHelper(logicImpler);
			helper.InitFromSave(Config.DefaultRepoBuffHelperSave.Clone());
			return helper;
		}
		#endregion

		#region Arch
		/// <summary>
		/// 根据保存数据创建一个新的Arch，并初始化为保存数据的值
		/// </summary>
		public ArchLogicBase LoadArch(ArchSaveBase save) {
			var arch = NewEmptyArch(save.ArchType);
			arch.InitFromSave(save.Clone());

			EventSystem.Invoke<ArchLogicBase>((int) ModelEvt.ArchAdded_A_1, arch, NSFrame.EventType.Model);

			return arch;
		}
		/// <summary>
		/// 根据类型创建一个新的Arch，并初始化为默认值
		/// </summary>
		public ArchLogicBase NewArch(ArchType type, OL ol) {
			var arch = NewEmptyArch(type);

			var save = Config.GeDefaultArchSave(type).Clone();
			save.ID = IDMgr.Inst.GetID();
			save.OL = ol;

			arch.InitFromSave(save);

			EventSystem.Invoke<ArchLogicBase>((int) ModelEvt.ArchAdded_A_1, arch, NSFrame.EventType.Model);

			return arch;
		}
		private ArchLogicBase NewEmptyArch(ArchType type) {
			return type switch {
				ArchType.Cottage => new CottageLogic(),
				ArchType.Ruin => new RuinLogic(),
				ArchType.HunterCabin => new HunterCabinLogic(),
				_ => throw new System.NotImplementedException(),
			};
		}
		#endregion



		#region Layer
		/// <summary>
		/// 根据类型创建一个新的Layer，并初始化为默认值
		/// </summary>
		public LayerLogicBase LoadLayer(LayerSaveBase save) {
			var layer = NewEmptyLayer(save.LayerType);
			layer.InitFromSave(save.Clone());

			EventSystem.Invoke<LayerLogicBase>((int) ModelEvt.LayerAdded_L_1, layer, NSFrame.EventType.Model);

			return layer;
		}
		/// <summary>
		/// 根据类型创建一个新的Layer，并初始化为默认值
		/// </summary>
		public LayerLogicBase NewLayer(LayerType type, int lyr) {
			var layer = NewEmptyLayer(type);
			var save = Config.GetDefaultLayerSave(type).Clone();
			save.ID = IDMgr.Inst.GetID();
			save.LYR = lyr;
			layer.InitFromSave(save);

			EventSystem.Invoke<LayerLogicBase>((int) ModelEvt.LayerAdded_L_1, layer, NSFrame.EventType.Model);

			return layer;
		}
		private LayerLogicBase NewEmptyLayer(LayerType type) {
			return type switch {
				LayerType.Grass => new GrassLayerLogic(),
				LayerType.Snow => new SnowLayerLogic(),
				LayerType.WasteLand => new WasteLandLayerLogic(),
				LayerType.SeaEnd => new SeaEndLayerLogic(),
				LayerType.Beach => new BeachLayerLogic(),
				LayerType.SnowMountainEnd => new SnowMountainEndLayerLogic(),
				_ => throw new System.NotImplementedException(),
			};
		}
		#endregion
	}
}