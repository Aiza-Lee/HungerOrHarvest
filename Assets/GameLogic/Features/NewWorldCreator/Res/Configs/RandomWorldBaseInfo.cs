using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.NewWorldCreator {
	/// <summary>
	/// 随机世界基础信息配置，计划是手动创建多个配置文件，
	/// 供随机世界生成器在创建新世界时随机选择一个作为基础信息
	/// </summary>
	[CreateAssetMenu(fileName = "RandomWorldBaseInfo", menuName = "HungerOrHarvest/Configs/RandomWorldBaseInfo", order = 1)]
	public class RandomWorldBaseInfo : ScriptableObject {
		[Tooltip("解锁的资源种类")] public ReadOnlyEtList<RepoType, bool> UnlockedRepos;
		[Tooltip("layer类型 -> 所在层")] public ReadOnlyEtList<LayerType, uint> Layers;
		[Tooltip("arch类型 -> 所在位置")] public ReadOnlyEtList<ArchType, OL> Archs;
		[Tooltip("vill类型 -> 所在位置")] public ReadOnlyEtList<VillType, OL> Vills;
	}
}