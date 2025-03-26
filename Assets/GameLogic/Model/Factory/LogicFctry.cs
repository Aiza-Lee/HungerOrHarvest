using NSFrame;

namespace GameLogic 
{
	public class LogicFctry : MonoSingleton<LogicFctry> {

		protected override void Awake() {
			base.Awake();
			PoolSystem.InitObjectPool<SleepSta>();
			PoolSystem.InitObjectPool<WorkSta>();
			PoolSystem.InitObjectPool<SpareSta>();
		}

		public LogicFctryConfig Config;

		#region Vill
		/// <summary>
		/// 根据保存数据创建一个新的Vill，并初始化为保存数据的值
		/// </summary>
		public VillLogicBase LoadVill(VillSaveBase save) {
			var vill = NewEmptyVill(save.VillType);
			vill.InitFromSave(save.Clone());

			EventSystem.Invoke<VillLogicBase>((int)LogicEvt.VillAdded_V, vill);

			return vill;
		}
		/// <summary>
		/// 根据类型创建一个新的Vill，并初始化为默认值
		/// </summary>
		public VillLogicBase NewVill(VillType type, OL ol) {
			var save = Config.GetDefaultVillSave(type).Clone();
				save.ID = IDMgr.Inst.GetID();
				save.Coord = ol.ToCoord();
			return LoadVill(save);
		}

		private VillLogicBase NewEmptyVill(VillType type) {
			return type switch {
				VillType.Normal => new NormalVillLogic(),
				_ => throw new System.NotImplementedException(),
			};
		}
		#endregion

		#region StaMachine
		/// <summary>
		/// 根据保存数据创建一个新的StaMachine，并初始化为保存数据的值
		/// </summary>
		public StaMachine LoadStaMachine(VillLogicBase vill, StaMachineSave save) {
			var sm = new StaMachine(vill);
			sm.InitFromSave(save);
			return sm;
		}
		/// <summary>
		/// 根据类型创建一个新的StaMachine，并初始化为默认值
		/// </summary>
		public StaMachine NewStaMachine(VillLogicBase vill) {
			var save = Config.DefaultStaMachine.Clone();
			return LoadStaMachine(vill, save);
		}
		#endregion

		#region Sta
		/// <summary>
		/// 根据保存数据创建一个新的Sta，并初始化为保存数据的值
		/// </summary>
		public StaBase LoadSta(StaSaveBase save) {
			var type = save.StaType;
			StaBase sta = type switch {
				StaType.Sleep => PoolSystem.PopObj<SleepSta>(),
				StaType.Work => PoolSystem.PopObj<WorkSta>(),
				StaType.Spare => PoolSystem.PopObj<SpareSta>(),
				_ => throw new System.NotImplementedException(),
			};
			sta.InitFromSave(save);
			return sta;
		}

		public StaBase NewSta(StaType type) {
			StaBase sta = type switch {
				StaType.Work => PoolSystem.PopObj<WorkSta>(),
				StaType.Sleep => PoolSystem.PopObj<SleepSta>(),
				StaType.Spare => PoolSystem.PopObj<SpareSta>(),
				_ => throw new System.NotImplementedException(),
			};
			StaSaveBase save = type switch {
				StaType.Work => Config.DefaultWorkSta.Clone(),
				StaType.Sleep => Config.DefaultSleepSta.Clone(),
				StaType.Spare => Config.DefaultSpareSta.Clone(),
				_ => throw new System.NotImplementedException(),
			};
			sta.InitFromSave(save);
			return sta;
		}

		#endregion


