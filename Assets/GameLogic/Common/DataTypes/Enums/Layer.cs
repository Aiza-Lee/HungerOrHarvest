using Newtonsoft.Json;
namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum LayerType {
		Grass = 0,
		Snow = 1,
		WasteLand = 2,
		Beach = 3,
		SeaEnd = 4,
		SnowMountainEnd = 5,
	}
}