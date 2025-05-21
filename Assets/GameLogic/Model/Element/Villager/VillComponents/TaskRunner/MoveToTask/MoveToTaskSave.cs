using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	[System.Serializable]
	public class MoveToTaskSave : TaskSaveBase {
		[HideInInspector] public Coord Target;
		[HideInInspector] public List<Coord> Route;
		[HideInInspector] public int Timer;
		[HideInInspector] public int Idx;
		[HideInInspector] public MoveToTargetType TargetType;
		protected override TaskSaveBase Clone_Derived() {
			return new MoveToTaskSave() {
				Target = Target,
				Route = new(Route),
				Timer = Timer,
				Idx = Idx,
				TargetType = TargetType,
			};
		}
	}
}