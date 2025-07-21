using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// VillIdentityComponent 用于存储村民的身份信息，包括类型、名字和姓氏。
	/// </summary>
	public class VillIdentityComponent : IComponent {
		public VillType Type;
		public string FirstName;
		public string LastName;
	}
}