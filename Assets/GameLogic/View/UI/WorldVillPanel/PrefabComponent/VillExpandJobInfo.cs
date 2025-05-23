using System;
using GameLogic.Model.Element.Vill;
using GameLogic.Model.Mgr;
using NSFrame;
using TMPro;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class VillExpandJobInfo : MonoBehaviour, IGroupLayoutEle {
		
		[SerializeField] private TextMeshProUGUI _jobNameText;
		[SerializeField] private RectTransform _expBarBack;
		[SerializeField] private RectTransform _expBarInner;
		[SerializeField] private TextMeshProUGUI _buffText;
		[SerializeField] private TextMeshProUGUI _debuffText;		
		[SerializeField] private TextMeshProUGUI _lvNumber;

		private readonly float Height = 20f;

		private float ExpBarWidth => _expBarBack.rect.width;

		private JobType _jobType;
		private VillLogicBase _villLogic;
		
		private RectTransform _rectTrans;

		void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}

		void Update() {
			_lvNumber.text = (_villLogic.GetJobLevel(_jobType) /* + 1*/ ).ToString();
			_expBarInner.offsetMax = new(-(1f - _villLogic.GetJobExpProportion(_jobType)) * ExpBarWidth, 0);
			// todo: buff debuff
		}

		#region PublicMethods
		public void Clear() {
			_villLogic = null;
			PoolSystem.PushGO(gameObject);
		}
		public void InjectVillAndJobType(VillLogicBase logic, JobType jobType) {
			_villLogic = logic;
			_jobType = jobType;
			_jobNameText.text = ConfigMgr.Config.FindJobConfig(jobType).ChineseName;
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