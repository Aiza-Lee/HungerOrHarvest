using Unity.Plastic.Newtonsoft.Json;
namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Unity.Plastic.Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum RepoType {
		Wood,
		Food,
		Water,
		Iron,
		Meat,
		Villager,
		Science,
	}
}