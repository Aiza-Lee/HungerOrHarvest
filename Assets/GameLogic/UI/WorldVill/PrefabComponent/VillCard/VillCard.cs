using System;
using GameLogic.Features.Elements.Vill;
using GameLogic.Features.MainCamera;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using GameLogic.UI.Common.UiComponents.PercentBar;
using GameLogic.UI.Common.UiComponents.SmoothChange;
using GameLogic.UI.Common.UiMgr;
using NsEcsFrame.Core;
using NSFrame;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.UI.WorldVill {
	/// <summary>
	/// 村民卡片
	/// </summary>
	public class VillCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IGroupLayoutEle {
		[SerializeField] private TextMeshProUGUI _nameText;
		[SerializeField] private Image _image;
		[SerializeField] private RectTransform _expandMask;
		[SerializeField] private GameObject _lightingEdge;
		[SerializeField] private JobInfoLayout _jobInfoLayout;
		[SerializeField] private PrecentageBar _vitBar;

		private SmoothOffsetMin _expandMaskSOMin;
		private SmoothOffsetMax _expandMaskSOMax;
		private SmoothOffsetMin _thisSOMin;
		private SmoothOffsetMax _thisSOMax;
		private SmoothScale _smoothScale;
		private MainPanel _mainPanel;

		public Entity TargetVill { get; private set; }
		private bool _isFocused;
		public const float MAX_WIDTH = 500f;
		public const float MIN_WIDTH = 130f;
		public const float HEIGHT = 260f;

		public bool Selected { get; private set; }

		[SerializeField] private int _updateInterval = 3;
		private int _updataCount = 0;

		private RectTransform _rectTrans;
		private void Awake() {
			_expandMaskSOMin = _expandMask.GetComponent<SmoothOffsetMin>();
			_expandMaskSOMax = _expandMask.GetComponent<SmoothOffsetMax>();
			_thisSOMin = GetComponent<SmoothOffsetMin>();
			_thisSOMax = GetComponent<SmoothOffsetMax>();
			_smoothScale = GetComponent<SmoothScale>();
			_rectTrans = GetComponent<RectTransform>();
		}
		private void Start() {
			_mainPanel = UIMgr.Inst.FindPanel<MainPanel>();
		}

		void FixedUpdate() {
			if (TargetVill == null) return;
			if (++_updataCount < _updateInterval) return;
			_updataCount = 0;

			if (!TargetVill.IsValid()) {
				LogicDestroy();
				return;
			}

			_vitBar.SetPercentage(VillQueryAPI.GetVitPercentage(TargetVill));
		}

		#region Injection
		public VillCard SetEntity(Entity vill) {
			TargetVill = vill;
			_nameText.text = VillQueryAPI.GetName(vill);
			_image.sprite = VillQueryAPI.GetArtConfig(VillQueryAPI.GetVillType(vill)).WorldSprite;
			return this;
		}
		#endregion

		private void Expand() {
			_jobInfoLayout.OnExpand(TargetVill);
			_expandMaskSOMax
				.SetOnChanged((_) => OnDirty?.Invoke())
				.SetTarget(new(MAX_WIDTH, _expandMask.offsetMax.y));
		}
		private void Shrink() {
			_expandMaskSOMax
				.SetOnChanged((_) => OnDirty?.Invoke())
				.SetDoneCallback(() => _jobInfoLayout.OnShrinkDone())
				.SetTarget(new(MIN_WIDTH, _expandMask.offsetMax.y));
		}

		#region PublicMethods
		public void LogicDestroy() {
			TargetVill = null;
			_image.sprite = null;
			Selected = false;
			_lightingEdge.SetActive(false);
			PoolSystem.PushGO(gameObject);
		}

		/// <summary>
		/// 将该卡片移动到目标位置，并逐渐缩小到 0 后销毁
		/// </summary>
		/// <param name="target">目标位置</param>
		/// <param name="callBack">移动完成后的回调</param>
		public void TransferTo(RectTransform target, Action<VillCard> callBack) {
			_rectTrans.SetParent(target);
			_rectTrans.SetAsLastSibling();
			_thisSOMin
				.SetMod(1)
				.SetOnChanged((val) => _thisSOMax.SetCurVal(new(val.x + MIN_WIDTH, val.y + HEIGHT)))
				.SetTarget(new(0f, 0f));
			_smoothScale
				.SetDoneCallback(() => { callBack?.Invoke(this); })
				.SetTarget(new(0f, 0f, 0f));
		}
		#endregion

		#region IGroupLayoutEle
		public GroupLayoutBase BelongedGroup { get; set; }
		public RectTransform RectTrans => _rectTrans;
		public float EleSize => _expandMask.rect.width;
		public float Height => HEIGHT;
		public event Action OnDirty;
		public void SetPos(float x) {
			_thisSOMax.SetCurVal(new(x + MIN_WIDTH, _rectTrans.offsetMax.y));
			_thisSOMin.SetCurVal(new(x, _rectTrans.offsetMin.y));
		}
		public void OnAddedToGroup() {
			_smoothScale.SetCurVal(new(1, 1, 1));
			_thisSOMax.SetCurVal(new(MIN_WIDTH, HEIGHT));
			_thisSOMin.SetCurVal(new(0, 0));
			_expandMaskSOMax.SetCurVal(new(MIN_WIDTH, 0));
			_expandMaskSOMin.SetCurVal(new(0, 0));
		}
		#endregion

		#region IPointerEnterHandler
		public void OnPointerEnter(PointerEventData _) {
			if (Selected) return;
			Expand();
		}
		#endregion
		#region IPointerExitHandler
		public void OnPointerExit(PointerEventData _) {
			if (Selected) return;
			Shrink();
			if (_isFocused) {
				CameraFollowAPI.SetCemeraFollow(null);
				_isFocused = false;
			}
		}
		#endregion
		#region IPointerClickHandler
		public void OnPointerClick(PointerEventData _) {
			if (Input.GetKey(KeyCode.LeftControl)) {
				if (_isFocused) {
					CameraFollowAPI.SetCemeraFollow(null);
					_isFocused = false;
				}
				Selected = !Selected;
				_lightingEdge.SetActive(Selected);

				if (Selected) {
					_mainPanel.SelectCard(this);
					Shrink();
				} else {
					_mainPanel.DeselectCard(this);
					Expand();
				}

			} else {
				CameraFollowAPI.SetCemeraFollow(TargetVill);
				_isFocused = true;
			}
		}
		#endregion
	}
}