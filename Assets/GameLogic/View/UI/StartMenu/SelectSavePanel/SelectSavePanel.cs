using System.Collections.Generic;
using System.Linq;
using NSFrame;
using UnityEngine;

namespace GameLogic.View.UI.StartMenu.SelectSavePanel
{
	public class SelectSavePanel : PanelBase {

		[SerializeField] private LeftLayout _leftLayout;
		[SerializeField] private RightLayout _rightLayout;

		private List<SaveInfo> _saveInfos;
		// key: worldHashID, value: (key: worldName, value: saveInfos)
		private List<Pair<string, Pair<string, List<SaveInfo>>>> _groupedSaveInfosList = new();

		public override void OnClose() {
			_saveInfos.Clear();
			_groupedSaveInfosList.Clear();
			UIMgr.Inst.TogglePanel<MainPanel>();
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
			
			var tmpDictHelper = new Dictionary<string, Pair<string, List<SaveInfo>>>();
			foreach (var si in _saveInfos) {
				var baseInfo = SaveSystem.LoadObject<WorldBaseInfoMgrSave>(si);
				tmpDictHelper.TryAdd(baseInfo.WorldHashID, new(baseInfo.WorldName, new()));
				tmpDictHelper[baseInfo.WorldHashID].Value.Add(si);
			}
			foreach (var ele in tmpDictHelper) {
				_groupedSaveInfosList.Add(new(ele.Key, ele.Value));
				ele.Value.Value.Sort((a, b) => b.LastUpdateTime.CompareTo(a.LastUpdateTime));
			}
			_groupedSaveInfosList.Sort((a, b) => b.Value.Value[0].LastUpdateTime.CompareTo(a.Value.Value[0].LastUpdateTime));
		}
		
		/// <summary>
		/// 初始化所有存档信息
		/// <para>每一个世界所有存档按照最后保存的时间排序</para>
		/// <para>每个世界按照该世界中最后保存的时间排序</para>
		/// <para>Linq 版本，可读性更好，性能上不及上面的版本，但通常差别不大</para>
		/// </summary>
		private void InitAllSaves_Linq() {
			_saveInfos = SaveSystem.GetAllSaveInfos();
			
			_groupedSaveInfosList = _saveInfos
				.Select(si => new {
					SaveInfo = si,
					BaseInfo = SaveSystem.LoadObject<WorldBaseInfoMgrSave>(si)
				})
				.GroupBy(x => x.BaseInfo.WorldHashID)
				.Select(group => new {
					WorldHashID = group.Key,
					WorldName = group.First().BaseInfo.WorldName,
					SaveInfos = group	.Select(x => x.SaveInfo)
										.OrderByDescending(si => si.LastUpdateTime)
										.ToList()
				})
				.OrderByDescending(g => g.SaveInfos.First().LastUpdateTime)
				.Select(g => new Pair<string, Pair<string, List<SaveInfo>>>(
					g.WorldHashID,
					new(g.WorldName, g.SaveInfos)
				))
				.ToList();
		}

		public void ChooseWorld(string worldHash) {
			_rightLayout.ResetContent(
				_groupedSaveInfosList.Find(group => group.Key == worldHash).Value.Value
			);
		}
		public void Refresh() {
			InitAllSaves_Linq();
			_leftLayout.ResetContent(
				_groupedSaveInfosList.Select(group => new Pair<string, string>(group.Key, group.Value.Key)).ToList()
			);
			_leftLayout.ChooseFirstWorld();
		}
	}
}