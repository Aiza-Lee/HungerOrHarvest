namespace GameLogic.Model.Mgr {
	/// <summary>
	/// 资源管理中心接口
	/// </summary>
	public interface IRepoMgr {
		/// <summary>
		/// 实时更新的每日消耗总额
		/// </summary>
		RTList<float> DailyCons_F { get; }
		/// <summary>
		/// 实时更新的每日产出总额
		/// </summary>
		RTList<float> DailyProd_F { get; }
		/// <summary>
		/// 实时更新的每日净产量
		/// </summary>
		RTList<float> DailyNet_F { get; }
		/// <summary>
		/// 解锁的资源种类
		/// </summary>
		RTList<bool> UnlockedRepos_F { get; }
		RTList<float> Repos_F { get; }

		/// <summary>
		/// 直接添加资源，用于初始化资源、加载存档，不计入每日统计
		/// </summary>
		/// <param name="repos"> 添加的资源 </param>
		void AddRepoFromSave(RTList<float> repos);

		/// <summary>
		/// 检查当前资源是否满足消耗要求
		/// </summary>
		/// <param name="demands">消耗要求</param>
		/// <param name="buffs">减少消耗的buff</param>
		bool CheckRequest(RTList<float> demands, RTList<float>[] buffs);

		/// <summary>
		/// 检查一种资源是否满足消耗要求
		/// </summary>
		/// <param name="repo">资源种类</param>
		/// <param name="demand">需求量</param>
		/// <param name="buffs">减少消耗的buff</param>
		bool CheckRequest(RepoType repo, float demand, RTList<float>[] buffs);

		/// <summary>
		/// 检查一种资源是否满足消耗要求
		/// </summary>
		/// <param name="repo">资源种类</param>
		/// <param name="demand">需求量</param>
		/// <param name="buff">减少消耗的buff</param>
		bool CheckRequest(RepoType repo, float demand, float buff = 0f);

		/// <summary>
		/// 尝试消耗一些资源，如果资源不足则返回false，否则消耗资源并返回true
		/// </summary>
		/// <param name="cons">欲消耗的资源数量</param>
		/// <param name="buffs">减少消耗buff</param>
		bool TryCons(RTList<float> cons, params RTList<float>[] buffs);

		/// <summary>
		/// 尝试消耗一种资源，如果资源不足则返回false，否则消耗资源并返回true
		/// </summary>
		/// <param name="repo">资源种类</param>
		/// <param name="cons">需求量</param>
		/// <param name="buff">减少消耗的buff</param>
		bool TryCons(RepoType repo, float cons, float buff = 0f);

		/// <summary>
		/// 产出资源
		/// </summary>
		/// <param name="prod">欲产出的资源</param>
		/// <param name="buffs">增加产出的buff</param>
		void Prod(RTList<float> prod, params RTList<float>[] buffs);

		/// <summary>
		/// 添加全局的减少消耗buff
		/// </summary>
		/// <param name="repo">资源种类</param>
		/// <param name="buff">buff值</param>
		void AddConsBuff(RepoType repo, float buff);
		/// <summary>
		/// 添加全局的减少消耗buff
		/// </summary>
		/// <param name="rtPair">资源种类和buff值的键值对</param>
		void AddConsBuff(RTPair<float> rtPair);
		/// <summary>
		/// 添加全局的增加产出buff
		/// </summary>
		/// <param name="repo">资源种类</param>
		/// <param name="buff">buff值</param>
		void AddProdBuff(RepoType repo, float buff);
		/// <summary>
		/// 添加全局的增加产出buff
		/// </summary>
		/// <param name="rtPair">资源种类和buff值的键值对</param>
		void AddProdBuff(RTPair<float> rtPair);
		/// <summary>
		/// 解锁资源，通过事件中心发布资源解锁事件
		/// </summary>
		/// <param name="repo">解锁的资源种类</param>
		void UnlockRepo(RepoType repo);
	}
}
