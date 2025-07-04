using System.Collections.Generic;
using UnityEngine;

namespace OldGameLogic.Controller
{
	[CreateAssetMenu(fileName = "PreSetWorldConfig", menuName = "HungerOrHarvest/Config/Preset World")]
	public class PreSetConfig : ScriptableObject {
		[Space][Space] [Header("正方向层类型(包括0)")] public List<string> PosLayers;
		[Space][Space] [Header("负方向层类型")] public List<string> NegLayers;
		[Space][Space] public List<Pair<int, Pair<int, int>>> Layer_Range;
		[Space][Space] public List<Pair<string, OL>> Arch_OL;
		[Space][Space] public List<Pair<string, OL>> Vill_OL;
		[Space][Space] public List<string> UnlockedRepo;
		[Space][Space] public RTListSave<float> StartingRepoSave;

		private RTList<float> _startingRepo;
		public RTList<float> StartingRepo {
			get {
				if (_startingRepo == null) {
					_startingRepo = new();
					_startingRepo.InitFromSave(StartingRepoSave);
				}
				return _startingRepo;
			}
		}
	}
}