using System.Collections.Generic;
using NSFrame;
using UnityEngine.UI;
using UnityEngine;

namespace GameLogic.View.UI.WorldRepoPanal
{
	public class MainPanel : PanelBase {

		[SerializeField] private List<Pair<RepoType, Sprite>> _repoIcons;

		public override void OnClose() {
		}

		public override void OnShow() {
		}



		#region PublicMethods
		public Sprite FindRepoIcon(RepoType type) {
			return _repoIcons.Find(p => p.Key == type).Value;
		}
		#endregion

	}
}