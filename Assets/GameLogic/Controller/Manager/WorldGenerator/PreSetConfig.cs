using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	[CreateAssetMenu(fileName = "PreSetWorldConfig", menuName = "HungerOrHarvest/Config/Preset World")]
	public class PreSetConfig : ScriptableObject {
		[Space][Space] [Header("正方向层类型(包括0)")] public List<LayerType> PosLayers;
		[Space][Space] [Header("负方向层类型")] public List<LayerType> NegLayers;
		[Space][Space] public List<Pair<int, Pair<int, int>>> Layer_Range;
		[Space][Space] public List<Pair<ArchType, OL>> Arch_OL;
		[Space][Space] public List<Pair<VillType, OL>> Vill_OL;
		[Space][Space] public List<RepoType> UnlockedRepo;
		[Space][Space] public RTList<float> StartingRepo;
	}
}