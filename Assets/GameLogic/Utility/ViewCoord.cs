using UnityEngine;

namespace GameLogic
{
	public static class ViewCoord {
		public static Vector3 ToViewCoord(this Coord coord) {
			return new(
				ViewConstMgr.VX_MX_RATE * coord.X, 
				ViewConstMgr.DEFAULT_Y, 
				ViewConstMgr.VZ_MY_RATE * coord.Y
			);
		}

		public static Vector3 ToViewCoord(this OL ol) {
			return ol.ToCoord().ToViewCoord();
		}
	}
}