using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.WorldEdge {
	/// <summary>
	/// 记录村庄边界范围，主要用于界定村民和相机的随机移动范围。
	/// </summary>
	public class WorldEdgeResource : IResource {
		/// <summary>
		/// 左下角的坐标。
		/// </summary>
		public OL ArchMinOL = ConstMgr.WORLD_CENTER_OL;

		/// <summary>
		/// 右上角的坐标。
		/// </summary>
		public OL ArchMaxOL = ConstMgr.WORLD_CENTER_OL;

		private int Width => ConstMgr.DEFAULT_WORLD_EDGE_WIDTH;

		public void UpdateArchEdge(OL archOL) {
			if (archOL.ODR - Width < ArchMinOL.ODR) { ArchMinOL.ODR = Mathf.Max(archOL.ODR - Width, ConstMgr.MIN_ORDER); }
			if (archOL.ODR + Width > ArchMaxOL.ODR) { ArchMaxOL.ODR = Mathf.Min(archOL.ODR + Width, ConstMgr.MAX_ORDER); }
			if (archOL.LYR - Width < ArchMinOL.LYR) { ArchMinOL.LYR = Mathf.Max(archOL.LYR - Width, ConstMgr.MIN_LAYER); }
			if (archOL.LYR + Width > ArchMaxOL.LYR) { ArchMaxOL.LYR = Mathf.Min(archOL.LYR + Width, ConstMgr.MAX_LAYER); }
		}

		/// <summary>
		/// 限制相机移动的层统计
		/// </summary>
		public int LayerMin = ConstMgr.WORLD_CENTER_OL.LYR;
		/// <summary>
		/// 限制相机移动的层统计
		/// </summary>
		public int LayerMax = ConstMgr.WORLD_CENTER_OL.LYR;

		public void UpdateLayerRange(int layer) {
			if (layer < LayerMin) { LayerMin = layer; }
			if (layer > LayerMax) { LayerMax = layer; }
		}
	}
}