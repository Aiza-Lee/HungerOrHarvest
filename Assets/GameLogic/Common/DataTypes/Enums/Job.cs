using Unity.Plastic.Newtonsoft.Json;

namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Unity.Plastic.Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum JobType {
		Farmer = 0,
		Timberjack = 1,
		Hunter = 2,
	}
}