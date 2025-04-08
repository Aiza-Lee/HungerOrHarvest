using NSFrame;

namespace GameLogic 
{
	public class LogicFctry : MonoSingleton<LogicFctry> {

		protected override void Awake() {
			base.Awake();
			PoolSystem.InitObjectPool<MoveToTask>();
			PoolSystem.InitObjectPool<WorkTask>();
			PoolSystem.InitObjectPool<SleepTask>();
		}

		public LogicFctryConfig Config;

		#region Vill
		/// <summary>
		/// 根据保存数据创建一个新的Vill，并初始化为保存数据的值
		/// </summary>
		public VillLogicBase LoadVill(VillSaveBase save) {
			var vill = NewEmptyVill(save.VillType);
			vill.InitFromSave(save);

			EventSystem.Invoke<VillLogicBase>((int)LogicEvt.VillAdded_V_1, vill);

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

		#region VillTaskRunner

		public VillTaskRunner LoadVillTaskRunner(VillLogicBase vill, VillTaskRunnerSave save) {
			var runner = new VillTaskRunner(vill);
			runner.InitFromSave(save);
			return runner;
		}

		public VillTaskRunner NewVillTaskRunner(VillLogicBase vill) {
			return LoadVillTaskRunner(vill, Config.DefaultVillTaskRunnerSave.Clone());
		}

		#endregion

		#region Task

		public TaskBase LoadTask(TaskSaveBase save) {
			var task = NewEmptyTask(save.TaskType);
			task.InitFromSave(save);
			return task;
		}

		public MoveToTask NewMoveToTask(Coord target) {
			var save = Config.DefaultMoveToTaskSave.Clone() as MoveToTaskSave;
				save.Target = target;
			var task = PoolSystem.PopObj<MoveToTask>();
			task.InitFromSave(save);
			return task;
		}
		public SleepTask NewSleepTask(ulong homeID) {
			var save = Config.DefaultSleepTaskSave.Clone() as SleepTaskSave;
				save.HomeID = homeID;
			var task = PoolSystem.PopObj<SleepTask>();
			task.InitFromSave(save);
			return task;
		}
		public WorkTask NewWorkTask(ulong archID) {
			var save = Config.DefaultWorkTaskSave.Clone() as WorkTaskSave;
				save.WorkArchId = archID;
			var task = PoolSystem.PopObj<WorkTask>();
			task.InitFromSave(save);
			return task;
		}

		private TaskBase NewEmptyTask(TaskType type) {
			return type switch {
				TaskType.MoveTo => PoolSystem.PopObj<MoveToTask>(),
				TaskType.Sleep => PoolSystem.PopObj<SleepTask>(),
				TaskType.Work => PoolSystem.PopObj<WorkTask>(),
				_ => throw new System.NotImplementedException(),
			};
		}


		#endregion


		#region Arch
		/// <summary>
		/// 根据保存数据创建一个新的Arch，并初始化为保存数据的值
		/// </summary>
		public ArchLogicBase LoadArch(ArchSaveBase save) {
			var arch = NewEmptyArch(save.ArchType);
			arch.InitFromSave(save.Clone());

			EventSystem.Invoke<ArchLogicBase>((int)LogicEvt.ArchAdded_A_1, arch);

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
			
			EventSystem.Invoke<ArchLogicBase>((int)LogicEvt.ArchAdded_A_1, arch);

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
				ArchType.Ruin => new RuinLogic(),
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

			EventSystem.Invoke<LayerLogicBase>((int)LogicEvt.LayerAdded_L_1, layer);

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

			EventSystem.Invoke<LayerLogicBase>((int)LogicEvt.LayerAdded_L_1, layer);

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