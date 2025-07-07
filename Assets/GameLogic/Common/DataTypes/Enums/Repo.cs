using Unity.Plastic.Newtonsoft.Json;
namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Unity.Plastic.Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum RepoType {
		Wood = 0,
		Food = 1,
		Water = 2,
		Iron = 3,
		Meat = 4,
		Villager = 5,
		Science = 6,
	}
}