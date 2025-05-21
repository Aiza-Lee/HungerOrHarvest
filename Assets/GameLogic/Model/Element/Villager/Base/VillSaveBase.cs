using GameLogic.Model.Element.Vill;
using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public abstract class VillSaveBase {
		[HideInInspector] public ulong ID;
		public VillType VillType;
		public string FirstName, LastName;
		[HideInInspector] public Coord Coord;
		[HideInInspector] public TaskRunnerSave TaskRunner;
		[HideInInspector] public ExpHelperSave ExpHelper;
		[HideInInspector] public ulong HomeID;
		[HideInInspector] public ulong AttachedWorkArchID;
		

		protected abstract VillSaveBase GetDerivedClone();
		public VillSaveBase Clone() {
			var save = GetDerivedClone();
				save.ID 			= ID;
				save.VillType 		= VillType;
				save.FirstName 		= FirstName;
				save.LastName 		= LastName;
				save.Coord 			= Coord;
				save.TaskRunner 	= TaskRunner.Clone();
				save.ExpHelper 		= ExpHelper.Clone();
				save.HomeID 		= HomeID;
				save.AttachedWorkArchID = AttachedWorkArchID;
			return save;
		}
	}
}