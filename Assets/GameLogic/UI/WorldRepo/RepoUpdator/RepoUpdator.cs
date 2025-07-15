using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Features.Repo;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using GameLogic.UI.Common.UiMgr;
using UnityEngine;

namespace GameLogic.UI.WorldRepo {
	public class RepoUpdator : GroupLayoutBase {
		[SerializeField] private GameObject _repoPrefab;

		private MainPanel _mainPanel;
		private readonly List<KeyValuePair<RepoType, RepoEle>> _repoEles = new();
		private ReadOnlyEtList<RepoType, float> _repoAmounts, _repoMaxAmounts;

		private void Start() {
			_mainPanel = UIMgr.Inst.FindPanel<MainPanel>();
			_repoAmounts = RepoQueryAPI.GetAllRepoAmounts();
			_repoMaxAmounts = RepoQueryAPI.GetAllRepoMaxAmounts();
			foreach (RepoType type in Enum.GetValues(typeof(RepoType)).Cast<RepoType>()) {
				var icon = _mainPanel.FindRepoIcon(type);
				var repoEle = Instantiate(_repoPrefab).GetComponent<RepoEle>();
				AddEle(repoEle);
				repoEle.SetIcon(icon);
				repoEle.SetSumMax(0f, _repoMaxAmounts[type]);
				_repoEles.Add(new KeyValuePair<RepoType, RepoEle>(type, repoEle));
			}
		}

		[SerializeField] private int _updateInterval = 10;
		private int _updateCounter = 0;

		private void Update() {
			_updateCounter++;
			if (_updateCounter < _updateInterval) return;
			_updateCounter = 0;

			_repoEles.ForEach(pr => pr.Value.SetSumMax(_repoAmounts[pr.Key], _repoMaxAmounts[pr.Key]));
		}
	}
}