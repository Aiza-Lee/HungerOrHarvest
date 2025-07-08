using GameLogic.Common.DataTypes;
using UnityEngine;

namespace GameLogic.Features.Vill {
	[CreateAssetMenu(fileName = "NormalVillConfig", menuName = "HungerOrHarvest/Config/Vill/Normal")]
	public class NormalVillConfig : VillConfigBase {
		public override VillType VillType => VillType.Normal;
	}
}