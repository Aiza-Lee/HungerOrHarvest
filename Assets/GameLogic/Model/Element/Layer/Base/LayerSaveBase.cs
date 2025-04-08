using UnityEngine;

namespace GameLogic
{
	[System.Serializable]
	public abstract class LayerSaveBase {
		[HideInInspector] public ulong ID;
		public LayerType LayerType;
		[HideInInspector] public int LYR;
		
		protected abstract LayerSaveBase GetDerivedClone();
		public LayerSaveBase Clone() {
			var save = GetDerivedClone();
				save.ID = ID;
				save.LayerType = LayerType;
				save.LYR = LYR;
			return save;
		}
	}
}