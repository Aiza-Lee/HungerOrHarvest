using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.World;
using UnityEngine;

namespace GameLogic.Features.WorldEdge {
	public static class WorldEdgeAPI {
		public static Coord GetRandomCoordInWorldEdge() {
			var res = GameWorldMono.MainWorld.GetResource<WorldEdgeResource>();
			var ord = Random.Range(res.MinOL.ODR, res.MaxOL.ODR + 1);
			var lyr = Random.Range(res.MinOL.LYR, res.MaxOL.LYR + 1);
			return new OL(ord, lyr).ToCoord();
		}
	}
}