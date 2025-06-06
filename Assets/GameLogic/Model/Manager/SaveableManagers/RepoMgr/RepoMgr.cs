using System.Collections.Generic;
using GameLogic.Utilities;
using NSFrame;

namespace GameLogic.Model.Mgr {
	/// <summary>
	/// 资源管理中心
	/// </summary>
	public sealed class RepoMgr : ISaveable<RepoMgrSave>, IMananger {
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
		/// <summary>
		/// 实时更新的每日消耗总额
		/// </summary>
		public RTList<float> DailyCons_F => _dailyCons_F;
		/// <summary>
		/// 实时更新的每日产出总额
		/// </summary>
		public RTList<float> DailyProd_F => _dailyProd_F;
		/// <summary>
		/// 实时更新的每日净产量
		/// </summary>
		public RTList<float> DailyNet_F => _dailyProd_F.Sub_New(_dailyCons_F);
		/// <summary>
		/// 解锁的资源种类
		/// </summary>
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
		private void TickProdImpl(RTList<float> prod) {
			// todo: 处理最大容量问题
			_repos_F.Add(prod);
			_dailyProd_F.Add(prod);
			_curTickSum.Add(prod);
		}


		#region PublicMethods

		/// <summary>
		/// 直接添加资源，用于初始化资源、加载存档，不计入每日统计
		/// </summary>
		/// <param name="repos"> 添加的资源 </param>
		public void AddRepoFromSave(RTList<float> repos) {
			if (repos == null || repos.Count == 0) return;
			_repos_F.Add(repos);
		}

		/// <summary>
		/// 检查当前资源是否满足消耗要求
		/// </summary>
		/// <param name="demands"> 消耗要求 </param>
		public bool CheckRequest(RTList<float> demands) {
			return _repos_F.BigEnoughThan(demands);
		}

		/// <summary>
		/// 尝试消耗一些资源，如果资源不足则返回false，否则消耗资源并返回true
		/// </summary>
		/// <param name="cons">欲消耗的资源数量</param>
		/// <param name="buffs">减少消耗buff</param>
		public bool TryCons(RTList<float> cons, params RTList<float>[] buffs) {
			if (cons == null || cons.Count == 0) return true;

			var buff_F = new RTList<float>(fill: true);
			buff_F.Add(buffs).Add(_globalConsBuffs_F).Change((val) => 1f - val);

			var realCons = cons.Mul_New(buff_F);
			return TryTickConsImpl(realCons);
		}

		/// <summary>
		/// 产出资源
		/// </summary>
		/// <param name="prod">欲产出的资源</param>
		/// <param name="buffs">增加产出的buff</param>
		public void Prod(RTList<float> prod, params RTList<float>[] buffs) {
			if (prod == null || prod.Count == 0) return;

			var buff_F = new RTList<float>(fill: true);
			buff_F.Add(buffs).Add(_globalProdBuffs_F).Change((val) => 1f + val);

			var realProd = prod.Mul_New(buff_F);
			TickProdImpl(realProd);
		}

		/// <summary>
		/// 添加全局的减少消耗buff
		/// </summary>
		/// <param name="repo">资源种类</param>
		/// <param name="buff">buff值</param>
		public void AddConsBuff(RepoType repo, float buff) { _globalConsBuffs_F[(int) repo].Value += buff; }
		/// <summary>
		/// 添加全局的减少消耗buff
		/// </summary>
		/// <param name="rtPair">资源种类和buff值的键值对</param>
		public void AddConsBuff(RTPair<float> rtPair) { _globalConsBuffs_F[rtPair.Index].Value += rtPair.Value; }
		/// <summary>
		/// 添加全局的增加产出buff
		/// </summary>
		/// <param name="repo">资源种类</param>
		/// <param name="buff">buff值</param>
		public void AddProdBuff(RepoType repo, float buff) { _globalProdBuffs_F[(int) repo].Value += buff; }
		/// <summary>
		/// 添加全局的增加产出buff
		/// </summary>
		/// <param name="rtPair">资源种类和buff值的键值对</param>
		public void AddProdBuff(RTPair<float> rtPair) { _globalProdBuffs_F[rtPair.Index].Value += rtPair.Value; }
		/// <summary>
		/// 解锁资源，通过事件中心发布资源解锁事件
		/// </summary>
		/// <param name="repo">解锁的资源种类</param>
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