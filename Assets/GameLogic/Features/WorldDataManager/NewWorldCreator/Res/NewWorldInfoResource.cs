using NsEcsFrame.Core;

namespace GameLogic.Features.WorldDataManager {
	public class NewWorldInfoResource : IResource {
		public NewWorldInfo NewWorldInfo;
	}

	public class NewWorldInfo {
		public string WorldName;
		public RandomWorldBaseInfo BaseInfo;
	}
}