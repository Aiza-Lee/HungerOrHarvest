using System;
using NSFrame;
using TMPro;
using UnityEngine;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using GameLogic.UI.Common.UiComponents.PercentBar;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using GameLogic.Features.Job;
using GameLogic.Features.Elements.Vill;

namespace GameLogic.UI.WorldVill {
	/// <summary>
	/// 村民展开面板中单个职业的信息，目前是等级和exp百分比显示
	/// </summary>
	public class VillExpandJobInfo : MonoBehaviour, IGroupLayoutEle {

		[SerializeField] private TextMeshProUGUI _jobNameText;
		[SerializeField] private PrecentageBar _expBar;
		[SerializeField] private TextMeshProUGUI _lvNumber;

		private readonly float Height = 20f;

		private JobType _jobType;
		private Entity _targetVill;

		[SerializeField] private int _updateInterval = 3; // 更新间隔
		private int _updateCounter = 0;

		private RectTransform _rectTrans;
		void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}

		void FixedUpdate() {
			if (_targetVill == null) { return; }
			_updateCounter++;
			if (_updateCounter < _updateInterval) { return; }
			_updateCounter = 0;
			if (!_targetVill.IsValid()) { LogicDestroy(); return; }

			_lvNumber.text = VillQueryAPI.GetJobLevelExp(_targetVill, _jobType).Item1.ToString();
			_expBar.SetPercentage(VillQueryAPI.GetJobExpProportion(_targetVill, _jobType));
		}

		#region PublicMethods
		public void LogicDestroy() {
			_targetVill = null;
			PoolSystem.PushGO(gameObject);
		}
		public void SetJobInfo(Entity vill, JobType jobType) {
			_targetVill = vill;
			_jobType = jobType;
			_jobNameText.text = JobQueryAPI.GetJobName(jobType);
		}

		#endregion

		#region IGroupLayoutEle
		public GroupLayoutBase BelongedGroup { get; set; }
		public float EleSize => Height;
		public RectTransform RectTrans => _rectTrans;
#pragma warning disable 67
		public event Action OnDirty;
#pragma warning restore 67
		public void OnAddedToGroup() {
			_rectTrans.offsetMin = new(0, 0);
			_rectTrans.offsetMax = new(0, Height);
		}
		public void SetPos(float x) {
			_rectTrans.offsetMax = new(0, -x);
			_rectTrans.offsetMin = new(0, -x - Height);
		}
		#endregion
	}
}