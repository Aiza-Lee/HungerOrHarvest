using UnityEngine;

namespace GameLogic
{
	[CreateAssetMenu(fileName = "LogicFctryConfig", menuName = "HungerOrHarvest/Config/Fctry/Logic Fctry")]
	public class LogicFctryConfig : ScriptableObject {


		[Space][Space][Space] [Header("Default Sta Mahcine Save")] 
		[Space] public StaMachineSave DefaultStaMachine;



		[Space][Space][Space] [Header("Default Sta Save")]
		[Space] public SpareStaSave DefaultSpareSta;
		[Space] public SleepStaSave DefaultSleepSta;
		[Space] public WorkStaSave DefaultWorkSta;



		[Space][Space][Space] [Header("Default Vill Save")]
		[Space] public NormalVillSave DefaultNormalVill;


		
		[Space][Space][Space] [Header("Default Arch Save")]
		[Space] public CottageSave DefaultCottage;



		[Space][Space][Space] [Header("Default Layer Save")]
		[Space] public GrassLayerSave DefaultGrassLayerSave;
		[Space] public SnowLayerSave DefaultSnowLayerSave;
		[Space] public SeaEndLayerSave DefaultSeaEndLayerSave;
		[Space] public SnowMountainEndLayerSave DefaultSnowMountainEndLayerSave;
		[Space] public WasteLandLayerSave DefaultWasteLandLayerSave;
		[Space] public BeachLayerSave DefaultBeachLayerSave;


		public VillSaveBase GetDefaultVillSave(VillType type) {
			return type switch {
				VillType.Normal => DefaultNormalVill,
				_ => throw new System.NotImplementedException(),
			};
		}

		public ArchSaveBase GeDefaultArchSave(ArchType type) {
			return type switch {
				ArchType.Cottage => DefaultCottage,
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

		public LayerSaveBase GetDefaultLayerSave(LayerType type) {
			return type switch {
				LayerType.Grass => DefaultGrassLayerSave,
				LayerType.Snow => DefaultSnowLayerSave,
				LayerType.SeaEnd => DefaultSeaEndLayerSave,
				LayerType.SnowMountainEnd => DefaultSnowMountainEndLayerSave,
				LayerType.WasteLand => DefaultWasteLandLayerSave,
				LayerType.Beach => DefaultBeachLayerSave,
				_ => throw new System.NotImplementedException(),
			};
		}
	}
}

