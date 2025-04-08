using System.Collections.Generic;
using NSFrame;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLogic.View.UI.WorldVillPanel 
{
	public class GroupMgr : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
		public GameObject VillCardPrefab;
		public GameObject VillGroupPrefab;
		public float ScrollSpeed = 100;
		

		private RectTransform _rectTransform;
		private bool _isPointerIn;
		private readonly List<VillGroup> _groups = new();
		private GroupType _curGroupType;
		private float GroupSpace => _rectTransform.offsetMin.y;
		private float OutWidth;
		private float LeftEdge => _rectTransform.offsetMin.x;

		private void Awake() {
			PoolSystem.InitPrefabPool(VillCardPrefab, 15);
			PoolSystem.InitPrefabPool(VillGroupPrefab, 10);
			OutWidth = GetComponentInParent<RectTransform>().rect.width;
			_rectTransform = GetComponent<RectTransform>();
		}

		private void ClearGroup() {
			foreach (var group in _groups) {
				foreach (var card in group.VillCards) {
					PoolSystem.PushGO(card.gameObject);
				}
				PoolSystem.PushGO(group.gameObject);
			}
			_groups.Clear();
		}

		private void RearrageGroups() {
			var posX = 0f;
			foreach (var group in _groups) {
				group.SetRightEdge(posX + group.Width);
				group.SetLeftEdge(posX);
				posX += group.Width + GroupSpace;
			}
		}

		private void Update() {
			if (_isPointerIn) { UpdateScroll(); }
		}
		private void UpdateScroll() {
			if (_groups.Count == 0) { return; }
			if (Input.mouseScrollDelta.y > 0) { 
				MoveRight(); 
			} else if (Input.mouseScrollDelta.y < 0) { 
				MoveLeft(); 
			}
		}
		private void MoveRight() {
			if (LeftEdge < GroupSpace) { 
				SetLeftEdge(Mathf.Min(LeftEdge + ScrollSpeed * Time.unscaledDeltaTime, 0));
			}
		}
		private void MoveLeft() {
			if (LeftEdge > GroupSpace) {
				SetLeftEdge(Mathf.Max(LeftEdge - ScrollSpeed * Time.unscaledDeltaTime, GroupSpace));
			} else if (LeftEdge + CurWidth() > OutWidth) {
				SetLeftEdge(Mathf.Max(LeftEdge - ScrollSpeed * Time.unscaledDeltaTime, OutWidth - CurWidth()));
			}
		}
		private float CurWidth() {
			float res = 0f;
			foreach (var group in _groups) {
				res += group.Width + GroupSpace;
			}
			return res;
		}

		#region PublicMethods

		public void OnClose() {
			ClearGroup();
			_curGroupType = GroupType.None;
		}

		public void SetCurGroupType(GroupType groupType, ArchType archType = ArchType.None) {
			if (_curGroupType == groupType) { return; }
			ClearGroup();
			_curGroupType = groupType;
			if (archType != ArchType.None) {
				var archViews = WorldViewMgr.Inst.GetAllArchViews(archType);
				archViews.Sort((a, b) => a.Logic.Coord.X.CompareTo(b.Logic.Coord.X));

				foreach (var archView in archViews) {
					var group = PoolSystem.PopGO<VillGroup>(VillGroupPrefab, _rectTransform);
					group.OnSetedAsChild();
					group.InjectInfo(archView.Logic);
					_groups.Add(group);
				}
			}

			RearrageGroups();
		}

		public void SetLeftEdge(float x) {
			_rectTransform.offsetMin = new Vector2(x, _rectTransform.offsetMin.y);
		}
		#endregion
		public void OnPointerEnter(PointerEventData eventData) {
			_isPointerIn = true;
			Debug.Log("OnPointerEnter");
		}

		public void OnPointerExit(PointerEventData eventData) {
			_isPointerIn = false;
			Debug.Log("OnPointerExit");
		}
	}
}