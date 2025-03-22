using UnityEngine;

namespace GameLogic 
{
	public abstract class VillConfigBase : ScriptableObject {
		[Header("动画")] public Animator Animator;
	}
}