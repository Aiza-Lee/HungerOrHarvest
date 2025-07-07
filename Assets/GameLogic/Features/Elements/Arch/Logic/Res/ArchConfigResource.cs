using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Arch {
	[System.Serializable]
	public class ArchConfigResource : IResource {
		[SerializeReference] public List<ArchConfigBase> Configs = new();
	}

	public abstract class ArchConfigBase : ScriptableObject {
		abstract public ArchType ArchType { get; }

		public string Name;
		public float ConstructTime;
		public EtList<RepoType, float> ConstructCost;
		public float DeconstructRate;
		public float RepairRate;
		public Animator Animator;
		public List<ArchLevelConfigBase> LevelConfigs;
	}
	
	public abstract class ArchLevelConfigBase : ScriptableObject {
		[Tooltip("等级")] public int Level;
		[Tooltip("容纳人数上限")] public int MaxContain;
		[Tooltip("介绍")][TextArea(5, 30)] public string Introduction;
		[Tooltip("固有产出")] public EtList<RepoType, float> InherentProdVelsSave;
		[Tooltip("额外产出/每人")] public EtList<RepoType, float> ExtraProdVelsPerOneSave;
		[Tooltip("固有消耗")] public EtList<RepoType, float> InherentConsVelsSave;
		[Tooltip("额外消耗/每人")] public EtList<RepoType, float> ExtraConsVelsPerOneSave;
		[Tooltip("存储量增量")] public EtList<RepoType, float> VolumeAddsSave;
		[Tooltip("职业经验的增量")] public EtList<JobType, float> ExpAddsSave;
		[Tooltip("体力消耗速率")] public float VitConsRate;

	}

}