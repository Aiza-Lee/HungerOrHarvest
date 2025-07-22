using GameLogic.Common.Logic;
using GameLogic.Features.Elements.Vill;
using GameLogic.Features.Job;
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

		private bool _isCardOpened = false;
		[SerializeField] private float _updateIntervalTime = 0.7f;
		private float _lastUpdateTime = -100f;

		protected override void Awake() {
			base.Awake();
			_updateIntervalTime += Random.Range(0f, 1f);
		}

		void FixedUpdate() {
			if (!_isCardOpened) {
				Clear();
				return;
			}
			if (_targetVill == null) return;
			if (Time.unscaledTime - _lastUpdateTime < _updateIntervalTime) return;
			_lastUpdateTime = Time.unscaledTime;

			Clear();
			GenerateJobInfos();
		}
		private void GenerateJobInfos() {
			if (_eles.Count == 0) {
				for (int i = 0; i < Mathf.Min(JOB_INFO_MAX, ConstMgr.JOB_TYPE_SIZE); ++i) {
					AddEle(VillExpandJobInfoFactory.Inst.Create());
				}
			}
			var jobs = VillQueryAPI.GetSortedJobLevels(_targetVill);
			for (int i = 0; i < _eles.Count; ++i) {
				var jobInfo = _eles[i] as VillExpandJobInfo;
				jobInfo.SetContent(
					JobQueryAPI.GetJobName(jobs[i].Item1),
					jobs[i].Item2, jobs[i].Item3
				);
			}
		}


		#region PublicMethods
		public void Initialize(Entity vill) {
			_targetVill = vill;
		}
		public void SetOpened(bool opened) {
			_isCardOpened = opened;
			if (opened == true) {
				_lastUpdateTime = -100f;
				FixedUpdate();
			}
		}
		public void LogicDestroy() {
			Clear();
			_targetVill = null;
		}
		#endregion
	}
}