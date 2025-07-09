using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// 临时记录要添加的工作经验
	/// </summary>
	public class AddJobExpComponent : IComponent {
		public EtList<JobType, float> JobExpsToAdd_F = new(fillAll: true);
	}
}