using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	[Serializable]
	public class VitHelperSave {
		public float CurVit;
		[HideInInspector] public List<BuffAdderSave> ConsBuffAdders;
		[HideInInspector] public List<BuffAdderSave> RecoverBuffAdders;

		public VitHelperSave Clone() {
			return new() {
				CurVit = CurVit,
				ConsBuffAdders = ConsBuffAdders.Select((x) => x.Clone()).ToList(),
				RecoverBuffAdders = RecoverBuffAdders.Select((x) => x.Clone()).ToList(),
			};
		}
	}
}