using System;
using NSFrame;
using TMPro;
using UnityEngine;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using GameLogic.UI.Common.UiComponents.PercentBar;

namespace GameLogic.UI.WorldVill {
	/// <summary>
	/// 村民展开面板中单个职业的信息，目前是等级和exp百分比显示
	/// 只作为接受数据的容器，不负责数据获取
	/// </summary>
	public class VillExpandJobInfo : MonoBehaviour, IGroupLayoutEle {

		[SerializeField] private TextMeshProUGUI _jobNameText;
		[SerializeField] private PrecentageBar _expBar;
		[SerializeField] private TextMeshProUGUI _lvNumber;

		private readonly float Height = 20f;

		private RectTransform _rectTrans;
		void Awake() {
			_rectTrans = GetComponent<RectTransform>();
		}

		#region PublicMethods
		public void LogicDestroy() {
			BelongedGroup.RemoveEle(this);
			PoolSystem.PushGO(gameObject);
		}
		public void SetContent(string jobName, int level, float expProportion) {
			_jobNameText.text = jobName;
			_lvNumber.text = level.ToString();
			_expBar.SetPercentage(expProportion);
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
			_rectTrans.localScale = Vector3.one;
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