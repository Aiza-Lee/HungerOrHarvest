using System;
using UnityEngine;

namespace GameLogic.View.UI
{
	public interface IGroupLayoutEle {
		GroupLayoutBase BelongedGroup { get; set;}
		float EleSize { get; }
		RectTransform RectTrans { get; }
		event Action OnDirty;
		void SetPos(float x);
		void OnAddedToGroup();
	}
}