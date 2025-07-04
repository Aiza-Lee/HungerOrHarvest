using System;
using System.Collections.Generic;


namespace OldGameLogic.Model.Element.Vill {
	public interface IExpHelper {
		/// <summary>
		/// 返回排序后的所有工作
		/// <para>排序规则：首先按照等级排序，相同等级的按照经验值排序</para>
		/// </summary>
		List<JobType> GetSortedJobLevels();
		/// <summary>
		/// 添加经验值，如果当前经验值满了而没有下一级的 Config，那经验值不会再增加
		/// </summary>
		void AddExp(JTList<float> exps);
		/// <summary>
		/// 返回某个职业的经验值占升级所需要的总经验值的比例
		/// </summary>
		float GetJobExpProportion(JobType jobType);
		/// <summary>
		/// 查询某个职业的等级
		/// </summary>
		int GetJobLevel(JobType jobType);

		event Action<JobType> OnJobLevelUp;
	}
}