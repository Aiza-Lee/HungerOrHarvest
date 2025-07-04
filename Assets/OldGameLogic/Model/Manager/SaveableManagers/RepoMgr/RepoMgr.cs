using System.Collections.Generic;
using OldGameLogic.Utilities;
using NSFrame;

namespace OldGameLogic.Model.Mgr {
	/// <summary>
	/// 资源管理中心
	/// </summary>
	public sealed class RepoMgr : ISaveable<RepoMgrSave>, IMananger, IRepoMgr {
		private RepoMgr() {
			EventSystem.AddListener((int) ModelEvt.MgrInitAfterMonoMgr, InitAfterMono, EventType.Model);
		}
		public static RepoMgr Inst { get; } = new();

		private readonly RTList<float> _repos_F = new(fill: true);
		private readonly RTList<float> _globalConsBuffs_F = new(fill: true);
		private readonly RTList<float> _globalProdBuffs_F = new(fill: true);

		private readonly RTList<bool> _unlockedRepos_F = new(fill: true);
		/// <summary>
		/// note: 记录消耗量的绝对值
		/// </summary>
		private readonly RTList<float> _dailyCons_F = new(fill: true);
		private readonly RTList<float> _dailyProd_F = new(fill: true);
		private readonly RTList<float> _lastSecondNet_F = new(fill: true);
		private readonly Queue<RTList<float>> _ticksNetInLastSeconds = new();

		private RTList<float> _curTickSum = new(fill: true);

		public RTList<float> Repos_F => _repos_F;
		public RTList<float> DailyCons_F => _dailyCons_F;
		public RTList<float> DailyProd_F => _dailyProd_F;
		public RTList<float> DailyNet_F => _dailyProd_F.Sub_New(_dailyCons_F);
		public RTList<bool> UnlockedRepos_F => _unlockedRepos_F;

		private void InitAfterMono() {
			TickTrigger.Inst.BeforeTick += BeforeTick;
			TickTrigger.Inst.AfterTick += AfterTick;
		}
		private void BeforeTick() {
			_curTickSum = new(fill: true);
		}
		private void AfterTick() {
			_lastSecondNet_F.Add(_curTickSum);
			_ticksNetInLastSeconds.Enqueue(_curTickSum);
			// 在队列中存储之前一秒内所有的帧产出信息，在超出时间后就弹出，并在_lastSecondNet_F中统计
			while (_ticksNetInLastSeconds.Count > TickTrigger.Inst.TickPerSec) {
				var item = _ticksNetInLastSeconds.Dequeue();
				_lastSecondNet_F.Sub(item);
			}
		}

		private bool TryTickConsImpl(RTList<float> cons) {
			if (!_repos_F.BigEnoughThan(cons)) {
				return false;
			}
			_repos_F.Sub(cons);
			_dailyCons_F.Add(cons);
			_curTickSum.Sub(cons);
			return true;
		}
		private bool TryTickConsImpl(RepoType repo, float cons) {
			if (cons <= 0) return true;
			if (_repos_F[(int) repo].Value < cons) return false;

			_repos_F[(int) repo].Value -= cons;
			_dailyCons_F[(int) repo].Value += cons;
			_curTickSum[(int) repo].Value -= cons;
			return true;
		}
		private void TickProdImpl(RTList<float> prod) {
			// todo: 处理最大容量问题
			_repos_F.Add(prod);
			_dailyProd_F.Add(prod);
			_curTickSum.Add(prod);
		}


		#region PublicMethods

		public void AddRepoFromSave(RTList<float> repos) {
			if (repos == null || repos.Count == 0) return;
			_repos_F.Add(repos);
		}

		public bool CheckRequest(RTList<float> demands, params RTList<float>[] buffs) {
			if (demands == null || demands.Count == 0) return true;

			var buff_F = new RTList<float>(fill: true);
			buff_F.Add(buffs).Add(_globalConsBuffs_F).Change((val) => 1f - val);

			var realCons = demands.Mul_New(buff_F);
			return _repos_F.BigEnoughThan(realCons);
		}
		public bool CheckRequest(RepoType repo, float demand, params RTList<float>[] buffs) {
			if (demand <= 0) return true;
			// // 检查资源种类是否解锁
			// if (!_unlockedRepos_F[(int) repo].Value) return false;
			// 检查资源数量是否满足需求
			var buff_F = new RTList<float>(fill: true);
			buff_F.Add(buffs).Add(_globalConsBuffs_F).Change((val) => 1f - val);

			return _repos_F[(int) repo].Value >= demand * buff_F[(int) repo].Value;
		}

