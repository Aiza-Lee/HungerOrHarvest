using NsEcsFrame.Components;
using NsEcsFrame.Core;

namespace GameLogic.Common.UnityComponentsBridge {
	public class AudioSourceComponent : IComponent, IDirtyMarker {
		public float Volume;
		public bool Dirty = true;

		public void MarkDirty() => Dirty = true;
		public void ClearDirty() => Dirty = false;
		public bool IsDirty() => Dirty;
	}
}