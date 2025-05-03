using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLogic.View.UI.WorldVillPanel 
{
	public class GroupMgr : GroupLayoutBase, IPointerEnterHandler, IPointerExitHandler {
		[SerializeField] private float _scrollSpeed = 100;
		// 为实现 Scroll
		[SerializeField] private RectTransform _groupRoot;

		/// <summary>
		/// GroupRoot SmoothoffsetMin
		/// </summary>
		private SmoothOffsetMin _groupRootSOMin;
		/// <summary>
		/// GroupRoot SmoothoffsetMax
		/// </summary>
		private SmoothOffsetMax _groupRootSOMax;

		private bool _isPointerIn;
		private GroupType _curGroupType;
		private ArchType _curArchType;
		private float ParentWidth;

		private float GroupSpace => _groupRoot.offsetMin.y;
		private float LeftEdge => _groupRoot.offsetMin.x;

		protected override void Awake() {
			base.Awake();
			_groupRootSOMin = _groupRoot.GetComponent<SmoothOffsetMin>();
			_groupRootSOMax = _groupRoot.GetComponent<SmoothOffsetMax>();
			ParentWidth = GetComponentInParent<RectTransform>().rect.width;
		}
		private void Update() {
			if (_isPointerIn) { 
				UpdateScroll(); 
			}
		}

		private void UpdateScroll() {
			if (_eles.Count == 0) { return; }
			if (Input.mouseScrollDelta.y != 0) {
				if (Input.mouseScrollDelta.y > 0) { 
					MoveRight(); 
				} else if (Input.mouseScrollDelta.y < 0) { 
					MoveLeft(); 
				}
			}
		}
		private void MoveRight() {
			if (LeftEdge < GroupSpace) { 
				SetLeftEdge_Smooth(Mathf.Min(LeftEdge + _scrollSpeed * Time.unscaledDeltaTime, 0));
			}
		}
		private void MoveLeft() {
			if (LeftEdge > GroupSpace) {
				SetLeftEdge_Smooth(Mathf.Max(LeftEdge - _scrollSpeed * Time.unscaledDeltaTime, GroupSpace));
			} else if (LeftEdge + EleSize > ParentWidth) {
				SetLeftEdge_Smooth(Mathf.Max(LeftEdge - _scrollSpeed * Time.unscaledDeltaTime, ParentWidth - EleSize));
			}
		}

		private void SetLeftEdge_Smooth(float x) {
			_groupRootSOMin.SetTarget(new(x, _groupRootSOMin.CurVal.y));
		}
		private void SetLeftEdge(float x) {
			_groupRootSOMin.SetCurVal(new(x, _groupRootSOMin.CurVal.y));
		}

		#region PublicMethods

		public void OnShow() {}
		public void OnClose() {
			Clear();
		}

		public void SetCurGroupType(GroupType groupType, ArchType archType = ArchType.None) {
			// if (_curGroupType == groupType && _curArchType == archType) { return; }
			Clear();
			_curGroupType = groupType;
			_curArchType = archType;
			// 如果是展示建筑的group
			if (archType != ArchType.None) {
				// 获取对应类型的全部建筑的 View
				var archViews = WorldViewMgr.Inst.GetAllArchViews(archType);
				archViews.Sort((a, b) => a.Logic.Coord.X.CompareTo(b.Logic.Coord.X));

				// 为每一个 View 创建一个 Group
				foreach (var archView in archViews) {
					var group = VillGroupFactory.Inst.Create(archView.Logic);
					AddEle(group);
					group.RearrangeEle();
				}
			} else { // 如果是展示 Homeless 或者 Workless 的group
				var group = VillGroupFactory.Inst.Create(groupType);
				AddEle(group);
				group.RearrangeEle();
			}
		}

		public override void Clear() {
			SetLeftEdge(0);
			foreach (var ele in _eles) {
				(ele as VillGroup).Clear();
			}
			_curGroupType = GroupType.None;
			_curArchType = ArchType.None;
			base.Clear();
		}

		#endregion

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