		public bool CheckRequest(RepoType repo, float demand, float buff = 0f) {
			if (demand <= 0) return true;
			return _repos_F[(int) repo].Value >= demand * (1f - buff);
		}

		public bool TryCons(RTList<float> cons, params RTList<float>[] buffs) {
			if (cons == null || cons.Count == 0) return true;

			var buff_F = new RTList<float>(fill: true);
			buff_F.Add(buffs).Add(_globalConsBuffs_F).Change((val) => 1f - val);

			var realCons = cons.Mul_New(buff_F);
			return TryTickConsImpl(realCons);
		}
		public bool TryCons(RepoType repo, float demand, float buff = 0f) {
			if (demand <= 0) return true;
			
			var realCons = demand * (1f - buff);
			if (_repos_F[(int) repo].Value < realCons) return false;

			return TryTickConsImpl(repo, realCons);
		}

		public void Prod(RTList<float> prod, params RTList<float>[] buffs) {
			if (prod == null || prod.Count == 0) return;

			var buff_F = new RTList<float>(fill: true);
			buff_F.Add(buffs).Add(_globalProdBuffs_F).Change((val) => 1f + val);

			var realProd = prod.Mul_New(buff_F);
			TickProdImpl(realProd);
		}

		public void AddConsBuff(RepoType repo, float buff) { _globalConsBuffs_F[(int) repo].Value += buff; }
		public void AddConsBuff(RTPair<float> rtPair) { _globalConsBuffs_F[rtPair.Index].Value += rtPair.Value; }
		public void AddProdBuff(RepoType repo, float buff) { _globalProdBuffs_F[(int) repo].Value += buff; }
		public void AddProdBuff(RTPair<float> rtPair) { _globalProdBuffs_F[rtPair.Index].Value += rtPair.Value; }

		public void UnlockRepo(RepoType repo) {
			_unlockedRepos_F[(int) repo].Value = true;
			EventSystem.Invoke<RepoType>((int) ModelEvt.UnlockRepo_R_1, repo, EventType.Model);
		}
		#endregion

		#region ISaveable
		public RepoMgrSave GetSave() {
			var save = new RepoMgrSave {
				Repos = _repos_F.GetSave(),
				GlobalConsBuffs = _globalConsBuffs_F.GetSave(),
				GlobalProdBuffs = _globalProdBuffs_F.GetSave(),
				UnlockedRepos = _unlockedRepos_F.GetSave(),
				DailyCons = _dailyCons_F.GetSave(),
				DailyProd = _dailyProd_F.GetSave(),
				LastSecondNet = _lastSecondNet_F.GetSave(),
				LastSecondTickProduces = new()
			};
			foreach (var item in _ticksNetInLastSeconds) {
				save.LastSecondTickProduces.Add(item.GetSave());
			}
			return save;
		}

		public void InitFromSave(RepoMgrSave saveData) {
			_repos_F.InitFromSave_Full(saveData.Repos);
			_globalConsBuffs_F.InitFromSave_Full(saveData.GlobalConsBuffs);
			_globalProdBuffs_F.InitFromSave_Full(saveData.GlobalProdBuffs);
			_unlockedRepos_F.InitFromSave_Full(saveData.UnlockedRepos);
			_dailyCons_F.InitFromSave_Full(saveData.DailyCons);
			_dailyProd_F.InitFromSave_Full(saveData.DailyProd);
			_lastSecondNet_F.InitFromSave_Full(saveData.LastSecondNet);

			_ticksNetInLastSeconds.Clear();
			foreach (var item in saveData.LastSecondTickProduces) {
				var list = new RTList<float>();
				list.InitFromSave(item);
				_ticksNetInLastSeconds.Enqueue(list);
			}
		}
		#endregion

		#region IManager
		public void ClearMgr() { }
		#endregion
	}
}