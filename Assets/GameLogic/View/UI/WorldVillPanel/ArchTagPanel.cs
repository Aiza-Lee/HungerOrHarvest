using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class ArchTagPanel : MonoBehaviour {
		[Header("挂载")]
		public GameObject ArchTagPrefab;
		public Transform ArchTagParent;
		public List<Pair<ArchType, Sprite>> ArchTagIcons;
		public Sprite DefaultIcon;
		// 这里用 MoveTo 指代Spare的状态
		public Sprite MoveToIcon;
		public Sprite SleepIcon;
		// 这里用 Leave 指代NoHome的状态
		public Sprite LeaveIcon;

		private void Awake() {
			foreach (var pr in ArchTagIcons) {
				var archTag = Instantiate(ArchTagPrefab, ArchTagParent).GetComponent<ArchTag>();
				archTag.InjectInfo(pr.Value, pr.Key);
			}
			var at = Instantiate(ArchTagPrefab, ArchTagParent).GetComponent<ArchTag>();
			at.InjectInfo(MoveToIcon, TaskType.MoveTo);
			
			at = Instantiate(ArchTagPrefab, ArchTagParent).GetComponent<ArchTag>();
			at.InjectInfo(SleepIcon, TaskType.Sleep);
			
			at = Instantiate(ArchTagPrefab, ArchTagParent).GetComponent<ArchTag>();
			at.InjectInfo(LeaveIcon, TaskType.Leave);
		}

	}
}