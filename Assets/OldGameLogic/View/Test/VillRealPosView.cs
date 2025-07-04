using OldGameLogic.Model.Element.Vill;
using OldGameLogic.Utilities;
using UnityEngine;

namespace OldGameLogic
{
	public class VillRealPosView : MonoBehaviour {
		
		public VillLogicBase VillLogicBase { get; private set; }

		bool _ok = false;

		private void Update() {
			if (_ok) {
				transform.position = VillLogicBase.Coord.ToViewCoord();
			} else {
				var l = WorldMgr.Inst.GetAllVills;
				if (l.Count > 0) {
					VillLogicBase = l[0];
					_ok = true;
					return;
				}
			}
		}

	}
}