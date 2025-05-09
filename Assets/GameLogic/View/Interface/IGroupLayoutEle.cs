using System;
using UnityEngine;

namespace GameLogic.View.UI
{
	public interface IGroupLayoutEle {
		GroupLayoutBase BelongedGroup { get; set;}
		float EleSize { get; }
		RectTransform RectTrans { get; }
		/// <summary>
		/// 当元素的长度发生变化时，调用此方法
		/// </summary>
		event Action OnDirty;
		void SetPos(float x);
		void OnAddedToGroup();
		void Clear();
	}
}