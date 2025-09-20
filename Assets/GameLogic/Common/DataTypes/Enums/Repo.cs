using Newtonsoft.Json;
namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum RepoType {
		Wood = 0,
		Food = 1,
		// Water = 2,
		// Iron = 3,
		// Meat = 4,
		Science = 6,
		Stone = 7,
		Clay = 8,
		Copper = 9,
		Hide = 10,
	}
}