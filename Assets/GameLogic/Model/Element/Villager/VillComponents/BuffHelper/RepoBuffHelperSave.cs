using System.Collections.Generic;
using System.Linq;

namespace GameLogic.Model.Element.Vill {
	[System.Serializable]
	public class RepoBuffHelperSave {
		public List<RepoBuffAdderSave> Adders;
		public RTListSave<float> ProdBuffs_F;
		public RTListSave<float> ConsBuffs_F;
		public RepoBuffHelperSave Clone() {
			return new() {
				Adders = Adders.Select(sv => sv.Clone()).ToList(),
				ProdBuffs_F = ProdBuffs_F.Clone(),
				ConsBuffs_F = ConsBuffs_F.Clone(),
			};
		}
	}
}