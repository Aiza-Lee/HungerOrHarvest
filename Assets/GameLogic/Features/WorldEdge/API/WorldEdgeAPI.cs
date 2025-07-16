using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.World;
using UnityEngine;

namespace GameLogic.Features.WorldEdge {
	public static class WorldEdgeAPI {
		public static Coord GetRandomCoordInWorldEdge() {
			var res = GameWorldMono.MainWorld.GetResource<WorldEdgeResource>();
			var ord = Random.Range(res.ArchMinOL.ODR, res.ArchMaxOL.ODR + 1);
			var lyr = Random.Range(res.ArchMinOL.LYR, res.ArchMaxOL.LYR + 1);
			return new OL(ord, lyr).ToCoord();
		}
		public static int MaxLyr => GameWorldMono.MainWorld.GetResource<WorldEdgeResource>().LayerMax;
		public static int MinLyr => GameWorldMono.MainWorld.GetResource<WorldEdgeResource>().LayerMin;

	}
}