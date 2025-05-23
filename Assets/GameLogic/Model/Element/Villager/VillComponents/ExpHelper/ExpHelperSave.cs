using System;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	[Serializable]
	public class ExpHelperSave {
		[HideInInspector] public JTListSave<int> JobLevel;
		[HideInInspector] public JTListSave<float> JobExps;
		[HideInInspector] public RTListSave<float> ConsBuffs;
		[HideInInspector] public RTListSave<float> ProdBuffs;
		public ExpHelperSave Clone() {
			return new() {
				JobLevel 	= JobLevel.Clone(),
				JobExps 	= JobExps.Clone(),
				ConsBuffs 	= ConsBuffs.Clone(),
				ProdBuffs 	= ProdBuffs.Clone(),
			};
		}
	}
}