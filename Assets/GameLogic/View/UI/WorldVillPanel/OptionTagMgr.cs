using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;
using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class OptionTagMgr : MonoBehaviour {
		[Header("挂载")]
		[SerializeField] private GameObject _optionTagPrefab;
		[SerializeField] private RectTransform _tagRoot;
		[SerializeField] private List<Pair<string, Sprite>> _archIcons;
		[SerializeField] private Sprite _defaultIcon;
		[SerializeField] private Sprite _homelessIcon;
		[SerializeField] private Sprite _worklessIcon;

		private readonly List<OptionTag> _tags = new();
		private float Width => _tagRoot.rect.height;
		private float Space => _tagRoot.offsetMin.y;
		private readonly List<Pair<ArchType, Sprite>> _icons = new();

		private void Awake() {
			// Debug.Log("OptionTagMgr Awake");
			PoolSystem.InitPrefabPool(_optionTagPrefab, 30);
			_archIcons.ForEach(p => _icons.Add(new Pair<ArchType, Sprite>(Enum.Parse<ArchType>(p.Key), p.Value)));
		}

		private OptionTag GetTagGO() {
			var tag = PoolSystem.PopGO<OptionTag>(_optionTagPrefab, _tagRoot);
			tag.OnSetedAsChild();
			return tag;
		}
		private Sprite FindIcon(ArchType archType) {
			return _icons.Find(p => p.Key == archType).Value;
		}
		
		private void RearrageAllTags() {
			var posx = 0f;
			foreach (var tag in _tags) {
				tag.SetLeftEdge(posx);
				tag.SetWidth(Width);
				posx += Width + Space;
			}
		}

		#region PublicMethods
		public void OnShow() {
			for (int i = 0; i < ConstMgr.ARCH_TYPE_SIZE; ++i) {
				var aType = (ArchType)i;
				if (aType == ArchType.Cottage) { continue; }
				if (WorldMgr.Inst.IsAnyArch(aType)) {
					var tag = GetTagGO();
					tag.SetTagInfo(FindIcon(aType), aType);
					_tags.Add(tag);
				}
			}

			var homelessTag = GetTagGO();
			homelessTag.SetTagInfo(_homelessIcon, GroupType.Homeless);
			_tags.Add(homelessTag);

			var worklessTag = GetTagGO();
			worklessTag.SetTagInfo(_worklessIcon, GroupType.Workless);
			_tags.Add(worklessTag);

			RearrageAllTags();

			// 正常运作的时候只有可能在（不考虑UI的遮挡而需要的开关panel，因为已经分层了）最开始的时候会调用这个方法，所以可以直接点击第一个tag
			// 这里默认设置为点击了 homelessTag
			homelessTag.OnPointerClick(null);
		}
		public void OnClose() {
			foreach (var tag in _tags) {
				PoolSystem.PushGO(tag.gameObject);
			}
			_tags.Clear();
		}
		public void SetAllTagDirty() {
			foreach (var tag in _tags) {
				tag.Dirty = true;
			}
		}
		#endregion

	}
}