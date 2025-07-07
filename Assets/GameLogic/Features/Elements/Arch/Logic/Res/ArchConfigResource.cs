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
	}

}