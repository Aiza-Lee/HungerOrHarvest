using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// JobExpComponent 存储村民的工作经验和工作等级。
	/// </summary>
	[System.Serializable]
	public class JobExpComponent : IComponent {
		public EtList<JobType, float> JobExps;
		public EtList<JobType, int> JobLevels;
	}
}