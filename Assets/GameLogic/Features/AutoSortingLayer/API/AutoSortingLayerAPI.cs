using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Features.Arch;
using GameLogic.Features.Layer;
using GameLogic.Features.Vill;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.AutoSortingLayer {
	public static class AutoSortingLayerAPI {
		public static void SetSortingLayerByCoordY(Entity entity, int cy) {
			SetSortingLayerByOlLyr(entity, Mathf.CeilToInt(1f * cy / ConstMgr.CY_PER_LYR));
			if (cy % ConstMgr.CY_PER_LYR != 0) {
				entity.GetComponent<SpriteRendererComponent>().SortingOrder = ConstMgr.MAX_SORTING_ORDER;
			}
		}
		public static void SetSortingLayerByOlLyr(Entity entity, int lyr) {
			var srComp = entity.GetComponent<SpriteRendererComponent>();
			srComp.SortingLayerID = SortingLayer.NameToID($"m_Layer{lyr}");
			if (entity.HasComponent<ArchIdentityComponent>()) {
				srComp.SortingOrder = ConstMgr.ARCH_SORTING_ORDER;
			} else if (entity.HasComponent<VillIdentityComponent>()) {
				srComp.SortingOrder = ConstMgr.VILL_SORTING_ORDER;
			} else if (entity.HasComponent<LayerIdentityComponent>()) {
				srComp.SortingOrder = ConstMgr.EARTH_SORTING_ORDER;
			} else {
				srComp.SortingOrder = ConstMgr.BACK_SORTING_ORDER;
			}
			srComp.MarkDirty();
		}
	}
}