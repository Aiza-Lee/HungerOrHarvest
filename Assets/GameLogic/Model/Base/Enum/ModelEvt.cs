namespace GameLogic
{
	public enum ModelEvt {
		/// <summary>
		/// 由于Mono的单例激活依赖Unity创建实例，会慢于非Mono的静态单例的创建，因此需要在Mono单例激活后再初始化其他单例
		/// </summary>
		MgrInitAfterMonoMgr,

		Tick_0,
		/// <summary>
		/// <para> 变化后的时间倍速: float </para>
		/// </summary>
		TickSpeedChange_f_1,
		GamePause_0,

		DayStart_0,
		/// <summary>
		/// 夜晚开始时，游戏暂停，确认每日信息后，游戏倍速进入下一天
		/// </summary>
		NightStart_0,

		ArchAdded_A_1,
		ArchDestroyed_A_1,

		LayerAdded_L_1,
		LayerDestroyed_L_1,

		VillAdded_V_1,
		VillDestroyed_V_1,

		/// <summary>
		/// <para> 村民ID: ulong </para> 
		/// <para> 建筑ID: ulong </para>
		/// </summary>
		VillArriveArch_VuAu_2,
		/// <summary>
		/// <para> 村民ID: ulong </para> 
		/// <para> 建筑ID: ulong </para>
		/// </summary>
		VillLeaveArch_VuAu_2,
		/// <summary>
		/// 村民ID，升级的JobType
		/// </summary>
		VillLevelUp_VuJ_2,
		/// <summary>
		/// <para> 村民ID: ulong </para> 
		/// <para> 原来的建筑ID: ulong </para>
		/// <para> 新的建筑ID: ulong </para>
		/// </summary>
		VillChengeWork_VuAuAu_3,

		/// <summary>
		/// 房间被分配给了村民
		/// <para> 村民ID: ulong </para>
		/// <para> 房间ID: ulong </para>
		/// </summary>
		NewRoomDistributed_VuAu_2,

		/// <summary>
		/// 解锁的OL
		/// </summary>
		UnlockOL_O_1,

		/// <summary>
		/// 解锁新类型的资源
		/// <para> 资源类型: RepoType </para>
		/// </summary>
		UnlockRepo_R_1,
		
	}
}