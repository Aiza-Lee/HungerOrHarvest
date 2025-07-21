using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	[System.Serializable]
	public class InArchComponent : IComponent {
		/// <summary>
		/// 记录村民所在建筑的 GID。
		/// 如果为 0，表示村民不在任何建筑内。
		/// </summary>
		public ulong ArchGid = 0;
	}
}