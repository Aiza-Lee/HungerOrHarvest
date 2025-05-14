using System;
using System.Collections;
using Codice.CM.Common;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.View.UI.PopUpPanels.ScreenEdgePanel
{
	[RequireComponent(typeof(SmoothOffsetMin), typeof(SmoothOffsetMax))]
	public abstract class ScreenEdgePanelBase : PanelBase {

		private RectTransform _rectTrans;
		private SmoothOffsetMin _SOMin;
		private SmoothOffsetMax _SOMax;
		protected override void Awake() {
			base.Awake();
			_rectTrans = GetComponent<RectTransform>();
			_SOMin = GetComponent<SmoothOffsetMin>();
			_SOMax = GetComponent<SmoothOffsetMax>();
		}

		public enum EdgeType {
			Top, Bottom, Left, Right
		}
		[SerializeField] private EdgeType _edgePopFrom;
		[SerializeField] private float _distanceToEdge;
		[SerializeField] private TextMeshProUGUI _tip;
		public void SetTipText(string tip) => _tip.text = tip;
		private float _originalSize;

		private void ClosePanelImpl() {
			// 窗口的返回动画
			_SOMin.SetTarget(_edgePopFrom switch {
				EdgeType.Top => new(_SOMin.CurVal.x, 0),
				EdgeType.Bottom => new(_SOMin.CurVal.x, -_originalSize),
				EdgeType.Left => new(-_originalSize, _SOMin.CurVal.y),
				EdgeType.Right => new(0, _SOMin.CurVal.y),
				_ => throw new NotImplementedException(),
			});
			_SOMax.SetTarget(_edgePopFrom switch {
				EdgeType.Top => new(_SOMax.CurVal.x, _originalSize),
				EdgeType.Bottom => new(_SOMax.CurVal.x, 0),
				EdgeType.Left => new(0, _SOMax.CurVal.y),
				EdgeType.Right => new(_originalSize, _SOMax.CurVal.y),
				_ => throw new NotImplementedException(),
			});
			StartCoroutine(DoWait(1, () => {
				this.Toggle();
				_tip.text = "";
			}));
		}
		private IEnumerator DoWait(float sec, Action callback) {
			yield return new WaitForSecondsRealtime(sec);
			callback.Invoke();
		}


		/// <summary>
		/// 指定多少秒后关闭
		/// </summary>
		/// <param name="sec">延迟时间</param>
		public void CloseAfterSeconds(float sec) {
			StartCoroutine(DoWait(sec, () => ClosePanelImpl()));
		}
		
		/// <summary>
		/// 关闭该窗口的方法，如果从外部直接调用Toggle不会触发返回动画而是直接关闭
		/// </summary>
		public void ClosePanel() {
			ClosePanelImpl();
		}

		abstract protected void OnClose_Derived();
		public override void OnClose() {
			OnClose_Derived();
		}

		abstract protected void OnShow_Derived();
		public override void OnShow() {
			// 设置锚点位置
			_rectTrans.anchorMin = _edgePopFrom switch {
				EdgeType.Top => new(0, 1),
				EdgeType.Bottom => new(0, 0),
				EdgeType.Left => new(0, 0),
				EdgeType.Right => new(1, 0),
				_ => throw new NotImplementedException(),
			};
			_rectTrans.anchorMax = _edgePopFrom switch {
				EdgeType.Top => new(1, 1),
				EdgeType.Bottom => new(1, 0),
				EdgeType.Left => new(0, 1),
				EdgeType.Right => new(1, 1),
				_ => throw new NotImplementedException(),
			};
			// 记录原始大小
			_originalSize = _edgePopFrom switch {
				EdgeType.Top or EdgeType.Bottom => _rectTrans.rect.height,
				EdgeType.Left or EdgeType.Right => _rectTrans.rect.width,
				_ => throw new NotImplementedException(),
			};
			// 设置初始位置
			_SOMin.SetCurVal(_edgePopFrom switch {
				EdgeType.Top => new(_SOMin.CurVal.x, 0),
				EdgeType.Bottom => new(_SOMin.CurVal.x, -_originalSize),
				EdgeType.Left => new(-_originalSize, _SOMin.CurVal.y),
				EdgeType.Right => new(0, _SOMin.CurVal.y),
				_ => throw new NotImplementedException(),
			});
			_SOMax.SetCurVal(_edgePopFrom switch {
				EdgeType.Top => new(_SOMax.CurVal.x, _originalSize),
				EdgeType.Bottom => new(_SOMax.CurVal.x, 0),
				EdgeType.Left => new(0, _SOMax.CurVal.y),
				EdgeType.Right => new(_originalSize, _SOMax.CurVal.y),
				_ => throw new NotImplementedException(),
			});
			// 设置目标位置
			_SOMin.SetTarget(_edgePopFrom switch {
				EdgeType.Top => new(_SOMin.CurVal.x, -_originalSize - _distanceToEdge),
				EdgeType.Bottom => new(_SOMin.CurVal.x, _distanceToEdge),
				EdgeType.Left => new(_distanceToEdge, _SOMin.CurVal.y),
				EdgeType.Right => new(-_originalSize - _distanceToEdge, _SOMin.CurVal.y),
				_ => throw new NotImplementedException(),
			});
			_SOMax.SetTarget(_edgePopFrom switch {
				EdgeType.Top => new(_SOMax.CurVal.x, -_distanceToEdge),
				EdgeType.Bottom => new(_SOMax.CurVal.x, _originalSize + _distanceToEdge),
				EdgeType.Left => new(_originalSize + _distanceToEdge, _SOMax.CurVal.y),
				EdgeType.Right => new(-_distanceToEdge, _SOMax.CurVal.y),
				_ => throw new NotImplementedException(),
			});
		
			OnShow_Derived();
		}

		
	}
}