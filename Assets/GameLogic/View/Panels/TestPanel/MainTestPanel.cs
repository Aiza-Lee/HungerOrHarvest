using NSFrame;
using TMPro;

namespace GameLogic
{
	public class MainTestPanel : PanelBase {

		public TextMeshProUGUI Text;

		private void Update() {
			var repos = RepoMgr.Inst.Repos;
			var str = "";
			for (int i = 0; i < repos.Count; ++i) {
				str += $"{(RepoType)i}: \t{repos[i].Value:0.00}\n";
			}
			Text.text = str;
		}

		public override void OnClose() {
		}
		public override void OnShow() {
		}
	}
}