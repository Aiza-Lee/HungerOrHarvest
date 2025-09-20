using Newtonsoft.Json;

namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum JobType {
		Farmer = 0,
		Timberjack = 1,
		Hunter = 2,
		Fisher = 3,
		Miner = 4,
		Blacksmith = 5,
	}
}