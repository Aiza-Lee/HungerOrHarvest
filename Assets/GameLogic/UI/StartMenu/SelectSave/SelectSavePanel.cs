using System.Collections.Generic;
using System.Linq;
using GameLogic.Features.UiData.StartMenuData;
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

		void Update() {
			if (StartMenuDataAPI.IsAnySaveChanged) {
				StartMenuDataAPI.IsAnySaveChanged = false;
				Refresh();
			}
		}

		/// <summary>
		/// 刷新左侧世界列表和右侧存档列表
		/// </summary>
		private void Refresh() {
			InitAllSaves();
			_leftLayout.ResetContent(
				_nameToSaveInfos.Select(group => group.Key).ToList()
			);
			_leftLayout.ChooseFirstWorld();
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
			_nameToSaveInfos.Clear();
			foreach (var pr in tmpDict) {
				_nameToSaveInfos.Add(pr);
				pr.Value.Sort((a, b) => b.LastUpdateTime.CompareTo(a.LastUpdateTime));
			}
			_nameToSaveInfos.Sort((a, b) => b.Value[0].LastUpdateTime.CompareTo(a.Value[0].LastUpdateTime));
		}

		public void ChooseWorld(string saveName) {
			_rightLayout.ResetContent(
				_nameToSaveInfos.Find(group => group.Key == saveName).Value
			);
		}

		public (bool, string) CheckNameValid(string name) {
			if (string.IsNullOrWhiteSpace(name)) {
				return (false, "存档名称不能为空");
			}
			if (_nameToSaveInfos.Any(group => group.Key == name)) {
				return (false, "存档名称已存在");
			}
			return (true, string.Empty);
		}

		public void DeletSaveInfo(SaveInfo saveInfo) {
			_nameToSaveInfos
				.Find(group => group.Key == saveInfo.SaveName)
				.Value.Remove(saveInfo);
			SaveSystem.DeleteSaveFile(saveInfo);
			if (_nameToSaveInfos.Find(group => group.Key == saveInfo.SaveName).Value.Count == 0) {
				_rightLayout.ResetContent(null);
				// 如果该世界没有存档了，则直接全部重新加载，不会有很大的性能损失
				Refresh();
			} else {
				ChooseWorld(saveInfo.SaveName);
			}
		}
	}
}