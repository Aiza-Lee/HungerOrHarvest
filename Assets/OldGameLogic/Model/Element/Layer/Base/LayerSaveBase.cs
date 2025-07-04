using UnityEngine;

namespace OldGameLogic.Model.Element.Layer
{
	[System.Serializable]
	public abstract class LayerSaveBase {
		public abstract LayerType LayerType { get; }
		[HideInInspector] public ulong ID;
		[HideInInspector] public int LYR;
		
		protected abstract LayerSaveBase GetDerivedClone();
		public LayerSaveBase Clone() {
			var save = GetDerivedClone();
				save.ID 		= ID;
				save.LYR 		= LYR;
			return save;
		}
	}
}