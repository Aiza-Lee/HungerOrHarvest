using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	public class UnlockedArchResource : IResource, ISaveableResource, IWorldClearRespondable {
		public EtList<ArchType, bool> Unlocked_F = new(fillAll: true);

		public void Load(IEnumerable<object> loadedData) {
			foreach (var data in loadedData) {
				if (data is UnlockedArchResource res) {
					Unlocked_F = res.Unlocked_F;
					break;
				}
			}
		}

		public void RespondWorldClear() {
			Unlocked_F.Fill(false);
		}
	}
}