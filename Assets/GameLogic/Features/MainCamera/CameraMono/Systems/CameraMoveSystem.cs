using System.Collections;
using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Common.View;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NSFrame;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	/// <summary>
	/// CameraMoveSystem 用于处理摄像机的移动逻辑。
	/// </summary>
	public class CameraMoveSystem : ISystem {
		public int Priority => 10050;
		public bool Enabled { get; set; }

		private IWorld _world;
		/// <summary> 前后移动时阻碍左右移动 </summary>
		private bool _moveLocked = false;

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
			var smoothStat = camera.GetComponent<SmoothChangeStatComponent>();

			if (input.IsSizeDirty) {
				input.IsSizeDirty = false;
				var targetSize = config.CameraSizes[input.TargetCameraSizeIndex];
				var cameraComp = camera.GetComponent<CameraComponent>();
				if (cameraComp.FeildOfView != targetSize) {
					var info = PoolSystem.PopObj<SmoothChangeInfo>().InitFrom(config.DefaultCameraSizeChangeInfo, new SmoothValue(targetSize));
					smoothStat.AddNewChange(info);
				}
			}

			if (_moveLocked) return;

			var moveLength = config.CAMERA_MOVE_SPEED * deltaTime;
			var curPos = camera.GetComponent<TransformComponent>().LocalPosition;

			if (input.MoveLeftKey) {
				var target = new SmoothValue(curPos + moveLength * Vector3.left);
				var info = SmoothChangeInfo.NewDirectInfo(ChangeTargetType.Transform_Position, target);
				smoothStat.AddNewChange(info);
			} else if (input.MoveRightKey) {
				var target = new SmoothValue(curPos + moveLength * Vector3.right);
				var info = SmoothChangeInfo.NewDirectInfo(ChangeTargetType.Transform_Position, target);
				smoothStat.AddNewChange(info);
			} else if (input.MoveLeftKeyUp) {
				var target = new SmoothValue(curPos + config.CAMERA_STOP_LENGTH * Vector3.left);
				var info = PoolSystem.PopObj<SmoothChangeInfo>().InitFrom(config.DefaultCameraStopPositionChangeInfo, target);
				smoothStat.AddNewChange(info);
			} else if (input.MoveRightKeyUp) {
				var target = new SmoothValue(curPos + config.CAMERA_STOP_LENGTH * Vector3.right);
				var info = PoolSystem.PopObj<SmoothChangeInfo>().InitFrom(config.DefaultCameraStopPositionChangeInfo, target);
				smoothStat.AddNewChange(info);
			}

			if (input.MoveForwardKeyDown) {
				MonoService.Inst.StartCoroutine(LockMoveCoro(config.DefaultForwardPositionChangeInfo.TotalTime));
				var target = new SmoothValue(curPos + ConstMgr.LayerGap * Vector3.forward);
				var info = PoolSystem.PopObj<SmoothChangeInfo>().InitFrom(config.DefaultForwardPositionChangeInfo, target);
				smoothStat.AddNewChange(info);
			} else if (input.MoveBackwardKeyDown) {
				MonoService.Inst.StartCoroutine(LockMoveCoro(config.DefaultBackwardPositionChangeInfo.TotalTime));
				var target = new SmoothValue(curPos + ConstMgr.LayerGap * Vector3.back);
				var info = PoolSystem.PopObj<SmoothChangeInfo>().InitFrom(config.DefaultBackwardPositionChangeInfo, target);
				smoothStat.AddNewChange(info);
			}

		}
		IEnumerator LockMoveCoro(float time) {
			_moveLocked = true;
			yield return new WaitForSecondsRealtime(time);
			_moveLocked = false;
		}
	} 
}