using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Model.Element.Arch
{
	[System.Serializable]
	public abstract class ArchSaveBase {
		abstract public ArchType ArchType { get; }
		[HideInInspector] public ulong ID;
		[HideInInspector] public OL OL;
		[HideInInspector] public int Level;
		[HideInInspector] public RTListSave<float> ProdBuffs;
		[HideInInspector] public RTListSave<float> ConsBuffs;
		[HideInInspector] public List<ulong> BondedVillIDs;
		[HideInInspector] public List<ulong> InVillIDs;

		protected abstract ArchSaveBase GetDerivedClone();
		public ArchSaveBase Clone() {
			var save = GetDerivedClone();
				save.ID 			= ID;
				save.OL 			= OL;
				save.Level 			= Level;
				save.ProdBuffs 		= ProdBuffs.Clone();
				save.ConsBuffs 		= ConsBuffs.Clone();
				save.BondedVillIDs 	= new(BondedVillIDs);
				save.InVillIDs 		= new(InVillIDs);
			return save;
		}
	}
}