using UnityEngine;
using NSFrame;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Components;

namespace Test {
	public class SaveTest : MonoBehaviour {
		private void Start() {
			var saveinfo = SaveSystem.CreateSaveFile("TestSave");

			var coord = new Coord(1, 2);
			saveinfo.SaveObject(coord);
			var savedCoord = saveinfo.LoadObject<Coord>();
			Debug.Log($"Saved Coord: {savedCoord}");
		}
	}
	public class Recur {
		public EtList<RepoType, float> Exps { get; set; }
		public Recur() {
			Exps = new EtList<RepoType, float>(true);
		}
	}
}