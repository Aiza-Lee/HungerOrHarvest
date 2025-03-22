using UnityEngine;

namespace NSFrame {
	[CreateAssetMenu(fileName = "ScriptTemplate", menuName = "NSFrame/ScriptTemplate")]
	public class ScriptTemplate : ScriptableObject {
		[Header("Name")] public string Name;
		[Header("Content")][TextArea(10, 50)] public string Content;
	}
}