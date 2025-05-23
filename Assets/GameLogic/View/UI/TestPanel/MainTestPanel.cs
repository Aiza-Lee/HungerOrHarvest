using System.Text;
using GameLogic.Model.Mgr;
using NSFrame;
using TMPro;

namespace GameLogic.View.Test
{
	public class MainTestPanel : PanelBase {

		public TextMeshProUGUI RepoTextMesh;
		public TextMeshProUGUI TimeTextMesh;

		private readonly StringBuilder _sb = new(2048);


		private void Update() {
			UpdateRepo();
			UpdateTime();
		}
		private void UpdateRepo() {
			var repos = RepoMgr.Inst.Repos_F;
			_sb.Clear();
			for (int i = 0; i < repos.Count; ++i) {
				_sb.AppendLine($"{(RepoType)i}: \t{repos[i].Value:0.00}");
			}
			RepoTextMesh.text = _sb.ToString();
		}
		private void UpdateTime() {
			_sb.Clear();
			var timeSave = LogicTimeMgr.Inst.GetSave();
			_sb.AppendLine($"DayTicks: \t{ConfigMgr.Config.DAY_TICKS}");
			_sb.AppendLine($"NightTicks: \t{ConfigMgr.Config.NIGHT_TICKS}");
			_sb.AppendLine($"DayOrNight: \t{(timeSave.InDay ? "Day" : "Night")}");
			_sb.AppendLine($"Days: \t{timeSave.Days}");
			_sb.AppendLine($"TodayTick: \t{timeSave.TodayTick}");
			TimeTextMesh.text = _sb.ToString();
		}

		public override void OnClose() {}
		public override void OnShow() {}
	}
}