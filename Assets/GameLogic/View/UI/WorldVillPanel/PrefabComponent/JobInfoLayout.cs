using GameLogic.Model.Mgr;
using UnityEngine;
using UnityEngine.Assertions;

namespace GameLogic.View.UI.WorldVillPanel
{
	/// <summary>
	/// 村民展开面板中，展示职业信息的布局
	/// </summary>
	public class JobInfoLayout : GroupLayoutBase {

		/// <summary>
		/// 最多展示等级前多少的职业
		/// </summary>
		private const int JOB_INFO_MAX = 4;

		private ulong _attachedVillID;

		private void OnLevelUp(JobType _) {
			Clear();
			GenerateJobInfos();
		}

		private void GenerateJobInfos() {
			var jobs = WorldMgr.Inst.FindVill(_attachedVillID).GetSortedJobLevels();
			for (int i = 0; i < Mathf.Min(JOB_INFO_MAX, ConstMgr.JOB_TYPE_SIZE); ++i) {
				AddEle(VillExpandJobInfoFactory.Inst.Create(_attachedVillID, jobs[i]));
			}
		}


		#region PublicMethods
		/// <summary>
		/// 在扩展面板收缩的时候调用
		/// </summary>
		public void OnShrinkDone() {
			Clear();
			WorldMgr.Inst.FindVill(_attachedVillID).OnLevelUp -= OnLevelUp;
			_attachedVillID = 0;
		}
		/// <summary>
		/// 在扩展面板展开的时候检查是否需要初始化
		/// </summary>
		public void OnExpand(ulong villID) {
			if (_attachedVillID != 0) return;
			_attachedVillID = villID;
			WorldMgr.Inst.FindVill(villID).OnLevelUp += OnLevelUp;
			GenerateJobInfos();
		}
		#endregion
	}
}