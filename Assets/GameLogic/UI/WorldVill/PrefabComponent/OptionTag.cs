using GameLogic.Common.DataTypes;
using GameLogic.Features.Elements.Arch;
using GameLogic.UI.Common.UiMgr;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.UI.WorldVill {
	public class OptionTag : MonoBehaviour, IPointerClickHandler {

		[SerializeField] private Image _image;
		[SerializeField] private GameObject _unSelectableMask;

		public bool Dirty { get; set; }

		/// <summary>
		/// Tag代表的组类型
		/// </summary>
		private GroupType _groupType;
		private ArchType? _archType;
		private RectTransform _rectTrans;
		private MainPanel _mainPanel;

		/// <summary>
		/// Tag代表的组类型
		/// </summary>
		public GroupType TagGroupType => _groupType;
		public ArchType? TagArchType => _archType;
		public RectTransform RectTrans => _rectTrans;

		private void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}
		private void Start() {
			if (_mainPanel == null)
				_mainPanel = UIMgr.Inst.FindPanel<MainPanel>();
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
			// 按下左ctrl就是要开始调遣村民了
			if (Input.GetKey(KeyCode.LeftControl)) {
				switch (_groupType) {
					// 分配家，只能对Homeless的生效
					case GroupType.Home: {
							SetSelectableImpl(
								_mainPanel.CurGroupType == GroupType.Homeless
								&& ArchQueryAPI.GetIsSpaceEnoughForVill(_mainPanel.SelectedVillCount, ArchType.Cottage)
							);
							break;
						}
					// 调遣到工作建筑
					case GroupType.Arch: {
							SetSelectableImpl(
								_mainPanel.CurGroupType != GroupType.Homeless
								&& _mainPanel.CurGroupType != GroupType.Home
								&& ArchQueryAPI.GetIsSpaceEnoughForVill(_mainPanel.SelectedVillCount, (ArchType) _archType)
								&& (
									_mainPanel.CurGroupType != GroupType.Arch
									|| (_mainPanel.CurGroupType == GroupType.Arch && _mainPanel.CurArchType != _archType)
								)
							);
							break;
						}
					// 使没有home 或者没有工作
					case GroupType.Workless: {
							SetSelectableImpl(
								_mainPanel.CurGroupType == GroupType.Arch
							);
							break;
						}
					// 使没有home，只在Home处生效
					case GroupType.Homeless: {
							SetSelectableImpl(_mainPanel.CurGroupType == GroupType.Home);
							break;
						}
					default: {
							Debug.LogError("未知的GroupType");
							break;
						}
				}
			} else {
				SetSelectableImpl(true);
			}
		}

		#region PublicMethods
		/// <summary>
		/// 设置建筑Tag的信息
		/// </summary>
		/// <param name="sprite">显示的图标</param>
		/// <param name="archType">建筑类型</param>
		public void SetTagInfo(Sprite sprite, ArchType archType) {
			_groupType = GroupType.Arch;
			_image.sprite = sprite;
			_archType = archType;
		}
		/// <summary>
		/// 设置非建筑Tag的信息
		/// </summary>
		/// <param name="sprite">显示的图标</param>
		/// <param name="groupType">设置组的类型</param>
		public void SetTagInfo(Sprite sprite, GroupType groupType) {
			_groupType = groupType;
			_image.sprite = sprite;
		}

		// note: 这里由于 OptionTagMgr 写完的时候还没有开发出GroupLayout，所以这里是单独实现的
		public void OnSetedAsChild() {
			_rectTrans.offsetMin = new(_rectTrans.offsetMin.x, 0);
			_rectTrans.offsetMax = new(_rectTrans.offsetMax.x, 0);
		}
		public void SetLeftEdge(float x) {
			_rectTrans.offsetMin = new Vector2(x, _rectTrans.offsetMin.y);
		}
		public void SetWidth(float width) {
			_rectTrans.offsetMax = new(_rectTrans.offsetMin.x + width, _rectTrans.offsetMax.y);
		}
		public void OnPointerClick(PointerEventData _) {

			// Debug.Log("OnPointerClick");

			// note: 目前的权宜之计
			// 在初始化的时候默认点击了homeless的tag，可能会在Start被调用之前进入这个函数，导致_mainPanel为null
			if (_mainPanel == null) {
				_mainPanel = UIMgr.Inst.FindPanel<MainPanel>();
			}
			_mainPanel.OnOptionTagClicked(this);
		}
		#endregion
	}
}