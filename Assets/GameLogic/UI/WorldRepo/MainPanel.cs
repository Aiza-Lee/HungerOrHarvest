using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.UI.Common.UiMgr;
using NSFrame;
using UnityEngine;

namespace GameLogic.UI.WorldRepo {
	public class MainPanel : PanelBase, IRegisterUiMgr {

		[SerializeField] private List<SerializablePair<RepoType, Sprite>> _repoIcons;

		public override void OnClose() { }

		public override void OnShow() { }



		#region PublicMethods
		public Sprite FindRepoIcon(RepoType type) {
			return _repoIcons.Find(p => p.Key == type).Value;
		}
		#endregion

	}
}