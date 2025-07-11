using GameLogic.Common.UnityComponentsBridge;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Common.View {
	/// <summary>
	/// 平滑变化RectTransformComponent、TransformComponent、SpriteRendererComponent等组件的属性。
	/// <para>该系统会消耗SmoothChangeStatComponent中的信息，平滑地改变目标</para>
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
					.WithAll<SmoothChangeStatComponent>()
					.Build();
			var curveRes = _world.GetResource<ChangeCurveResource>();
			query.ForEach(e => {
				var statComp = e.GetComponent<SmoothChangeStatComponent>();
				var changInfos = statComp.SmoothChangeInfos;
				if (changInfos.Count == 0) return;
				foreach (var info in changInfos.Values) {
					if (!info.Started) {
						info.Started = true;
						info.ElapsedTime = 0f;
						// 根据ChangeTargetType获取初始值
						info.StartValue = info.ChangeTargetType switch {
							ChangeTargetType.Transform_Position => new SmoothValue(e.GetComponent<TransformComponent>().LocalPosition),
							ChangeTargetType.Transform_Rotation => new SmoothValue(e.GetComponent<TransformComponent>().LocalRotation),
							ChangeTargetType.Transform_Scale => new SmoothValue(e.GetComponent<TransformComponent>().LocalScale),
							ChangeTargetType.Renderer_Alpha => new SmoothValue(e.GetComponent<SpriteRendererComponent>().Color.a),
							ChangeTargetType.RectTransform_OffsetMin => new SmoothValue(e.GetComponent<RectTransformComponent>().OffsetMin),
							ChangeTargetType.RectTransform_OffsetMax => new SmoothValue(e.GetComponent<RectTransformComponent>().OffsetMax),
							ChangeTargetType.Camera_Size => new SmoothValue(e.GetComponent<CameraComponent>().FeildOfView),
							ChangeTargetType.AudioSource_Volume => new SmoothValue(e.GetComponent<AudioSourceComponent>().Volume),
							_ => throw new System.ArgumentOutOfRangeException()
						};
					}
					info.ElapsedTime += info.IsLogicTime ? Time.deltaTime : Time.unscaledDeltaTime;
					var progress = info.TotalTime >= 0f ? Mathf.Clamp01(info.ElapsedTime / info.TotalTime) : 1f;
					var percent = curveRes.PresetCurves[info.ChangeCurveType](progress);
					var newValue = info.StartValue + (info.TargetValue - info.StartValue) * percent;
					ApplyChange(e, info, newValue);
				}
				statComp.ClearOveredInfos();
			});
		}

		private void ApplyChange(Entity e, SmoothChangeInfo info, SmoothValue newValue) {
			switch (info.ChangeTargetType) {
				case ChangeTargetType.Transform_Position:
					UpdateComp<TransformComponent>(e, t => t.LocalPosition = newValue.Vector3Value); break;
				case ChangeTargetType.Transform_Rotation:
					UpdateComp<TransformComponent>(e, t => t.LocalRotation = Quaternion.Euler(newValue.Vector3Value)); break;
				case ChangeTargetType.Transform_Scale:
					UpdateComp<TransformComponent>(e, t => t.LocalScale = newValue.Vector3Value); break;
				case ChangeTargetType.Renderer_Alpha:
					UpdateComp<SpriteRendererComponent>(e, s => s.Color.ModifyAlpha(newValue.FloatValue)); break;
				case ChangeTargetType.RectTransform_OffsetMin:
					UpdateComp<RectTransformComponent>(e, r => r.OffsetMin = newValue.Vector2Value); break;
				case ChangeTargetType.RectTransform_OffsetMax:
					UpdateComp<RectTransformComponent>(e, r => r.OffsetMax = newValue.Vector2Value); break;
				case ChangeTargetType.Camera_Size:
					UpdateComp<CameraComponent>(e, c => c.FeildOfView = newValue.FloatValue); break;
				case ChangeTargetType.AudioSource_Volume:
					UpdateComp<AudioSourceComponent>(e, a => a.Volume = newValue.FloatValue); break;
			}
		}

		private void UpdateComp<T>(Entity e, System.Action<T> updateAction) where T : class, IComponent, IDirtyMarker {
			var comp = e.GetComponent<T>();
			updateAction(comp);
			comp.MarkDirty();
		}
	}
}