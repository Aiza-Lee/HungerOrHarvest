using UnityEngine;

namespace GameLogic
{
	public abstract class ArchLevelConfigBase : ScriptableObject {
		[Header("等级")] public int Level;
		[Header("容纳人数上限")] public int MaxContain;
		[Header("介绍")][TextArea(5, 30)] public string Introductions;
		[Header("固有产出")] public RTList<float> InherentProdVels;
		[Header("额外产出/每人")] public RTList<float> ExtraProdVelsPerOne;
		[Header("固有消耗")] public RTList<float> InherentConsVels;
		[Header("额外消耗/每人")] public RTList<float> ExtraConsVelsPerOne;
		[Header("存储量增量")] public RTList<float> VolumeAdds;
		[Header("职业经验的增量")] public JTList<float> ExpAdds;
	}
}