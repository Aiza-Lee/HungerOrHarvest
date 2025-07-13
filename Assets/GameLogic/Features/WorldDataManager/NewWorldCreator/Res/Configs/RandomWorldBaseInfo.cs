using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.WorldDataManager {
	/// <summary>
	/// 随机世界基础信息配置，计划是手动创建多个配置文件，
	/// 供随机世界生成器在创建新世界时随机选择一个作为基础信息
	/// </summary>
	[CreateAssetMenu(fileName = "RandomWorldBaseInfo", menuName = "HungerOrHarvest/Config/RandomWorldBaseInfo", order = 1)]
	public class RandomWorldBaseInfo : ScriptableObject {
		[Tooltip("解锁的建筑种类")] public List<ArchType> UnlockedArchs;
		[Tooltip("layer类型(自动设置中间那一层是地图中心所在层)")] public List<LayerType> Layers;

		[Tooltip("解锁的Repo -> 资源上限")] public ReadOnlyEtList<RepoType, float> UnlockedRepos;
		[Tooltip("初始资源Repo种类 -> 资源数量")] public ReadOnlyEtList<RepoType, float> InitialRepos;
		
		[Tooltip("arch类型 -> 所在位置(相对于地图中心)")] public ReadOnlyEtList<ArchType, OL> Archs;
		[Tooltip("vill类型 -> 所在位置(相对于地图中心)")] public ReadOnlyEtList<VillType, OL> Vills;
	}
}