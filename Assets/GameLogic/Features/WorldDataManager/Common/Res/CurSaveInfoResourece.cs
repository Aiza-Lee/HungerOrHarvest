using NsEcsFrame.Core;
using NSFrame;

namespace GameLogic.Features.WorldDataManager {
	public class CurSaveInfoResource : IResource, IWorldClearRespondable {
		public SaveInfo SaveInfo;
		/// <summary>
		/// 当前世界中是否已经加载了有效的存档数据。
		/// </summary>
		public bool IsLoaded = false;

		public void RespondWorldClear() {
			SaveInfo = null;
			IsLoaded = false;
		}
	}
}