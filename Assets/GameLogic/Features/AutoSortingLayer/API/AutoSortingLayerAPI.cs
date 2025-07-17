using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Elements.Decorations;
using GameLogic.Features.Layer;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.AutoSortingLayer {
	/// <summary>
	/// 自动设置实体的SortingLayer的API。
	/// <para>目前只是作为内部实现的Utils使用</para>
	/// </summary>
	public static class AutoSortingLayerAPI {
		public static void Set_SortingOrderAndLayer_ByCoord(Entity entity, Coord coord) {
			// 在layer之间行动时，把layer设置为较大的lyr（被前面的遮挡），然后根据coord.y设置sortingOrder
			Set_SortingLayer_ByOlLyr(entity, Mathf.CeilToInt(1f * coord.Y / ConstMgr.CY_PER_LYR));
			var srComp = entity.GetComponent<SpriteRendererComponent>();
			if (coord.Y % ConstMgr.CY_PER_LYR != 0) {
				srComp.SortingOrder = ConstMgr.MAX_SORTING_ORDER + (ConstMgr.CY_PER_LYR - coord.Y % ConstMgr.CY_PER_LYR);
				srComp.MarkDirty();
			} else if (entity.HasComponent<DecorationIdentityComp>()) {
				// 同一层的按照coord.x设置sortingOrder
				srComp.SortingOrder = ConstMgr.BACK_SORTING_ORDER + coord.X;
				srComp.MarkDirty();
			} else if (entity.HasComponent<VillIdentityComponent>()) {
				srComp.SortingOrder = ConstMgr.VILL_SORTING_ORDER + coord.X;
				srComp.MarkDirty();
			}
		}
		public static void Set_SortingLayer_ByOlLyr(Entity entity, int lyr) {
			var srComp = entity.GetComponent<SpriteRendererComponent>();
			srComp.SortingLayerID = SortingLayer.NameToID($"m_Layer{lyr}");
			if (entity.HasComponent<ArchIdentityComponent>()) {
				srComp.SortingOrder = ConstMgr.ARCH_SORTING_ORDER;
			} else if (entity.HasComponent<VillIdentityComponent>()) {
				srComp.SortingOrder = ConstMgr.VILL_SORTING_ORDER;
			} else if (entity.HasComponent<LayerIdentityComponent>()) {
				srComp.SortingOrder = ConstMgr.EARTH_SORTING_ORDER;
			} else if (entity.HasComponent<DecorationIdentityComp>()) {
				srComp.SortingOrder = ConstMgr.BACK_SORTING_ORDER;
			} else {
				srComp.SortingOrder = ConstMgr.FRONT_SORTING_ORDER;
			}
			srComp.MarkDirty();
		}
	}
}