using GameLogic.Common.Logic;
using GameLogic.Features.Elements.Vill;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.UI.WorldVill {
	/// <summary>
	/// 村民展开面板中，展示职业信息的布局
	/// </summary>
	public class JobInfoLayout : GroupLayoutBase {

		/// <summary>
		/// 最多展示等级前多少的职业
		/// </summary>
		private const int JOB_INFO_MAX = 4;

		private Entity _targetVill;

		[SerializeField] private int _updateInterval = 5;
		private int _updateCount = 0;

		void FixedUpdate() {
			if (_targetVill == null) return;
			if (++_updateCount < _updateInterval) return;
			_updateCount = 0;
			if (_targetVill.IsValid() == false) {
				Clear();
				return;
			}
			Clear();
			GenerateJobInfos();
		}

		private void GenerateJobInfos() {
			var jobs = VillQueryAPI.GetSortedJobLevels(_targetVill);
			for (int i = 0; i < Mathf.Min(JOB_INFO_MAX, ConstMgr.JOB_TYPE_SIZE); ++i) {
				AddEle(VillExpandJobInfoFactory.Inst.Create(_targetVill, jobs[i].EnumType));
			}
		}


		#region PublicMethods
		/// <summary>
		/// 在扩展面板收缩的时候调用
		/// </summary>
		public void OnShrinkDone() {
			Clear();
			_targetVill = null;
		}
		/// <summary>
		/// 在扩展面板展开的时候检查是否需要初始化
		/// </summary>
		public void OnExpand(Entity vill) {
			_targetVill = vill;
			GenerateJobInfos();
		}
		#endregion
	}
}