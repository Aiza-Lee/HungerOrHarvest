using OldGameLogic.Model.Element.Arch;
using OldGameLogic.Model.Element.Layer;
using OldGameLogic.Model.Element.Vill;
using UnityEngine;

namespace OldGameLogic.Model.Factory
{
	/// <summary>
	/// 逻辑层工厂配置，用于配置逻辑层工厂创建实例的默认值
	/// </summary>
	// [CreateAssetMenu(fileName = "LogicFctryConfig", menuName = "HungerOrHarvest/Config/Fctry/Logic Fctry")]
	public class LogicFctryConfig : ScriptableObject {


		[Space][Space][Space] [Header("Default Vill Exp Helper Save")]
		[Space] public ExpHelperSave DefaultVillExpHelperSave;
		
		[Space][Space][Space] [Header("Default Vill Vit Helper Save")]
		[Space] public VitHelperSave DefaultVillVitHelperSave;
		[Space][Space][Space] [Header("Default Vill BondArch Helper Save")]
		[Space] public BondArchHelperSave DefaultBondArchHelperSave;
		[Space][Space][Space] [Header("Default Vill RepoBuff Helper Save")]
		[Space] public RepoBuffHelperSave DefaultRepoBuffHelperSave;
		

		[Space]
		[Space][Space] [Header("Default Vill Save")]
		[Space] public NormalVillSave DefaultNormalVill;


		
		[Space][Space][Space] [Header("Default Arch Save")]
		[Space] public CottageSave DefaultCottage;
		[Space] public RuinSave DefaultRuin;
		[Space] public HunterCabinSave DefaultHunterCabin;



		[Space]
		[Space][Space] [Header("Default Layer Save")]
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
				ArchType.Ruin => DefaultRuin,
				ArchType.HunterCabin => DefaultHunterCabin,
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

