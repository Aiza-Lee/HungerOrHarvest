using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Common.Logic.Utils {
	public static class RouteGenerator {

		private static readonly int[] _randomOrder = new int[3];

		public static List<Coord> GetRoute(Coord start, Coord end) {
			if (start.IsOnSameEdge(end)) {
				if (start == end) { return new(); } else { return new() { end }; }
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
				// 如果 cur 和 end 在同一条边上，那么 cur 到 end 的路径就是 end
				if (cur.IsOnSameEdge(end)) {
					// 但是要保证 end 是可到达的
					if (!end.IsOL() || end.Y >= cur.Y) {
						var res = ReconstructPath(cameFrom, cur, addStart: false);
						if (cur != end) {
							res.Add(end);
						}
						return res;
					}
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

		private static List<Coord> ReconstructPath(Dictionary<Coord, Coord> cameFrom, Coord cur, bool addStart) {
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