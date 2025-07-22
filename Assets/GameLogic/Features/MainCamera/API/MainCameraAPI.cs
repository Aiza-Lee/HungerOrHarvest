using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {
	public static class MainCameraAPI {
		private static Entity _mainCamera;
		public static Entity GetMainCamera() =>
			_mainCamera ??= GameWorldMono.MainWorld.CreateQueryBuilder().WithAll<MainCameraComponent>().Build().GetEntities()[0];

	}
}