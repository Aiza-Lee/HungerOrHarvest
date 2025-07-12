using NsEcsFrame.Core;
using NSFrame;

namespace GameLogic.Features.SaveLoadData {
	public class SaveInfoResource : IResource {
		public SaveInfo SaveInfo;
		/// <summary>
		/// 是否已经加载了存档数据
		/// </summary>
		public bool LoadedSave = false;
	}
}