using System;
using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public abstract class VillSaveBase {
		public string TypeName;
		public VillType VillType => Enum.Parse<VillType>(TypeName);

		public LogicImplerSave LogicImpler;
		

		protected abstract VillSaveBase GetDerivedClone();
		public VillSaveBase Clone() {
			var save = GetDerivedClone();
				save.TypeName 		= TypeName;
				save.LogicImpler 	= LogicImpler.Clone();
			return save;
		}
	}
}