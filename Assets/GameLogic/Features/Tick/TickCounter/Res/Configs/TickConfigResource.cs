using System;
using NsEcsFrame.Core;

namespace GameLogic.Features.TickCounter {
	[Serializable]
	public class TickConfigResource : IResource {
		public TickConfig TickConfig;
	}
}