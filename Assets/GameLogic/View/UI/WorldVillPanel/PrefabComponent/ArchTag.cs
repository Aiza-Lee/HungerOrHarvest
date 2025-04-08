using UnityEngine;
using UnityEngine.UI;

namespace GameLogic.View.UI.WorldVillPanel
{
	public class ArchTag : MonoBehaviour {
		private Image _image;
		private ArchType _archType;
		private TaskType _taskType = TaskType.None;

		private void Awake() {
			_image = GetComponent<Image>();
		}

		public void InjectInfo(Sprite sprite, ArchType archType) {
			_image.sprite = sprite;
			_archType = archType;
		}
		public void InjectInfo(Sprite sprite, TaskType taskType) {
			_image.sprite = sprite;
			_taskType = taskType;
		}
	}
}