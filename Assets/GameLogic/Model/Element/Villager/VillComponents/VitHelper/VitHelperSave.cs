using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	[Serializable]
	public class VitHelperSave {
		[HideInInspector] public float CurVit;
		[HideInInspector] public VitState CurState;
		[HideInInspector] public List<VitBuffAdderSave> ConsBuffAdders;
		[HideInInspector] public List<VitBuffAdderSave> RecoverBuffAdders;

		public VitHelperSave Clone() {
			return new() {
				CurVit = CurVit,
				CurState = CurState,
				ConsBuffAdders = ConsBuffAdders.Select((x) => x.Clone()).ToList(),
				RecoverBuffAdders = RecoverBuffAdders.Select((x) => x.Clone()).ToList(),
			};
		}
	}
}