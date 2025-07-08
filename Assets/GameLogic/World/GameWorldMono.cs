using GameLogic.Common.View;
using GameLogic.Features.VillGenerator;
using NsEcsFrame.Unity;

namespace GameLogic.World {
	public class GameWorldMono : WorldBehaviour {

		protected override void RegisterSystems() {
			World.SystemManager
				.RegisterSystem<SmoothChangeSystem>()
				.RegisterSystem<TransformSyncSystem>()
				.RegisterSystem<VillGeneratorSystem>()
				.RegisterSystem<CoordToSmoothChangeStatSystem>()
			;
		}
		protected override void RegisterResources() {
			World.InsertResource(new ChangeCurveResource())
				.InsertResource(new VillGeneratorResource())
			;
		}

		void Start() {
			var villGe = World.GetResource<VillGeneratorResource>();
			villGe.VillGenerateInfos.Clear();
			villGe.VillGenerateInfos.Add(new() {
				VillStat = new(),
				Coord = new(0, 0),
				VillIdentity = new() { FirstName = "村民", LastName = "一号", Type = Common.DataTypes.VillType.Normal },
				VillJobExp = new()
			});
		}
	}
}