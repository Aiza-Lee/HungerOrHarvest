using System.Collections.Generic;
using NSFrame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class VillCard : MonoBehaviour {
		[Header("挂载")]
		public TextMeshProUGUI NameText;
		public List<Pair<TextMeshProUGUI, TextMeshProUGUI>> JobLevelTexts;
		public Image Image;

		private VillViewBase _villView;

		public int MaxJobInfoCount => JobLevelTexts.Count;
		public float Width => 130f;
		public float Height => 260f;


		private RectTransform _rectTransform;
		private void Awake() {
			_rectTransform = GetComponent<RectTransform>();
		}
		private void OnEnable() {
			EventSystem.AddListener<ulong, JobType>((int)LogicEvt.VillLevelUp_VuJ_2, OnVillLevelChange);
		}
		private void OnDisable() {
			EventSystem.RemoveListener<ulong, JobType>((int)LogicEvt.VillLevelUp_VuJ_2, OnVillLevelChange);
			_villView = null;
			Image.sprite = null;
		}
		private void Update() {
			if (_villView == null) return;
			UpdateLevelText();
		}


		private void OnVillLevelChange(ulong vID, JobType _) {
			if (_villView.Logic.ID != vID) return;
			UpdateLevelText();
		}
		private void UpdateLevelText() {
			var jobs = _villView.Logic.GetSortedJobLevels();
			jobs.Full = true;
			for (int i = 0; i < MaxJobInfoCount; i++) {
				if (i < jobs.Count) {
					JobLevelTexts[i].Key.text = jobs[i].Job.ToString();
					JobLevelTexts[i].Value.text = $"Lv.{jobs[i].Value}";
				} else {
					JobLevelTexts[i].Key.text = string.Empty;
					JobLevelTexts[i].Value.text = string.Empty;
				}
			}
		}

		#region PublicMethods
		public VillCard InjectVillView(VillViewBase villView) {
			_villView = villView;
			NameText.text = villView.Logic.FirstName + villView.Logic.LastName;
			Image.sprite = villView.Sprite;
			return this;
		}

		public void SetLeftEdge(float x) {
			_rectTransform.offsetMin = new(x, _rectTransform.offsetMin.y);
		}
		public void SetRightEdge(float x) {
			_rectTransform.offsetMax = new(x, _rectTransform.offsetMax.y);
		} 
		public void OnSetedAsChild() {
			_rectTransform.offsetMax = new(Width, Height);
			_rectTransform.offsetMin = new(0, 0);
		}
		#endregion
	}
}