using GameLogic.Model.Element.Vill;

namespace GameLogic.Model.Element.Arch
{
	/// <summary>
	/// 建筑绑定村民的接口，由村民调用建筑绑定村民的逻辑
	/// </summary>
	public interface IBondVill {
		bool CheckBondVill();
		bool HasBondedVill(ulong vID);

		/// <summary>
		/// 绑定村民，如果绑定成功，则返回 true，否则返回 false
		/// </summary>
		bool BondVill(ulong id);
		/// <summary>
		/// 解绑村民，如果解绑成功，则返回 true，否则返回 false
		/// </summary>
		bool DisBondVill(ulong id);
	}
}