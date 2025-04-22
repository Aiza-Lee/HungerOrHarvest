using System;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public interface IGroupLayoutEle {
		GroupLayoutBase BelongedGroup { get; set;}
		float Width { get; }
		RectTransform RectTrans { get; }
		event Action OnDirty;
		void SetPos(float x);
		void OnAddedToGroup();
	}
}