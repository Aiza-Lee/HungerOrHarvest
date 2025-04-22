using System;
using System.Collections.Generic;
using NSFrame;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class VillCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IGroupLayoutEle {
		[SerializeField] private TextMeshProUGUI _nameText;
		[SerializeField] private List<Pair<TextMeshProUGUI, TextMeshProUGUI>> _jobLevelTexts;
		[SerializeField] private Image _image;
		[SerializeField] private RectTransform _expandMask;
		[SerializeField] private GameObject _lightingEdge;
		[SerializeField] private JobInfoLayout _jobInfoLayout;

		private SmoothOffsetMin _expandMaskSOMin;
		private SmoothOffsetMax _expandMaskSOMax;
		private SmoothOffsetMin _thisSOMin;
		private SmoothOffsetMax _thisSOMax;
		private SmoothScale _smoothScale;
		private MainPanel _mainPanel;

		private VillViewBase _villView;
		public const float MAX_WIDTH = 500f;
		public const float MIN_WIDTH = 130f;
		public const float HEIGHT = 260f;

		public int MaxJobInfoCount => _jobLevelTexts.Count;
		public ulong AttachedVillID => _villView.Logic.ID;
		public bool Selected { get; private set; }

		private RectTransform _rectTrans;
		private void Awake() {
			_expandMaskSOMin = _expandMask.GetComponent<SmoothOffsetMin>();
			_expandMaskSOMax = _expandMask.GetComponent<SmoothOffsetMax>();
			_thisSOMin = GetComponent<SmoothOffsetMin>();
			_thisSOMax = GetComponent<SmoothOffsetMax>();
			_smoothScale = GetComponent<SmoothScale>();
			_rectTrans = GetComponent<RectTransform>();
		}
		private void Start() {
			UIMgr.Inst.FindPanel(out _mainPanel);
		}
		private void OnEnable() {
			NSFrame.EventSystem.AddListener<ulong, JobType>((int)ModelEvt.VillLevelUp_VuJ_2, OnVillLevelChange, NSFrame.EventType.Model);
		}
		private void OnDisable() {
			NSFrame.EventSystem.RemoveListener<ulong, JobType>((int)ModelEvt.VillLevelUp_VuJ_2, OnVillLevelChange, NSFrame.EventType.Model);
		}
		private void Update() {
			if (_villView == null) return;
			UpdateLevelText();
		}

		#region Injection
		public VillCard InjectVillView(VillViewBase villView) {
			_villView = villView;
			_nameText.text = villView.Logic.FirstName + villView.Logic.LastName;
			_image.sprite = villView.Sprite;
			return this;
		}
		#endregion

		private void UpdateLevelText() {
			var jobs = _villView.Logic.GetSortedJobLevels();
			jobs.Full = true;
			for (int i = 0; i < MaxJobInfoCount; i++) {
				if (i < jobs.Count) {
					_jobLevelTexts[i].Key.text = jobs[i].Job.ToString();
					_jobLevelTexts[i].Value.text = $"Lv.{jobs[i].Value}";
				} else {
					_jobLevelTexts[i].Key.text = string.Empty;
					_jobLevelTexts[i].Value.text = string.Empty;
				}
			}
		}

		private void Expand() {
			_jobInfoLayout.Init(AttachedVillID);
			_expandMaskSOMax
				.SetOnChanged((val) => OnDirty?.Invoke())
				.SetTarget(new(MAX_WIDTH, _expandMask.offsetMax.y));
		}
		private void Shrink() {
			_expandMaskSOMax
				.SetOnChanged((val) => OnDirty?.Invoke())
				.SetStopCallback(() => _jobInfoLayout.Clear())
				.SetTarget(new(MIN_WIDTH, _expandMask.offsetMax.y));
		}

		#region PublicMethods
		public void Clear() {
			_villView = null;
			_image.sprite = null;
			Selected = false;
			_lightingEdge.SetActive(false);
			_jobInfoLayout.Clear();
			PoolSystem.PushGO(gameObject);
		}

		public void TransferTo(RectTransform target, Action<VillCard> callBack) {
			_rectTrans.SetParent(target);
			_rectTrans.SetAsLastSibling();
			_thisSOMin
				.SetMod(1)
				.SetOnChanged((val) => _thisSOMax.SetCurVal(new(val.x + MIN_WIDTH, val.y + HEIGHT)))
				.SetTarget(new(0f, 0f));
			_smoothScale
				.SetDoneCallback(() => { callBack?.Invoke(this); })
				.SetTarget(new(0f, 0f, 0f));
		}
		#endregion

		#region EventSystem
		private void OnVillLevelChange(ulong vID, JobType _) {
			if (_villView.Logic.ID != vID) return;
			UpdateLevelText();
		}
		#endregion

		#region IGroupLayoutEle
		public GroupLayoutBase BelongedGroup { get; set; }
		public RectTransform RectTrans => _rectTrans;
		public float Width => _expandMask.rect.width;
		public float Height => HEIGHT;
		public event Action OnDirty;
		public void SetPos(float x) {
			_thisSOMax.SetCurVal(new(x + MIN_WIDTH, _rectTrans.offsetMax.y));
			_thisSOMin.SetCurVal(new(x, _rectTrans.offsetMin.y));
		}
		public void OnAddedToGroup() {
			_smoothScale.SetCurVal(new(1, 1, 1));
			_thisSOMax.SetCurVal(new(MIN_WIDTH, HEIGHT));
			_thisSOMin.SetCurVal(new(0, 0));
			_expandMaskSOMax.SetCurVal(new(MIN_WIDTH, 0));
			_expandMaskSOMin.SetCurVal(new(0, 0));
		}
		#endregion

		#region IPointerEnterHandler
		public void OnPointerEnter(PointerEventData _) {
			if (Selected) return;
			Expand();
		}
		#endregion
		#region IPointerExitHandler
		public void OnPointerExit(PointerEventData _) {
			if (Selected) return;
			Shrink();
			Controller.CmdRunner.Run("/cam-free");
		}
		#endregion
		#region IPointerClickHandler
		public void OnPointerClick(PointerEventData _) {
			if (Input.GetKey(KeyCode.LeftControl)) {
				Controller.CmdRunner.Run("/cam-free");
				Selected = !Selected;
				_lightingEdge.SetActive(Selected);

				if (Selected) {
					_mainPanel.AddSelectedVillId(this);
					Shrink();
				} else {
					_mainPanel.RemoveSelectedVillId(this);
					Expand();
				}

			} else {
				Controller.CmdRunner.Run("/cam-focus-vill " + _villView.Logic.ID);
			}
		}
		#endregion
	}
}