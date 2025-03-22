using System;

namespace NSFrame {
	[Serializable]
	public class SaveInfo {
		/// <summary>
		/// 存档的随机文件夹名称(唯一，也许吧，重复概率极小，重复了再说吧qwq)
		/// </summary>
		public string DirName;
		public string SaveName;
		public string CreateTime;
		public string LastUpdateTime;
		public SaveInfo(string _dirName, string _saveName, string _createTime) {
			DirName = _dirName;
			SaveName = _saveName;
			CreateTime = _createTime;
		}
		public void Update(string updateTime) {
			LastUpdateTime = updateTime;
		}
	}
}
