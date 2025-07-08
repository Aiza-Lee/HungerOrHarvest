using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	public class AddJobExpComponent : IComponent {
		public EtList<JobType, float> JobExpsToAdd_F = new(fillAll: true);
	}
}