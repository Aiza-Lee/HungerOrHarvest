using System;

namespace GameLogic.Model.Element.Vill
{
	[Serializable]
	public class ExpHelperSave {
		public JTList<int> JobLevel;
		public JTList<float> JobExps;
		public RTList<float> ConsBuffs;
		public RTList<float> ProdBuffs;
		public ExpHelperSave Clone() {
			return new() {
				JobLevel = JobLevel.Clone(),
				JobExps = JobExps.Clone(),
				ConsBuffs = ConsBuffs.Clone(),
				ProdBuffs = ProdBuffs.Clone(),
			};
		}
	}
}