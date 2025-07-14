using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// JobExpComponent 存储村民的工作经验和工作等级。
	/// </summary>
	[System.Serializable]
	public class JobExpComponent : IComponent {
		public EtList<JobType, float> JobExp_F = new(fillAll: true);
		public EtList<JobType, int> JobLevel_F = new(fillAll: true);

		public bool IsDirty = false;
	}
}