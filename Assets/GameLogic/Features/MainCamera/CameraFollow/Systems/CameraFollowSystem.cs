using GameLogic.Common.Logic;
using GameLogic.Common.View;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	/// <summary>
	/// CameraFollowSystem 负责处理相机跟随逻辑
	/// </summary>
	public class CameraFollowSystem : ISystem {
		public int Priority => 19500;
		public bool Enabled { get; set; }

		private IWorld _world;
		private float _lastSetTargetTime;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) {
			var config = _world.GetResource<CameraConfigResource>();
			if (Time.time - _lastSetTargetTime < config.FollowChangeInfo.TotalTime / Mathf.PI) {
				return;
			}
			_world.CreateQueryBuilder().WithAll<MainCameraComponent>().Build().ForEach(camera => {
				var targetEntity = camera.GetComponent<MainCameraComponent>().TargetEntity;
				if (targetEntity == null || !targetEntity.IsValid()) { return; }
				_lastSetTargetTime = Time.time;
				SimpleVector3 target = targetEntity.GetComponent<TransformComponent>().LocalPosition - new SimpleVector3(0, 0, ConstMgr.LayerGap);
				target.y = ConstMgr.DEFAULT_CAMERA_HEIGHT;
				camera.GetComponent<SmoothPositionStatComponent>()
						.SetChangeInfo(config.FollowChangeInfo)
						.StartAChange(camera, target);
			});
		}
	} 
}