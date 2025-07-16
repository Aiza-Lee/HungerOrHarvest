using GameLogic.Common.DataTypes;
using GameLogic.Features.Elements.Decorations;
using GameLogic.World;
using NsEcsFrame.Unity;

namespace GameLogic.Features.Generator {
	public static class DecorationGeneratorAPI {
		public static void Generate(DecorationType type, Coord coord, SimpleVector3 scale, bool flipX) {
			GameWorldMono.MainWorld.GetResource<DecorationGeneratorResource>().AddDecoration(type, coord, scale, flipX);
		}
	}
}