using UnityEngine;

namespace GameLogic
{
	public static class ViewCoord {
		public static Vector3 ToViewCoord(this Coord coord) {
			return new(
				ViewConstMgr.Inst.Config.VX_LX_RATE * coord.X, 
				ViewConstMgr.Inst.Config.DEFAULT_Y, 
				ViewConstMgr.Inst.Config.VZ_LY_RATE * coord.Y
			);
		}

		public static Vector3 ToViewCoord(this OL ol) {
			return ol.ToCoord().ToViewCoord();
		}
	}
}