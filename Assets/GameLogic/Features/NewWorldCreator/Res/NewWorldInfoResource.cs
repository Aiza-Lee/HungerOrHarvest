using NsEcsFrame.Core;
using NSFrame;

namespace GameLogic.Features.NewWorldCreator {
	public class NewWorldInfoResource : IResource {
		public NewWorldInfo NewWorldInfo;
	}

	public class NewWorldInfo {
		public string WorldName;
		public SaveInfo SaveInfo;
		public RandomWorldBaseInfo BaseInfo;
	}
}