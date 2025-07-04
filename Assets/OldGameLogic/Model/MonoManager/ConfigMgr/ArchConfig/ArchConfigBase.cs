using System;
using System.Collections.Generic;
using UnityEngine;

namespace OldGameLogic.Model.Mgr
{
	public abstract class ArchConfigBase : ScriptableObject {
		[Header("类型名称(区分大小写)")] public string TypeName;
		private ArchType? _archType;
		public ArchType ArchType => _archType ??= Enum.Parse<ArchType>(TypeName);
		
		[Header("名称")] public string Name;
		[Header("大小")] public int Size;
		[Header("建造时间")] public ulong ConstructTicks;
		[Header("建造费用")] public RTListSave<float> ConstructCostSave;
		[Header("拆除返还率")] public float DeconstructRate;
		[Header("修复所需原材料百分比")] public float RepairRate;
		[Header("动画")] public Animator Animator;

		[Header("每级配置")] public List<ArchLevelConfigBase> LevelConfigs;

		public ArchLevelConfigBase Level(int level) => LevelConfigs[level];

		private RTList<float> _constructCost;
		public RTList<float> ConstructCost {
			get {
				if (_constructCost == null) {
					_constructCost = new();
					_constructCost.InitFromSave(ConstructCostSave);
				}
				return _constructCost;
			}
		}
	}
}