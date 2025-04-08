using UnityEngine;

namespace GameLogic
{
	[System.Serializable]
	public abstract class VillSaveBase {
		[HideInInspector] public ulong ID;
		public VillType VillType;
		public string FirstName, LastName;
		public JTList<int> JobLevel;
		public JTList<float> JobExps;
		public RTList<float> ConsBuffs;
		public RTList<float> ProdBuffs;
		[HideInInspector] public Coord Coord;
		[HideInInspector] public VillTaskRunnerSave TaskRunner;
		[HideInInspector] public ulong HomeID;
		[HideInInspector] public ulong AttachedWorkArchID;
		

		protected abstract VillSaveBase GetDerivedClone();
		public VillSaveBase Clone() {
			var save = GetDerivedClone();
				save.ID 			= ID;
				save.VillType 		= VillType;
				save.FirstName 		= FirstName;
				save.LastName 		= LastName;
				save.JobLevel 		= JobLevel.Clone();
				save.JobExps 		= JobExps.Clone();
				save.ConsBuffs 		= ConsBuffs.Clone();
				save.ProdBuffs 		= ProdBuffs.Clone();
				save.Coord 			= Coord;
				save.TaskRunner 	= TaskRunner.Clone();
				save.HomeID 		= HomeID;
				save.AttachedWorkArchID = AttachedWorkArchID;
			return save;
		}
	}
}