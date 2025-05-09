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
		/// 元素的起始边的位置
		/// </summary>
		private float StartingEdge 
			=> base._direction switch {
				Direction.LeftToRight => _eleContainer.offsetMin.x,
				Direction.UpToDown => _eleContainer.offsetMax.y,
				_ => default,
			};
		/// <summary>
		/// 遮罩在排列方向的大小
		/// </summary>
		private float MaskSize
			=> base._direction switch {
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
				// 如果元素的容器大小小于遮罩的大小，则将元素的起始边设置为0
				// 如果元素的容器大小大于遮罩的大小，则将元素的起始边限制在遮罩的大小内
				if (EleContainerSize <= MaskSize) {
					if (!StartingEdge.IsApproximatelyEqual(0f)) SetStartingEdgePos(0f);
				} else {
					if (StartingEdge > 0f) SetStartingEdgePos(0f);
					else if (StartingEdge < MaskSize - EleContainerSize) SetStartingEdgePos(MaskSize - EleContainerSize);
				}
			}
			
		}

		private void UpdateScroll() {
			if (_eles.Count == 0) return;
			if (Input.mouseScrollDelta.y != 0) {
				SetStartingEdgePos(StartingEdge + Input.mouseScrollDelta.y * _scrollSpeed * Time.unscaledDeltaTime);
				_isScrolling = true;
			} else {
				_isScrolling = false;
			}
		} 
		private void SetStartingEdgePos(float pos) {
			if (_direction == Direction.LeftToRight) {
				// _eleContainer.offsetMin = new(pos, _eleContainer.offsetMin.y);
				_eleContainerSOMin.SetTarget(new(pos, _eleContainerSOMin.CurVal.y));
			} else if (_direction == Direction.UpToDown) {
				// _eleContainer.offsetMax = new(_eleContainer.offsetMax.x, pos);
				_eleContainerSOMax.SetTarget(new(_eleContainerSOMax.CurVal.x, pos));
			}
		}

		#region IPointer
		public void OnPointerEnter(PointerEventData eventData) {
			_isPointerIn = true;
		}
		public void OnPointerExit(PointerEventData eventData) {
			_isPointerIn = false;
		}
		#endregion
	}
}