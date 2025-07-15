using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Common.View;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	/// <summary>
	/// CameraMoveSystem 用于处理摄像机的移动逻辑。
	/// </summary>
	public class CameraMoveSystem : ISystem {
		public int Priority => 10050;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float deltaTime) {
			var input = _world.GetResource<CameraInputResource>();
			if (!input.EnableCameraInput) return;

			var config = _world.GetResource<CameraConfigResource>();
			var camera = _world.CreateQueryBuilder().WithAll<MainCameraComponent>().Build().GetEntities()[0];
			var smoothStat = camera.GetComponent<SmoothCameraSizeStatComponent>();

			if (input.IsSizeDirty) {
				input.IsSizeDirty = false;
				var targetSize = config.CameraSizes[input.TargetCameraSizeIndex];
				var cameraComp = camera.GetComponent<CameraComponent>();
				if (cameraComp.FeildOfView != targetSize) {
					smoothStat.SetChangeInfo(config.SizeChangeInfo)
							.StartAChange(camera, targetSize);
				}
			}

			var moveLength = config.CAMERA_MOVE_SPEED * deltaTime;
			var curPos = camera.GetComponent<TransformComponent>().LocalPosition;
			var moveStat = camera.GetComponent<SmoothPositionStatComponent>();


			if (input.MoveLeftKey) {
				var target = curPos + moveLength * Vector3.left;
				if (target.x < ConstMgr.MIN_UX) { target.x = ConstMgr.MIN_UX; }
				moveStat.SetChangeInfo(new(0, ChangeCurveType.Directive, false)).StartAChange(camera, target);

			} else if (input.MoveRightKey) {
				var target = curPos + moveLength * Vector3.right;
				if (target.x > ConstMgr.MAX_UX) { target.x = ConstMgr.MAX_UX; }
				moveStat.SetChangeInfo(new(0, ChangeCurveType.Directive, false)).StartAChange(camera, target);

			} else if (input.MoveLeftKeyUp) {
				var target = curPos + config.CAMERA_STOP_LENGTH * Vector3.left;
				if (target.x < ConstMgr.MIN_UX) { target.x = ConstMgr.MIN_UX; }
				moveStat.SetChangeInfo(config.StopPositionChangeInfo).StartAChange(camera, target);

			} else if (input.MoveRightKeyUp) {
				var target = curPos + config.CAMERA_STOP_LENGTH * Vector3.right;
				if (target.x > ConstMgr.MAX_UX) { target.x = ConstMgr.MAX_UX; }
				moveStat.SetChangeInfo(config.StopPositionChangeInfo).StartAChange(camera, target);
			}


			if (input.MoveForwardKeyDown) {
				CameraInputAPI.TempLockInput(config.ForwardPositionChangeInfo.TotalTime);
				var target = curPos + ConstMgr.LayerGap * Vector3.forward;
				if (target.z >= ConstMgr.MAX_UZ - ConstMgr.LayerGap) { target.z = ConstMgr.MAX_UZ - ConstMgr.LayerGap; }
				moveStat.SetChangeInfo(config.ForwardPositionChangeInfo).StartAChange(camera, target);

			} else if (input.MoveBackwardKeyDown) {
				CameraInputAPI.TempLockInput(config.BackwardPositionChangeInfo.TotalTime);
				var target = curPos + ConstMgr.LayerGap * Vector3.back;
				if (target.z <= ConstMgr.MIN_UZ - ConstMgr.LayerGap) { target.z = ConstMgr.MIN_UZ - ConstMgr.LayerGap; }
				moveStat.SetChangeInfo(config.BackwardPositionChangeInfo).StartAChange(camera, target);
			}

		}
	} 
}