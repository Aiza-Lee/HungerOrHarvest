using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.WorldEdge {
	/// <summary>
	/// 记录村庄边界范围，主要用于界定村民的随机移动范围。
	/// </summary>
	public class WorldEdgeResource : IResource {
		/// <summary>
		/// 左下角的坐标。
		/// </summary>
		public OL MinOL = ConstMgr.WORLD_CENTER_OL;

		/// <summary>
		/// 右上角的坐标。
		/// </summary>
		public OL MaxOL = ConstMgr.WORLD_CENTER_OL;

		private int Width => ConstMgr.DEFAULT_WORLD_EDGE_WIDTH;

		public void UpdateEdge(OL archOL) {
			if (archOL.ODR - Width < MinOL.ODR) { MinOL.ODR = Mathf.Max(archOL.ODR - Width, ConstMgr.MIN_ORDER); }
			if (archOL.ODR + Width > MaxOL.ODR) { MaxOL.ODR = Mathf.Min(archOL.ODR + Width, ConstMgr.MAX_ORDER); }
			if (archOL.LYR - Width < MinOL.LYR) { MinOL.LYR = Mathf.Max(archOL.LYR - Width, ConstMgr.MIN_LAYER); }
			if (archOL.LYR + Width > MaxOL.LYR) { MaxOL.LYR = Mathf.Min(archOL.LYR + Width, ConstMgr.MAX_LAYER); }
		}
	}
}