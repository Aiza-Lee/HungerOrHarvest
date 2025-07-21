using System;
using System.Collections.Generic;
using GameLogic.Common.View;
using GameLogic.UI.Common.UiComponents.SmoothChange;
using UnityEngine;

namespace GameLogic.UI.Common.UiComponents.GroupLayout {
	public abstract class GroupLayoutBase : MonoBehaviour {
		/// <summary>
		/// 这个物体的子物体就是这个group的元素
		/// </summary>
		[SerializeField] protected RectTransform _eleContainer;
		/// <summary>
		/// 元素之间的间隔距离
		/// </summary>
		[SerializeField] protected float _space;
		public enum Direction {
			LeftToRight,
			UpToDown,
		}
		[SerializeField] protected Direction _direction;


		protected SmoothOffsetMin _eleContainerSOMin;
		protected SmoothOffsetMax _eleContainerSOMax;


		public float EleContainerSize { get; private set; }

		protected RectTransform _rectTrans;
		protected virtual void Awake() {
			_rectTrans = GetComponent<RectTransform>();
			if (!_eleContainer.TryGetComponent(out _eleContainerSOMin)) {
				_eleContainerSOMin = _eleContainer.gameObject.AddComponent<SmoothOffsetMin>();
				_eleContainerSOMin.ChangeInfos = new() { new() { CurveType = ChangeCurveType.Linear, TotalTime = 0.2f } };
			}
			if (!_eleContainer.TryGetComponent(out _eleContainerSOMax)) {
				_eleContainerSOMax = _eleContainer.gameObject.AddComponent<SmoothOffsetMax>();
				_eleContainerSOMax.ChangeInfos = new() { new() { CurveType = ChangeCurveType.Linear, TotalTime = 0.2f } };
			}
		}

		protected readonly List<IGroupLayoutEle> _eles = new();

		#region PublicMethods
		public void Clear() {
			var tmpEles = new List<IGroupLayoutEle>(_eles);
			foreach (var ele in tmpEles) {
				ele.LogicDestroy();
				ele.OnDirty -= RearrangeEle;
				ele.BelongedGroup = null;
			}
			_eles.Clear();
		}
		/// <summary>
		/// 设置group的排列方向的长度
		/// </summary>
		public virtual void SetLength(float length) {
			EleContainerSize = length;
			if (_direction == Direction.LeftToRight) {
				_eleContainer.offsetMax = new(_eleContainer.offsetMin.x + length, _eleContainer.offsetMax.y);
			} else if (_direction == Direction.UpToDown) {
				_eleContainer.offsetMin = new(_eleContainer.offsetMin.x, _eleContainer.offsetMax.y - length);
			}
		}
		public virtual void RearrangeEle() {
			float pos = 0f;
			foreach (var ele in _eles) {
				ele.SetPos(pos);
				pos += ele.EleSize + _space;
			}
			SetLength(pos + _space);
		}
		/// <summary>
		/// 对group中的元素进行排序
		/// </summary>
		/// <param name="comparison">调用者提供的排序方法</param>
		public virtual void SortEle(Comparison<IGroupLayoutEle> comparison) {
			_eles.Sort(comparison);
			RearrangeEle();
		}
		/// <summary>
		/// 添加一个元素到group中
		/// </summary>
		public void AddEle(IGroupLayoutEle ele) {
			ele.RectTrans.SetParent(_eleContainer);
			_eles.Add(ele);
			ele.OnDirty += RearrangeEle;
			ele.OnAddedToGroup();
			ele.BelongedGroup = this;
			RearrangeEle();
		}
		/// <summary>
		/// 从group中移除一个元素
		/// </summary>
		public void RemoveEle(IGroupLayoutEle ele) {
			_eles.Remove(ele);
			ele.OnDirty -= RearrangeEle;
			ele.BelongedGroup = null;
			RearrangeEle();
		}
		#endregion
	}
}