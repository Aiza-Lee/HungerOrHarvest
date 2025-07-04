using Unity.Plastic.Newtonsoft.Json;

namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Unity.Plastic.Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum ArchType {
		/* 居住 */
		Cottage,
		HunterCabin,
		/* 特殊 */
		Ruin,
	}
}