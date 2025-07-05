using NsEcsFrame.Core;

namespace GameLogic.Common.Render {
	public class SmoothChangeStatComp : IComponent {
		public float CurTime;
		public bool IsChanging;
		public void CopyFrom(IComponent other) {
			if (other is SmoothChangeStatComp otherComp) {
				CurTime = otherComp.CurTime;
				IsChanging = otherComp.IsChanging;
			} else {
				throw new System.ArgumentException("Cannot copy from non-SmoothChangeStatComp component");
			}
		}
	}
}