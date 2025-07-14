using GameLogic.World;
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {
	public static class CameraFollowAPI {
		public static void SetCemeraFollow(Entity entity) {
			GameWorldMono.MainWorld.CreateQueryBuilder()
				.WithAll<MainCameraComponent>().Build()
				.ForEach(camera => {
					var mainCameraComp = camera.GetComponent<MainCameraComponent>();
					mainCameraComp.TargetEntity = entity;
				});
				
			if (entity == null) {
				CameraInputAPI.SetCameraInputEnabled(true);
			} else if (!GameWorldMono.MainWorld.GetResource<CameraInputResource>().EnableCameraInput) {
				CameraInputAPI.SetCameraInputEnabled(false);
			}
		}
	}
}