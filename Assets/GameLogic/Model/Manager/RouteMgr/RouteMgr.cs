using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public class RouteMgr {
		private RouteMgr() {}
		public static RouteMgr Inst { get; } = new();
		
		private readonly int[] _randomOrder = new int[3];

		public Coord GetRandomVillSpareCoord() {
			var spare = ConstMgr.VILL_SPARE_ORD_RADIUS;
			var odr = Random.Range(WorldMgr.Inst.MinArchODR - spare, WorldMgr.Inst.MaxArchODR + 1 + spare);
			var lyr = Random.Range(WorldMgr.Inst.MinUnlockedLayer, WorldMgr.Inst.MaxUnlockedLayer + 1);
			var middle = new OL(odr, lyr).ToCoord();
			var nbrs = middle.GetNeighborOLs();
			var opt = Random.Range(0, nbrs.Count);
			var direction = middle.DirectionTo(nbrs[opt].ToCoord());
			if (direction.X != 0) {
				return new Coord(middle.X + direction.X * Random.Range(0, ConstMgr.X_PER_ODR), middle.Y);
			} else {
				return new Coord(middle.X, middle.Y + direction.Y * Random.Range(0, ConstMgr.Y_PER_LYR));
			}
		}

		public List<Coord> GetRoute(Coord start, Coord end) {
			if (start.OnSameEdge(end)) {
				return new(){ end };
			}
			
			var q = new PriorityQueue<Coord>();
			var cost = new Dictionary<Coord, int>();
			var cameFrom = new Dictionary<Coord, Coord>();
			q.Enqueue(start, 0 + start.DistanceTo(end));
			cost[start] = 0;

			while (q.Count != 0) {
				var top = q.Dequeue();
				var cur = top.Value;
				var dis = cost[cur];
				if (cur.OnSameEdge(end)) {
					var res = ReconstructPath(cameFrom, cur, addStart: true);
					if (cur != end) {
						res.Add(end);
					}
					return res;
				}

				var nbrOLs = top.Value.GetNeighborOLs();
				var neighbors = new List<Coord>();
				foreach (var nbrOL in nbrOLs) { neighbors.Add(nbrOL.ToCoord()); }

				var cnt = nbrOLs.Count;
				for (int i = 0; i < cnt; i++) { _randomOrder[i] = i; }
				for (int i = 0; i < cnt - 1; i++) {
					int r = Random.Range(i, cnt);
					(_randomOrder[i], _randomOrder[r]) = (_randomOrder[r], _randomOrder[i]);
				}


				for (int i = 0; i < cnt; i++) {
					var neighbor = neighbors[_randomOrder[i]];
					var newCost = dis + cur.DistanceTo(neighbor);
					if (!cost.ContainsKey(neighbor) || newCost < cost[neighbor]) {
						cameFrom[neighbor] = cur;
						cost[neighbor] = newCost;
						q.Enqueue(neighbor, newCost + neighbor.DistanceTo(end));
					}
				}
			}

			throw new System.Exception("No path found!");
		}

		private List<Coord> ReconstructPath(Dictionary<Coord, Coord> cameFrom, Coord cur, bool addStart) {
			var path = new List<Coord>();
			while (cameFrom.ContainsKey(cur)) {
				path.Add(cur);
				cur = cameFrom[cur];
			}
			if (addStart) {
				path.Add(cur);
			}
			path.Reverse();
			return path;
		}
	}
}