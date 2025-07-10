using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Arch {
	public class UnlockedArchResource : IResource {
		public EtList<ArchType, bool> UnlockedArchs;
	}
}