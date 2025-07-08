using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[System.Serializable]
	public class ArchConfigResource : IResource {
		[SerializeReference] public List<ArchConfigBase> Configs = new();
		private Dictionary<ArchType, ArchConfigBase> _configs;
		public ArchConfigBase GetConfig(ArchType archType) {
			if (_configs == null) {
				_configs = new Dictionary<ArchType, ArchConfigBase>();
				foreach (var c in Configs) {
					_configs[c.ArchType] = c;
				}
			}
			return _configs.TryGetValue(archType, out var config) ? config : null;
		}
	}

	public abstract class ArchConfigBase : ScriptableObject {
		abstract public ArchType ArchType { get; }

		[Tooltip("建筑名称")] public string Name;
		[Tooltip("建造所需时间（Tick）")] public int ConstructTick;
		[Tooltip("建造消耗（资源类型-数量）")] public ReadOnlyEtList<RepoType, float> ConstructCost;
		[Tooltip("拆除返还比例")] public float DeconstructRate;
		[Tooltip("修复需要的资源占建造资源的比重")] public float RepairRate;
		[Tooltip("建筑动画控制器")] public Animator Animator;
		[Tooltip("各等级配置")] public List<ArchLevelConfigBase> LevelConfigs;
	}
	
	public abstract class ArchLevelConfigBase : ScriptableObject {
		[Tooltip("等级")] public int Level;
		[Tooltip("容纳人数上限")] public int MaxContain;
		[Tooltip("介绍")][TextArea(5, 30)] public string Introduction;
		[Tooltip("固有产出")] public ReadOnlyEtList<RepoType, float> InherentProdVelsSave;
		[Tooltip("额外产出/每人")] public ReadOnlyEtList<RepoType, float> ExtraProdVelsPerOneSave;
		[Tooltip("固有消耗")] public ReadOnlyEtList<RepoType, float> InherentConsVelsSave;
		[Tooltip("额外消耗/每人")] public ReadOnlyEtList<RepoType, float> ExtraConsVelsPerOneSave;
		[Tooltip("存储量增量")] public ReadOnlyEtList<RepoType, float> VolumeAddsSave;
		[Tooltip("职业经验的增量")] public ReadOnlyEtList<JobType, float> ExpAddsSave;
		[Tooltip("体力消耗速率")] public float VitConsRate;

	}

}