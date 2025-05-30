using System;
using UnityEngine;

namespace GameLogic.View.UI
{
	public interface IGroupLayoutEle {
		GroupLayoutBase BelongedGroup { get; set;}
		float EleSize { get; }
		RectTransform RectTrans { get; }
		/// <summary>
		/// 常在元素大小变化时，需要对整个group重新排列的时候触发该事件
		/// </summary>
		event Action OnDirty;
		/// <summary>
		/// 设置排列元素在排列方向上的坐标增量
		/// </summary>
		/// <param name="x">坐标增量</param>
		void SetPos(float x);
		/// <summary>
		/// 当被加入到groupLayoutBase中的时候被调用
		/// </summary>
		void OnAddedToGroup();
		void Clear();
	}
}