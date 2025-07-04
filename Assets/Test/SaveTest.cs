using UnityEngine;
using NSFrame;
using GameLogic.Common.DataTypes;

namespace Test {
	public class SaveTest : MonoBehaviour {
		private void Start() {
			var saveinfo = SaveSystem.CreateSaveFile("TestSave");

			var pair = new EtPair<RepoType, float>(RepoType.Wood, 100f);
			saveinfo.SaveObject(pair);
			var savedPair = saveinfo.LoadObject<EtPair<RepoType, float>>();
			Debug.Log($"Saved Pair: {savedPair.EnumType} - {savedPair.Value}");

			var etList = new EtList<RepoType, float>(true);
			saveinfo.SaveObject(etList);
			var savedList = saveinfo.LoadObject<EtList<RepoType, float>>();
			Debug.Log($"Saved EtList: {savedList}");

			var recur = new Recur();
			saveinfo.SaveObject(recur);
			var savedRecur = saveinfo.LoadObject<Recur>();
			Debug.Log($"Saved Recur: {savedRecur.Exps}");
		}
	}
	public class Recur {
		public EtList<RepoType, float> Exps { get; set; }
		public Recur() {
			Exps = new EtList<RepoType, float>(true);
		}
	}
}