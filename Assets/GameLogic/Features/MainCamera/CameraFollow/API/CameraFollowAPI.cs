using GameLogic.Common.Logic;
using GameLogic.Common.View;
using GameLogic.World;
using NsEcsFrame.Components;
using NsEcsFrame.Core;

namespace GameLogic.Features.MainCamera {
	public static class CameraFollowAPI {
		public static void SetCameraFollow(Entity entity) {
			var camera = MainCameraAPI.GetMainCamera();

			var mainComp = camera.GetComponent<MainCameraComponent>();
			mainComp.TargetEntity = entity;

			var config = GameWorldMono.MainWorld.GetResource<CameraConfigResource>();
			if (entity == null) {
				// 对应于取消跟随
				CameraInputAPI.UnlockCameraInput();
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
				CameraInputAPI.LockCameraInput();
				camera.GetComponent<SmoothCameraSizeStatComponent>()
					.SetChangeInfo(config.SizeChangeInfo)
					.StartAChange(camera, config.CameraSizeFocus);
			}
		}
	}
}