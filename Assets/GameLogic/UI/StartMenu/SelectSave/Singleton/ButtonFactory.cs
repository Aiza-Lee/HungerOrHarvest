using NSFrame;
using UnityEngine;

namespace GameLogic.UI.StartMenu {
	public class ButtonFactory : MonoSingleton<ButtonFactory> {
		[SerializeField] private GameObject _saveInfoButtonElePrefab;
		[SerializeField] private GameObject _worldButtonElePrefab;
		protected override void Awake() {
			base.Awake();
			PoolSystem.InitPrefabPool(_saveInfoButtonElePrefab, 7);
			PoolSystem.InitPrefabPool(_worldButtonElePrefab, 15);
		}

		public SaveInfoButtonEle CreateSaveInfoButton(SaveInfo si) {
			var button = PoolSystem.PopGO<SaveInfoButtonEle>(_saveInfoButtonElePrefab);
			button.SetSaveInfo(si);
			return button;
		}
		public WorldButtonEle CreateWorldButton(string worldName) {
			var button = PoolSystem.PopGO<WorldButtonEle>(_worldButtonElePrefab);
			button.SetInfo(worldName);
			return button;
		}
	}
}