		#region Arch
		/// <summary>
		/// 根据保存数据创建一个新的Arch，并初始化为保存数据的值
		/// </summary>
		public ArchLogicBase LoadArch(ArchSaveBase save) {
			var arch = NewEmptyArch(save.ArchType);
			arch.InitFromSave(save.Clone());

			EventSystem.Invoke<ArchLogicBase>((int)LogicEvt.ArchAdded_A, arch);

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
			
			EventSystem.Invoke<ArchLogicBase>((int)LogicEvt.ArchAdded_A, arch);

			return arch;
		}
		private ArchLogicBase NewEmptyArch(ArchType type) {
			return type switch {
				ArchType.Cottage => new CottageLogic(),
				ArchType.Farm => throw new System.NotImplementedException(),
				ArchType.LumberMill => throw new System.NotImplementedException(),
				ArchType.Quarry => throw new System.NotImplementedException(),
				ArchType.Mine => throw new System.NotImplementedException(),
				ArchType.Fishery => throw new System.NotImplementedException(),
				ArchType.Well => throw new System.NotImplementedException(),
				ArchType.Windmill => throw new System.NotImplementedException(),
				ArchType.Ochard => throw new System.NotImplementedException(),
				ArchType.Warehouse => throw new System.NotImplementedException(),
				ArchType.Blacksmith => throw new System.NotImplementedException(),
				ArchType.Workshop => throw new System.NotImplementedException(),
				ArchType.Garden => throw new System.NotImplementedException(),
				ArchType.Fountain => throw new System.NotImplementedException(),
				ArchType.Statue => throw new System.NotImplementedException(),
				ArchType.Ruins => throw new System.NotImplementedException(),
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

			EventSystem.Invoke<LayerLogicBase>((int)LogicEvt.LayerAdded_L, layer);

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

			EventSystem.Invoke<LayerLogicBase>((int)LogicEvt.LayerAdded_L, layer);

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

		// public VillLogicBase NewVill(VillSaveBase save = null, bool newID = false) {
		// 	if (newID) {
		// 		save.ID = IDMgr.Inst.GetID();
		// 	}
		// 	var prefab = Config.GetVillPrefab(save.VillType);
		// 	var go = GameObject.Instantiate(prefab);
		// 	if (!go.TryGetComponent<VillLogicBase>(out var vill)) {
		// 		Debug.LogError("error");
		// 		return null;
		// 	}
		// 	if (save != null) {
		// 		go.name = save.LastName + save.FirstName;
		// 		vill.InitFromSave(save);
		// 	} else {
		// 		go.name = DefaultVill.LastName + DefaultVill.FirstName;
		// 		vill.InitFromSave(DefaultVill);
		// 	}
		// 	EventSystem.Invoke<VillLogicBase>((int)LogicEvt.VillCreated_V, vill);
		// 	return vill;
		// }


		// public ArchLogicBase NewArch(ArchSaveBase save, bool newID = false) {
		// 	if (newID) {
		// 		save.ID = IDMgr.Inst.GetID();
		// 	}
		// 	var prefab = Config.GetArchPrefab(save.ArchType);
		// 	var go = GameObject.Instantiate(prefab);
		// 	if (!go.TryGetComponent<ArchLogicBase>(out var arch)) {
		// 		Debug.LogError("error");
		// 		return null;
		// 	}
		// 	go.name = save.ArchType.ToString();
		// 	arch.InitFromSave(save);
		// 	EventSystem.Invoke<ArchLogicBase>((int)LogicEvt.ArchCreated_A, arch);
		// 	return arch;
		// }

		// public LayerLogicBase NewLayer(LayerSaveBase save, bool newID = false) {
		// 	if (newID) {
		// 		save.ID = IDMgr.Inst.GetID();
		// 	}
		// 	var prefab = Config.GetLayerPrefab(save.LayerType);
		// 	var go = GameObject.Instantiate(prefab);
		// 	if (!go.TryGetComponent<LayerLogicBase>(out var layer)) {
		// 		Debug.LogError("error");
		// 		return null;
		// 	}
		// 	go.name = save.LayerType.ToString();
		// 	layer.InitFromSave(save);
		// 	EventSystem.Invoke<LayerLogicBase>((int)LogicEvt.LayerCreated_L, layer);
		// 	return layer;
		// }

	}
}