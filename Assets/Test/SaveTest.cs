using UnityEngine;
using NSFrame;
using GameLogic.Common.Components;
using GameLogic.Common.DataTypes;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Test {
	public class SaveTest : MonoBehaviour {
		private void Start() {
			var saveinfo = SaveSystem.CreateSaveFile("TestSave");

			var coord = new Coord(1, 2);
			saveinfo.SaveObject(coord);

			var repotype = RepoType.Food;
			saveinfo.SaveObject(repotype);

			var objToSave = new ObjToSave(1, "TestObject", 3.14f);
			saveinfo.SaveObject(objToSave);

			var objToSave2 = new ObjToSave(2, "TestObject2", 6.28f);
			var objToSave3 = new ObjToSave(3, "TestObject3", 9.42f);

			var sthToSave = new SthToSave(1, "TestSth", 1.23f);
			var sthToSave2 = new SthToSave(2, "TestSth2", 4.56f);
			var sthToSave3 = new SthToSave(3, "TestSth3", 7.89f);

			var list = new List<ISaveThing> { objToSave, objToSave2, objToSave3, sthToSave, sthToSave2, sthToSave3 };
			var dict = new Dictionary<int, ISaveThing> {
				{ 1, objToSave },
				{ 2, objToSave2 },
				{ 3, objToSave3 },
				{ 4, sthToSave },
				{ 5, sthToSave2 },
				{ 6, sthToSave3 }
			};
			var recur = new Recur(list, dict);
			saveinfo.SaveObject(recur);

			var savedrecur = saveinfo.LoadObject<Recur>();
			Debug.Log($"Recur List Count: {savedrecur.List.Count}");
		}
	}
	public interface ISaveThing {}
	public class ObjToSave : ISaveThing {
		public int Id { get; set; }
		public string Name { get; set; }
		public float Value { get; set; }

		public ObjToSave(int id, string name, float value) {
			Id = id;
			Name = name;
			Value = value;
		}
	}
	public class SthToSave : ISaveThing {
		public int Id { get; set; }
		public string Name { get; set; }
		public float Value { get; set; }

		public SthToSave(int id, string name, float value) {
			Id = id;
			Name = name;
			Value = value;
		}
	}
	public class Recur {
		public List<ISaveThing> List { get; private set; }
		public Dictionary<int, ISaveThing> Dict { get; private set; }

		public Recur() {
			List = new List<ISaveThing>();
			Dict = new Dictionary<int, ISaveThing>();
		}
		public Recur(List<ISaveThing> list, Dictionary<int, ISaveThing> dict) {
			List = list;
			Dict = dict;
		}
	}
}