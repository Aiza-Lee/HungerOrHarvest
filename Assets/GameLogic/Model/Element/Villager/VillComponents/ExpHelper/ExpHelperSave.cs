using System;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	[Serializable]
	public class ExpHelperSave {
		[HideInInspector] public JTListSave<int> JobLevel;
		[HideInInspector] public JTListSave<float> JobExps;
		public ExpHelperSave Clone() {
			return new() {
				JobLevel 	= JobLevel.Clone(),
				JobExps 	= JobExps.Clone(),
			};
		}
	}
}