using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NSFrame;

namespace GameLogic.Features.MainCamera {
	/// <summary>
	/// CameraCloseAlphaDecreaseSystem 负责...（请补充描述）
	/// </summary>
	public class CameraCloseAlphaDecreaseSystem : ISystem {
		public int Priority => 19500;
		public bool Enabled { get; set; }

		// 相机观察距离小于这个值乘LayerGap的会开始按照比例变透明
		private const float TRANSPARENT_BOUNDARY = 0.8f;

		private IWorld _world;
		private EntityQueryBuilder _query;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_query = world.CreateQueryBuilder().WithAll<SpriteRendererComponent>();
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) {
			var camera = MainCameraAPI.GetMainCamera();
			var cameraTrans = camera.GetComponent<TransformComponent>();
			_query.Build().ForEach(entity => {
				var trans = entity.GetComponent<TransformComponent>();
				var sr = entity.GetComponent<SpriteRendererComponent>();
				var deltaZ = trans.LocalPosition.z - cameraTrans.LocalPosition.z;
				if (deltaZ > ConstMgr.LayerGap * TRANSPARENT_BOUNDARY || deltaZ < 0f) {
					if (sr.Color.a != 1f) {
						sr.Color.a = 1f;
						sr.Dirty = true;
					}
				} else {
					sr.Color.a = deltaZ / (ConstMgr.LayerGap * TRANSPARENT_BOUNDARY);
					sr.Dirty = true;
				}
			});
		}
	} 
}