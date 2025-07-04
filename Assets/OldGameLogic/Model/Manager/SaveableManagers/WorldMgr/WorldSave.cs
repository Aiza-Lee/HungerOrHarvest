using UnityEngine;
using System.Collections.Generic;
using OldGameLogic.Model.Element.Vill;
using OldGameLogic.Model.Element.Arch;
using OldGameLogic.Model.Element.Layer;

namespace OldGameLogic
{
	[System.Serializable]
	public class WorldSave {
		[SerializeReference] public List<LayerSaveBase> LayerSaves;
		[SerializeReference] public List<ArchSaveBase> ArchSaves;
		[SerializeReference] public List<VillSaveBase> VillSaves;
		public List<Pair<int, Pair<int, int>>> OL_Range;
		public int MaxUnlockedLayer;
		public int MinUnlockedLayer;
	}
}