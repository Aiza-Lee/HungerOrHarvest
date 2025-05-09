using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.View.UI.WorldRepoPanel
{
	public class RepoUpdator : GroupLayoutBase {
		[SerializeField] private GameObject _repoPrefab;

		private MainPanel _mainPanel;
		private readonly List<RepoEle> _repoEles = new();

		private void Start() {
			UIMgr.Inst.FindPanel(out _mainPanel);
			for (int i = 0; i < ConstMgr.REPO_TYPE_SIZE; ++i) {
				var icon = _mainPanel.FindRepoIcon((RepoType)i);
				var repoEle = Instantiate(_repoPrefab).GetComponent<RepoEle>();
				AddEle(repoEle);
				repoEle.SetIcon(icon);
				repoEle.SetSum(0f);
				_repoEles.Add(repoEle);
			}
		}
		private void Update() {
			var repos = RepoMgr.Inst.Repos_F;
			for (int i = 0; i < repos.Count; ++i) {
				_repoEles[i].SetSum(repos[i].Value);
			}
		}
	}
}