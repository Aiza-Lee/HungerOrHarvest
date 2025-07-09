using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.Destroyer {
	/// <summary>
	/// 代表建筑销毁的资源
	/// </summary>
	public class LayerDestroyResource : IResource {
		public List<EntityId> LayerToDestroy = new();
	}
}