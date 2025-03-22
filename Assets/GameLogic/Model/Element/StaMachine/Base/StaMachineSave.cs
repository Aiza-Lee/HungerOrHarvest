using UnityEngine;

namespace GameLogic
{
	[System.Serializable]
	public class StaMachineSave {
		[SerializeReference] public StaSaveBase CurStaSave;
		
		public StaMachineSave Clone() {
			return new StaMachineSave() {
				CurStaSave = CurStaSave?.Clone(),
			};
		}
	}
}