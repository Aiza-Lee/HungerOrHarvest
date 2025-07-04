using System;

namespace NSFrame {
    [Serializable]
    public class SaveInfo {
        /// <summary>存档的唯一文件夹名称</summary>
        public string DirName { get; private set; }
        public string SaveName;
        public string CreateTime { get; private set; }
        public string LastUpdateTime { get; private set; }

        public SaveInfo(string dirName, string saveName, string createTime) {
            DirName = dirName;
            SaveName = saveName;
            CreateTime = createTime;
            LastUpdateTime = createTime;
        }

        public void Update(string updateTime) {
            LastUpdateTime = updateTime;
        }

        public override string ToString() =>
            $"SaveInfo(Name={SaveName}, Dir={DirName}, Created={CreateTime}, Updated={LastUpdateTime})";
    }
}
