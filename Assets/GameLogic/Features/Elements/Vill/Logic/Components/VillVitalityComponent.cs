using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// VillStatComponent 用于存储村民的体力值、恢复次数和死亡状态。
	/// </summary>
	public class VillVitalityComponent : IComponent {
		public float Vit;
		public int RecoverChances;
		public bool IsDying;
		public bool Die;
	}
}