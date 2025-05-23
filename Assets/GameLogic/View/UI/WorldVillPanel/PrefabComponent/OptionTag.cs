using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class OptionTag : MonoBehaviour, IPointerClickHandler {

		[SerializeField] private Image _image;
		[SerializeField] private GameObject _unSelectableMask;

		public bool Dirty { get; set; }

		private GroupType _groupType;
		private ArchType _archType;
		private RectTransform _rectTrans;
		private MainPanel _mainPanel;

		public GroupType GroupType => _groupType;
		public ArchType ArchType => _archType;
		public RectTransform RectTrans => _rectTrans;

		private void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}
		private void Start() {
			if (_mainPanel == null) 
				UIMgr.Inst.FindPanel(out _mainPanel);
		}

		/// <summary>
		/// 设置当前tag是否可选
		/// </summary>
		private void SetSelectableImpl(bool selectable) {
			_unSelectableMask.SetActive(!selectable);
			_image.raycastTarget = selectable;
		}

		private void Update() {
			if (!Dirty) { return; }
			Dirty = false;
			if (Input.GetKey(KeyCode.LeftControl)) {
				if (_groupType == GroupType.Arch) {
					if (_archType == _mainPanel.CurArchType) {
						SetSelectableImpl(false);
					} else {
						// SetSelectableImpl(WorldMgr.Inst.FindWorkForVill(_mainPanel.SelectedVillCount, _archType));
						SetSelectableImpl(false);
					}
				} else if (_groupType == GroupType.Homeless) {
					SetSelectableImpl(false);
				} else if (_groupType == GroupType.Workless) {
					SetSelectableImpl(_mainPanel.CurGroupType != GroupType.Workless);
				}
			} else {
				SetSelectableImpl(true);
			}
		}

		#region Injection
		public void InjectInfo(Sprite sprite, ArchType archType) {
			_groupType = GroupType.Arch;
			_image.sprite = sprite;
			_archType = archType;
		}
		public void InjectInfo(Sprite sprite, GroupType groupType) {
			_groupType = groupType;
			_image.sprite = sprite;
		}
		#endregion

		#region PublicMethods
		public void OnSetedAsChild() {
			_rectTrans.offsetMin = new(_rectTrans.offsetMin.x, 0);
			_rectTrans.offsetMax = new(_rectTrans.offsetMax.x, 0);
		}
		public void SetLeftEdge(float x) {
			_rectTrans.offsetMin = new Vector2(x, _rectTrans.offsetMin.y);
		}
		public void SetWidth(float width) {
			_rectTrans.offsetMax = new (_rectTrans.offsetMin.x + width, _rectTrans.offsetMax.y);
		}
		public void OnPointerClick(PointerEventData _) {

			Debug.Log("OnPointerClick");

			// note: 目前的权宜之计
			// 在初始化的时候默认点击了homeless的tag，可能会在Start被调用之前进入这个函数导致 _mainPanel 为null
			if (_mainPanel == null) {
				UIMgr.Inst.FindPanel(out _mainPanel);
			}
			_mainPanel.OnOptionTagClicked(this);
		}
		#endregion
	}
}