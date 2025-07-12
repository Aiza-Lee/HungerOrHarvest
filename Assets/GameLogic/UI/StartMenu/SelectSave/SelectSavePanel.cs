using System.Collections.Generic;
using System.Linq;
using GameLogic.UI.Common.UiMgr;
using NSFrame;
using UnityEngine;

namespace GameLogic.UI.StartMenu {
	public class SelectSavePanel : PanelBase, IRegisterUiMgr {

		[SerializeField] private LeftLayout _leftLayout;
		[SerializeField] private RightLayout _rightLayout;

		private List<SaveInfo> _saveInfos;
		// key: worldName, value: saveInfos
		private readonly List<KeyValuePair<string, List<SaveInfo>>> _nameToSaveInfos = new();

		public override void OnClose() {
			_saveInfos.Clear();
			_nameToSaveInfos.Clear();
		}
		public override void OnShow() {
			Refresh();
		}

		/// <summary>
		/// 初始化所有存档信息
		/// <para>每一个世界所有存档按照最后保存的时间排序</para>
		/// <para>每个世界按照该世界中最后保存的时间排序</para>
		/// </summary>
		private void InitAllSaves() {
			_saveInfos = SaveSystem.GetAllSaveInfos();

			var tmpDict = new Dictionary<string, List<SaveInfo>>();
			foreach (var si in _saveInfos) {
				tmpDict.TryAdd(si.SaveName, new());
				tmpDict[si.SaveName].Add(si);
			}
			foreach (var ele in tmpDict) {
				_nameToSaveInfos.Add(new(ele.Key, ele.Value));
				ele.Value.Sort((a, b) => b.LastUpdateTime.CompareTo(a.LastUpdateTime));
			}
			_nameToSaveInfos.Sort((a, b) => b.Value[0].LastUpdateTime.CompareTo(a.Value[0].LastUpdateTime));
		}

		public void ChooseWorld(string saveName) {
			_rightLayout.ResetContent(
				_nameToSaveInfos.Find(group => group.Key == saveName).Value
			);
		}
		public void Refresh() {
			InitAllSaves();
			_leftLayout.ResetContent(
				_nameToSaveInfos.Select(group => group.Key).ToList()
			);
			_leftLayout.ChooseFirstWorld();
		}
	}
}