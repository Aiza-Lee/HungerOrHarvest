using UnityEngine;

namespace GameLogic
{
	public abstract class LayerConfigBase : ScriptableObject {
		[Header("类型")] public LayerType LayerType;
		[Header("名称")] public string Name;
		[Header("介绍")][TextArea(5, 30)] public string Introductions;
	}
}