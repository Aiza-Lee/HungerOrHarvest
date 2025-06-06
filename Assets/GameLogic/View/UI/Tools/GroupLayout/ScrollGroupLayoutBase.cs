using NSFrame;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLogic.View.UI
{
	public abstract class ScrollGroupLayoutBase : GroupLayoutBase, IPointerEnterHandler, IPointerExitHandler {
		[SerializeField] private float _scrollSpeed = 100;
		[SerializeField] private RectTransform _maskRectTrans;

		private bool _isPointerIn;
		private bool _isScrolling;

		/// <summary>
		/// 元素的起始边的位置（排列方向为正方向）
		/// </summary>
		private float StartEdge => base._direction switch {
			Direction.LeftToRight => _eleContainer.offsetMin.x,
			Direction.UpToDown => -_eleContainer.offsetMax.y,
			_ => default,
		};
		/// <summary>
		/// 遮罩在排列方向的大小
		/// </summary>
		private float MaskSize => base._direction switch {
			Direction.LeftToRight => _maskRectTrans.rect.width,
			Direction.UpToDown => _maskRectTrans.rect.height,
			_ => default,
		};


		private void Update() {
			// 如果指针没有进入或者没有滚动的必要就不触发滚动的更新
			if (_isPointerIn) {
				UpdateScroll();
			}

			if (!_isScrolling) {
				if (EleContainerSize <= MaskSize) {
					if (!StartEdge.IsApproximatelyEqual(0f)) SetStartEdgePos(0f);
				} else {
					if (StartEdge > 0f) SetStartEdgePos(0f);
					else if (StartEdge + EleContainerSize < MaskSize) SetStartEdgePos(MaskSize - EleContainerSize);
				}
			}

		}

		private void UpdateScroll() {
			if (_eles.Count == 0) return;
			if (Input.mouseScrollDelta.y != 0) {
				SetStartEdgePos(StartEdge + Input.mouseScrollDelta.y * _scrollSpeed * Time.unscaledDeltaTime);
				_isScrolling = true;
			} else {
				_isScrolling = false;
			}
		} 
		private void SetStartEdgePos(float pos) {
			if (_direction == Direction.LeftToRight) {
				_eleContainerSOMin.SetTarget(new(pos, _eleContainerSOMin.CurVal.y));
			} else if (_direction == Direction.UpToDown) {
				_eleContainerSOMax.SetTarget(new(_eleContainerSOMax.CurVal.x, -pos));
			}
		}

		#region IPointer
		public void OnPointerEnter(PointerEventData _) {
			_isPointerIn = true;
		}
		public void OnPointerExit(PointerEventData _) {
			_isPointerIn = false;
		}
		#endregion
	}
}