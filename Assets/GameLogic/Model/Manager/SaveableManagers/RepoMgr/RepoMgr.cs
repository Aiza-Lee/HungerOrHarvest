namespace GameLogic
{
	public sealed class RepoMgr : ISaveable<RepoMgrSave>, IClearMgr {
		private RepoMgr() {}
		public static RepoMgr Inst { get; } = new();

		private RTList<float> _repos_F = new(fill: true);
		private RTList<float> _globalConsBuffs_F = new(fill: true);
		private RTList<float> _globalProdBuffs_F = new(fill: true);
		private RTList<bool> _unlockedRepos_F = new(fill: true);

		public RTList<float> Repos => _repos_F;

		public void AddRepo(RTList<float> adds) {
			if (adds == null || adds.Count == 0) return;
			foreach (var pair in adds.List) {
				_repos_F[pair.Index].Value += pair.Value;
			}
		}

		public bool CheckRequest(RTList<float> demands) {
			if (demands == null || demands.Count == 0) return true;
			foreach (var pair in demands.List) {
				if (_repos_F[pair.Index].Value < pair.Value) return false;
			}
			return true;
		}
		public bool TryConsume(RTList<float> cons) {
			if (!CheckRequest(cons)) return false;
			foreach (var pair in cons.List) {
				_globalConsBuffs_F[pair.Index].Value -= pair.Value;
			}
			return true;
		}

		public bool TryVillCons(RTList<float> cons, RTList<float> archBuffs_F, RTList<float> villBuffs_F) {
			if (cons == null || cons.Count == 0) return true;
			int idx;
			foreach (var pair in cons.List) {
				idx = pair.Index;
				if (_repos_F[idx].Value < pair.Value * (1f - archBuffs_F[idx].Value - villBuffs_F[idx].Value - _globalConsBuffs_F[idx].Value)) 
					return false; 
			}
			foreach (var consume in cons.List) {
				idx = consume.Index;
				_repos_F[idx].Value -= consume.Value * (1f - archBuffs_F[idx].Value - villBuffs_F[idx].Value - _globalConsBuffs_F[idx].Value);
			}
			return true;
		}
		public bool TryArchCons(RTList<float> cons, RTList<float> archBuffs_F) {
			if (cons == null || cons.Count == 0) return true;
			int idx;
			foreach (var pair in cons.List) {
				idx = pair.Index;
				if (_repos_F[idx].Value < pair.Value * (1f - archBuffs_F[idx].Value - _globalConsBuffs_F[idx].Value)) 
					return false; 
			}
			foreach (var pair in cons.List) {
				idx = pair.Index;
				_repos_F[idx].Value -= pair.Value * (1f - archBuffs_F[idx].Value - _globalConsBuffs_F[idx].Value);
			}
			return true;
		}
		public void VillProd(RTList<float> prod, RTList<float> archBuffs_F, RTList<float> villBuffs_F) {
			int idx;
			foreach (var pair in prod.List) {
				idx = pair.Index;
				_repos_F[pair.Index].Value += pair.Value * (1f + archBuffs_F[idx].Value + villBuffs_F[idx].Value + _globalProdBuffs_F[idx].Value);
			}
		}
		public void ArchProd(RTList<float> prod, RTList<float> archBuffs_F) {
			int idx;
			foreach (var pair in prod.List) {
				idx = pair.Index;
				_repos_F[pair.Index].Value += pair.Value * (1f + archBuffs_F[idx].Value + _globalProdBuffs_F[idx].Value);
			}
		}


		public void AddConsBuff(RepoType repo, float buff) { _globalConsBuffs_F[(int)repo].Value += buff; }
		public void AddConsBuff(RTPair<float> rtPair) { _globalConsBuffs_F[rtPair.Index].Value += rtPair.Value; }
		public void AddProdBuff(RepoType repo, float buff) { _globalProdBuffs_F[(int)repo].Value += buff; }
		public void AddProdBuff(RTPair<float> rtPair) { _globalProdBuffs_F[rtPair.Index].Value += rtPair.Value; }
		public void UnlockRepo(RepoType repo) { _unlockedRepos_F[(int)repo].Value = true; }


		public RepoMgrSave GetSave() {
			return new RepoMgrSave() {
				Repos 			= _repos_F.Clone(),
				GlobalConsBuffs = _globalConsBuffs_F.Clone(),
				GlobalProdBuffs = _globalProdBuffs_F.Clone(),
				UnlockedRepos 	= _unlockedRepos_F.Clone(),
			};
		}

		public void InitFromSave(RepoMgrSave saveData) {
			_repos_F 			= saveData.Repos.ConvertToFull();
			_globalConsBuffs_F 	= saveData.GlobalConsBuffs.ConvertToFull();
			_globalProdBuffs_F 	= saveData.GlobalProdBuffs.ConvertToFull();
			_unlockedRepos_F 	= saveData.UnlockedRepos.ConvertToFull();
		}

		public void ClearMgr() { }
	}
}