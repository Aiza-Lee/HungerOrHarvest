using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Common.View {
	/// <summary>
	/// 平滑变化系统
	/// </summary>
	public class SmoothChangeSystem : ISystem {
		public int Priority => 19000;

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
					.WithAny<SmoothAlphaStatComponent>()
					.WithAny<SmoothPositionStatComponent>()
					.WithAny<SmoothRotationStatComponent>()
					.WithAny<SmoothScaleStatComponent>()
					.WithAny<SmoothOffsetMinStatComponent>()
					.WithAny<SmoothOffsetMaxStatComponent>()
					.WithAny<SmoothAudioVolumeStatComponent>()
					.WithAny<SmoothCameraSizeStatComponent>()
					.Build();
			query.ForEach(e => {
				if (e.HasComponent<SmoothCameraSizeStatComponent>()) { CompOperate(e, e.GetComponent<SmoothCameraSizeStatComponent>()); }
				if (e.HasComponent<SmoothPositionStatComponent>()) { CompOperate(e, e.GetComponent<SmoothPositionStatComponent>()); }
				if (e.HasComponent<SmoothRotationStatComponent>()) { CompOperate(e, e.GetComponent<SmoothRotationStatComponent>()); }
				if (e.HasComponent<SmoothScaleStatComponent>()) { CompOperate(e, e.GetComponent<SmoothScaleStatComponent>()); }
				if (e.HasComponent<SmoothAlphaStatComponent>()) { CompOperate(e, e.GetComponent<SmoothAlphaStatComponent>()); }
				if (e.HasComponent<SmoothOffsetMinStatComponent>()) { CompOperate(e, e.GetComponent<SmoothOffsetMinStatComponent>()); }
				if (e.HasComponent<SmoothOffsetMaxStatComponent>()) { CompOperate(e, e.GetComponent<SmoothOffsetMaxStatComponent>()); }
				if (e.HasComponent<SmoothAudioVolumeStatComponent>()) { CompOperate(e, e.GetComponent<SmoothAudioVolumeStatComponent>()); }
			});
		}

		private void CompOperate<T>(Entity entity, SmoothChangeStatCompBase<T> comp) where T : struct {
			if (!comp.Started) return;
			if (comp.TotalTime <= 0f) {
				comp.ApplyChange(entity);
				comp.Started = false;
				return;
			}
			if (comp.ElapsedTime < comp.TotalTime) {
				comp.ElapsedTime += comp.UseLogicTime ? Time.deltaTime : Time.unscaledDeltaTime;
				if (comp.ElapsedTime > comp.TotalTime) {
					comp.ElapsedTime = comp.TotalTime;
					comp.Started = false;
				}
				comp.ApplyChange(entity);
			}
		}
	}
}