using Unity.Plastic.Newtonsoft.Json;

namespace GameLogic.Common.DataTypes {
	[JsonConverter(typeof(Unity.Plastic.Newtonsoft.Json.Converters.StringEnumConverter))]
	public enum ArchType {
		Ruins = 0,
		Cottage = 1,
		HuntingCabin = 2,
		StoneMine = 3,
		FishingDock = 4,
		Farmland = 5,
		CarpentryShop = 6,
		ClayPit = 7,
		CopperMine = 8,
		LumberMill = 9,
		MeteoriteMine = 10,
	}
}