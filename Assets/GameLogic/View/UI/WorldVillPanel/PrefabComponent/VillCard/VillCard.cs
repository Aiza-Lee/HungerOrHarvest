using System;
using NSFrame;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.View.UI.WorldVillPanel
{
	/// <summary>
	/// 村民卡片
	/// </summary>
	public class VillCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IGroupLayoutEle {
		[SerializeField] private TextMeshProUGUI _nameText;
		[SerializeField] private Image _image;
		[SerializeField] private RectTransform _expandMask;
		[SerializeField] private GameObject _lightingEdge;
		[SerializeField] private JobInfoLayout _jobInfoLayout;

		private SmoothOffsetMin _expandMaskSOMin;
		private SmoothOffsetMax _expandMaskSOMax;
		private SmoothOffsetMin _thisSOMin;
		private SmoothOffsetMax _thisSOMax;
		private SmoothScale _smoothScale;
		private MainPanel _mainPanel;

		private VillViewBase _villView;
		private bool _isFocused;
		public const float MAX_WIDTH = 500f;
		public const float MIN_WIDTH = 130f;
		public const float HEIGHT = 260f;
		
		public ulong AttachedVillID => _villView.Logic.ID;
		public bool Selected { get; private set; }

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
			UIMgr.Inst.FindPanel(out _mainPanel);
		}
		private void Update() {
			if (_villView == null) return;
		}

		#region Injection
		public VillCard InjectVillView(VillViewBase villView) {
			_villView = villView;
			_nameText.text = villView.Logic.FirstName + villView.Logic.LastName;
			_image.sprite = villView.Sprite;
			return this;
		}
		#endregion

		private void Expand() {
			_jobInfoLayout.OnExpand(AttachedVillID);
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
			_villView = null;
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
				Controller.CmdRunner.Run("/cam-free");
				_isFocused = false;
			}
		}
		#endregion
		#region IPointerClickHandler
		public void OnPointerClick(PointerEventData _) {
			if (Input.GetKey(KeyCode.LeftControl)) {
				if (_isFocused) {
					Controller.CmdRunner.Run("/cam-free");
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
				Controller.CmdRunner.Run("/cam-focus-vill " + _villView.Logic.ID);
				_isFocused = true;
			}
		}
		#endregion
	}
}