using GameLogic.Common.Logic;
using GameLogic.Common.View;
using GameLogic.World;
using NsEcsFrame.Components;
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {
	public static class CameraFollowAPI {
		public static void SetCameraFollow(Entity entity) {
			var camera = GameWorldMono.MainWorld.CreateQueryBuilder()
				.WithAll<MainCameraComponent>().Build().First();

			var mainComp = camera.GetComponent<MainCameraComponent>();
			mainComp.TargetEntity = entity;

			var config = GameWorldMono.MainWorld.GetResource<CameraConfigResource>();
			if (entity == null) {
				// 暂停一段时间的操作，使相机回到layer上
				CameraInputAPI.TempLockInput(config.UnfollowChangeInfo.TotalTime);
				camera.GetComponent<SmoothPositionStatComponent>()
					.SetChangeInfo(config.UnfollowChangeInfo)
					.StartAChange(
						camera,
						camera.GetComponent<TransformComponent>().LocalPosition.ClosestBackLayerPosition()
					);
				camera.GetComponent<SmoothCameraSizeStatComponent>()
					.SetChangeInfo(config.SizeChangeInfo)
					.StartAChange(camera, config.CameraSizeNormal);
			} else {
				CameraInputAPI.SetCameraInputEnabled(false);
				camera.GetComponent<SmoothCameraSizeStatComponent>()
					.SetChangeInfo(config.SizeChangeInfo)
					.StartAChange(camera, config.CameraSizeFocus);
			}
		}
	}
}