using Newtonsoft.Json;
namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum VillType {
		Normal = 0,
	}
}