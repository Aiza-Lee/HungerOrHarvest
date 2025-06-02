using System;
using System.Collections.Generic;
using GameLogic.Model.Element.Arch;

namespace GameLogic.Model.Element.Vill {
	public interface IVillLogic {

		#region Properties
		/// <summary>
		/// 村民的类型
		/// </summary>
		VillType VillType { get; }

		/// <summary>
		/// 村民的唯一标识符
		/// </summary>
		ulong ID { get; }

		/// <summary>
		/// 村民的名字
		/// </summary>
		string FirstName { get; }

		/// <summary>
		/// 村民的姓氏
		/// </summary>
		string LastName { get; }

		/// <summary>
		/// 村民的当前坐标位置
		/// </summary>
		Coord Coord { get; }

		/// <summary>
		/// 村民住所的建筑ID
		/// </summary>
		ulong HomeID { get; }

		/// <summary>
		/// 村民工作场所的建筑ID
		/// </summary>
		ulong BondedWorkArchID { get; }

		/// <summary>
		/// 标识村民是否无家可归
		/// </summary>
		bool IsHomeless { get; }

		/// <summary>
		/// 标识村民是否无工作
		/// </summary>
		bool IsWorkless { get; }

		/// <summary>
		/// 村民当前执行的任务类型
		/// </summary>
		TaskType? CurTaskType { get; }

		/// <summary>
		/// 获取当前移动任务的目标类型
		/// </summary>
		MoveToTargetType? CurMoveToTargetType { get; }

		#endregion

		#region Methods

		/// <summary>
		/// 销毁村民对象
		/// </summary>
		void LogicDestroy();

		/// <summary>
		/// 将村民绑定到指定建筑
		/// </summary>
		void BondArch(ArchLogicBase arch);

		/// <summary>
		/// 解除村民与工作建筑的绑定
		/// </summary>
		void DisBondWorkArch();

		/// <summary>
		/// 解除村民与住所的绑定
		/// </summary>
		void DisBondHome();

		/// <summary>
		/// 移动村民到新坐标
		/// </summary>
		void Move(Coord dltCoord);

		/// <summary>
		/// 获取排序后的职业等级列表
		/// </summary>
		List<JobType> GetSortedJobLevels();

		/// <summary>
		/// 获取指定职业的经验比例
		/// </summary>
		float GetJobExpProportion(JobType jobType);

		/// <summary>
		/// 获取指定职业的等级
		/// </summary>
		int GetJobLevel(JobType jobType);

		/// <summary>
		/// 获取村民当前体力百分比
		/// </summary>
		float GetVitPercentage();
		#endregion

		#region Events
		/// <summary>
		/// 当村民坐标改变时触发
		/// </summary>
		event Action<Coord> OnCoordChange;

		/// <summary>
		/// 当村民职业升级时触发
		/// </summary>
		event Action<JobType> OnJobLevelUp;

		/// <summary>
		/// 当村民体力值变化时触发
		/// </summary>
		event Action<float> OnVitChanged;
		#endregion
	}
}
