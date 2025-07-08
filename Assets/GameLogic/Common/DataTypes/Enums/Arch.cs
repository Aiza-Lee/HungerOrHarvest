using Unity.Plastic.Newtonsoft.Json;

namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Unity.Plastic.Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum ArchType {
		Ruin = 0,
		Cottage = 1,
		HunterCabin = 2,
		StoneMine = 3,
		FishingGround = 4,
		Farmland = 5,
		CarpenterWorkshop = 6,
		ClayPit = 7,
		CopperMine = 8,
	}
}