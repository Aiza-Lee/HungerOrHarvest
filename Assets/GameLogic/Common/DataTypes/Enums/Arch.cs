using Unity.Plastic.Newtonsoft.Json;

namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Unity.Plastic.Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum ArchType {
		/* 居住 */
		Cottage = 0,
		HunterCabin = 1,
		/* 特殊 */
		Ruin = 2,
	}
}