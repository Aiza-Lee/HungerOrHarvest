using GameLogic.Common.UnityComponentsBridge;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Common.View {
	/// <summary>
	/// CameraSyncSystem 负责将实体的 CameraComponent 实时同步到 Unity 的 Camera。
	/// </summary>
	public class CameraSyncSystem : ISystem {
		public int Priority => 20000;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) {
			var query = _world.CreateQueryBuilder()
				.WithAll<CameraComponent>()
				.Build();
			query.ForEach(static e => {
				var cameraComp = e.GetComponent<CameraComponent>();
				if (!cameraComp.IsDirty()) return;
				var go = EntityMono.GetByEntityId(e.ID);
				if (!go.TryGetComponent<Camera>(out var camera)) {
					Debug.LogWarning($"Entity {e.ID} does not have a Camera component attached.");
					return;
				}
				camera.fieldOfView = cameraComp.FeildOfView;
				cameraComp.ClearDirty();
			});
		}
	} 
}