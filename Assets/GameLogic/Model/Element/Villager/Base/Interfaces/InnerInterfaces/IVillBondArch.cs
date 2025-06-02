using GameLogic.Model.Element.Arch;

namespace GameLogic.Model.Element.Vill {
	public interface IVillBondArch {

		/// <summary>
		/// 绑定到建筑，绑定home和工作建筑都调用这个方法
		/// </summary>
		void BondArch(ArchLogicBase arch);
		/// <summary>
		/// 解绑工作建筑
		/// </summary>
		void DisBondWorkArch();
		/// <summary>
		/// 解绑家
		/// </summary>
		void DisBondHome();
		ulong HomeID { get; }
		ulong BondedWorkArchID { get; }
		bool IsHomeless { get; }
		bool IsWorkless { get; }
	}
}