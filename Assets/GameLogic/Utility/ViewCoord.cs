using GameLogic.View;
using UnityEngine;

namespace GameLogic.Utilities
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

		public static float GetBackLyrZ(this Vector3 vec3) {
			return Mathf.FloorToInt(vec3.z / ViewConstMgr.LayerGap) * ViewConstMgr.LayerGap;
		}
	}
}