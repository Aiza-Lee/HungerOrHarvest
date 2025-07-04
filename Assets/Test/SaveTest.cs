using UnityEngine;
using NSFrame;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Components;
using System.Collections.Generic;
using NsEcsFrame.Core;

namespace Test {
	public class SaveTest : MonoBehaviour {
		[SerializeReference] List<IComponent> components = new() {};

		private void Start() {
			components.Add(new Coord(1, 2));

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