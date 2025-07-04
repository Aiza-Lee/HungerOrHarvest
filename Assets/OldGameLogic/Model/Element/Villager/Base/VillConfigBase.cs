using UnityEngine;

namespace OldGameLogic.Model.Element.Vill
{
	public abstract class VillConfigBase : ScriptableObject {
		[Header("动画")] public Animator Animator;
	}
}