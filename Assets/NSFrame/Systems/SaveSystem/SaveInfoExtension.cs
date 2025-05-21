using System;
using UnityEngine;

namespace NSFrame 
{
	public static class SaveInfoExtension {
		public static void SaveObject(this SaveInfo si, object obj) {
			SaveSystem.SaveObject(si, obj);
		}
		public static void SaveObjects(this SaveInfo si, object[] objs) {
			SaveSystem.SaveObjects(si, objs);
		}


		public static T LoadObject<T>(this SaveInfo si) where T : class {
			return SaveSystem.LoadObject<T>(si);
		}
		public static object LoadObject(this SaveInfo si, Type type) {
			return SaveSystem.LoadObject(si, type);
		}


		public static void DeleteSaveInfo(this SaveInfo si) {
			SaveSystem.DeleteSaveFile(si);
		}
	}	
}