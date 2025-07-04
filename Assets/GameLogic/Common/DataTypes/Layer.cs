using Unity.Plastic.Newtonsoft.Json;
namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Unity.Plastic.Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum LayerType {
		Grass,
		Snow,
		WasteLand,
		Beach,
		SeaEnd,
		SnowMountainEnd,
	}
}