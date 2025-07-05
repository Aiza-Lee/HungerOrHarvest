using UnityEngine;
using NSFrame;
using GameLogic.Common.DataTypes;
using GameLogic.Resources.MainCamera;

namespace Test {
	public class SaveTest : MonoBehaviour {
		private void Start() {
			var saveinfo = SaveSystem.CreateSaveFile("TestSave");

			var res = new MainCameraResource() {
				Size = CameraSize.Focus,
				MoveSpeed = 5.0f,
				CurFocusEntityId = null,
			};
			saveinfo.SaveObject(res);
			var saved = saveinfo.LoadObject<MainCameraResource>();
			Debug.Log($"Camera Size: {saved.Size}, Move Speed: {saved.MoveSpeed}, Focus Entity ID: {saved.CurFocusEntityId}");
		}
	}
	public class Recur {
		public EtList<RepoType, float> Exps { get; set; }
		public Recur() {
			Exps = new EtList<RepoType, float>(true);
		}
	}
}