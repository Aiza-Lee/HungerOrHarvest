
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Events {

	public class VillTryProdRequestComponent : IComponent {
		public EtList<RepoType, float> Cons, Prod;
		public EtList<JobType, float> ExpGained;
		public float VitToCost;
	}

	public class VillConsFoodRecoverVitRequestComponent : IComponent {
		public float FoodRequest;
		public float VitToRecover;
	}

	public class BondToArchRequestComponent : IComponent {
		public ulong ArchGid;
	}
	public class DisbondArchRequestComponent : IComponent {
		public ulong ArhcGid;
	}

	public enum VitCostReason {
		Production,
		/// <summary> 白天的自然消耗 </summary>
		DayTckcCost
	}
	public class VillCostVitRequestComponent : IComponent {
		public float VitCost;
		public VitCostReason Reason;
	}


	public enum VitGainReason { EatFood }
	public class VillGainVitRequestComponent : IComponent {
		public float VitGain;
		public VitGainReason Reason;
	}

	public enum ExpSource { Production, }
	public class ExpGainRequestComponent : IComponent {
		public EtList<JobType, float> ExpGain;
		public ExpSource Source;
	}
}