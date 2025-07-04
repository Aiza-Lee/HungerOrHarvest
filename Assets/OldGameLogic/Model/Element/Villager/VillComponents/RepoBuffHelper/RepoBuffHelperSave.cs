using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OldGameLogic.Model.Element.Vill {
	[System.Serializable]
	public class RepoBuffHelperSave {
		[HideInInspector] public List<RepoBuffAdderSave> Adders;
		[HideInInspector] public RTListSave<float> ProdBuffs_F;
		[HideInInspector] public RTListSave<float> ConsBuffs_F;
		public RepoBuffHelperSave Clone() {
			return new() {
				Adders = Adders.Select(sv => sv.Clone()).ToList(),
				ProdBuffs_F = ProdBuffs_F.Clone(),
				ConsBuffs_F = ConsBuffs_F.Clone(),
			};
		}
	}
}