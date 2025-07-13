using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Arch {
	public static class ArchUtils {
		public static int ArchLevel(Entity entity) {
			var archComp = entity.GetComponent<ArchLevelComponent>();
			return archComp.Level;
		}
	}
}