using UnityEngine;

namespace OldGameLogic.Model.Mgr
{
	public abstract class ArchLevelConfigBase : ScriptableObject {
		[Header("等级")] public int Level;
		[Header("容纳人数上限")] public int MaxContain;
		[Header("介绍")][TextArea(5, 30)] public string Introductions;
		[Header("固有产出")] public RTListSave<float> InherentProdVelsSave;
		[Header("额外产出/每人")] public RTListSave<float> ExtraProdVelsPerOneSave;
		[Header("固有消耗")] public RTListSave<float> InherentConsVelsSave;
		[Header("额外消耗/每人")] public RTListSave<float> ExtraConsVelsPerOneSave;
		[Header("存储量增量")] public RTListSave<float> VolumeAddsSave;
		[Header("职业经验的增量")] public JTListSave<float> ExpAddsSave;
		[Header("体力消耗速率")] public float VitConsRate;

		private RTList<float> _inherentProdVels;
		public RTList<float> InherentProdVels {
			get {
				if (_inherentProdVels == null) {
					_inherentProdVels = new();
					_inherentProdVels.InitFromSave(InherentProdVelsSave);
				}
				return _inherentProdVels;
			}
		}
		private RTList<float> _extraProdVelsPerOne;
		public RTList<float> ExtraProdVelsPerOne {
			get {
				if (_extraProdVelsPerOne == null) {
					_extraProdVelsPerOne = new();
					_extraProdVelsPerOne.InitFromSave(ExtraProdVelsPerOneSave);
				}
				return _extraProdVelsPerOne;
			}
		}
		private RTList<float> _inherentConsVels;
		public RTList<float> InherentConsVels {
			get {
				if (_inherentConsVels == null) {
					_inherentConsVels = new();
					_inherentConsVels.InitFromSave(InherentConsVelsSave);
				}
				return _inherentConsVels;
			}
		}
		private RTList<float> _extraConsVelsPerOne;
		public RTList<float> ExtraConsVelsPerOne {
			get {
				if (_extraConsVelsPerOne == null) {
					_extraConsVelsPerOne = new();
					_extraConsVelsPerOne.InitFromSave(ExtraConsVelsPerOneSave);
				}
				return _extraConsVelsPerOne;
			}
		}
		private RTList<float> _volumeAdds;
		public RTList<float> VolumeAdds {
			get {
				if (_volumeAdds == null) {
					_volumeAdds = new();
					_volumeAdds.InitFromSave(VolumeAddsSave);
				}
				return _volumeAdds;
			}
		}

		private JTList<float> _expAdds;
		public JTList<float> ExpAdds {
			get {
				if (_expAdds == null) {
					_expAdds = new();
					_expAdds.InitFromSave(ExpAddsSave);
				}
				return _expAdds;
			}
		}

	}
}