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
				changInfos.ForEach(info => {
					if (!info.Started) {
						info.Started = true;
						info.ElapsedTime = 0f;
						// 根据ChangeTargetType获取初始值
						info.StartValue = info.ChangeTargetType switch {
							ChangeTargetType.Transform_Position => new SmoothValue(e.GetComponent<TransformComponent>().LocalPosition),
							ChangeTargetType.Transform_Rotation => new SmoothValue(e.GetComponent<TransformComponent>().LocalRotation.eulerAngles),
							ChangeTargetType.Transform_Scale => new SmoothValue(e.GetComponent<TransformComponent>().LocalScale),
							ChangeTargetType.Renderer_Alpha => new SmoothValue(e.GetComponent<SpriteRendererComponent>().Alpha),
							ChangeTargetType.RectTransform_OffsetMin => new SmoothValue(e.GetComponent<RectTransformComponent>().OffsetMin),
							ChangeTargetType.RectTransform_OffsetMax => new SmoothValue(e.GetComponent<RectTransformComponent>().OffsetMax),
							_ => throw new System.ArgumentOutOfRangeException()
						};
					}
					info.ElapsedTime += info.IsLogicTime ? Time.deltaTime : Time.unscaledDeltaTime;
					var progress = info.TotalTime >= 0f ? Mathf.Clamp01(info.ElapsedTime / info.TotalTime) : 1f;
					var percent = curveRes.PresetCurves[info.ChangeCurveType](progress);
					var newValue = info.StartValue + (info.TargetValue - info.StartValue) * percent;
					switch (info.ChangeTargetType) {
						case ChangeTargetType.Transform_Position:
							var tComp = e.GetComponent<TransformComponent>();
							tComp.LocalPosition = newValue.Vector3Value;
							tComp.MarkDirty();
							break;
						case ChangeTargetType.Transform_Rotation:
							tComp = e.GetComponent<TransformComponent>();
							tComp.LocalRotation = Quaternion.Euler(newValue.Vector3Value);
							tComp.MarkDirty();
							break;
						case ChangeTargetType.Transform_Scale:
							tComp = e.GetComponent<TransformComponent>();
							tComp.LocalScale = newValue.Vector3Value;
							tComp.MarkDirty();
							break;
						case ChangeTargetType.Renderer_Alpha:
							var sComp = e.GetComponent<SpriteRendererComponent>();
							sComp.Alpha = newValue.FloatValue;
							sComp.MarkDirty();
							break;
						case ChangeTargetType.RectTransform_OffsetMin:
							var rComp = e.GetComponent<RectTransformComponent>();
							rComp.OffsetMin = newValue.Vector2Value;
							rComp.MarkDirty();
							break;
						case ChangeTargetType.RectTransform_OffsetMax:
							rComp = e.GetComponent<RectTransformComponent>();
							rComp.OffsetMax = newValue.Vector2Value;
							rComp.MarkDirty();
							break;
					}
				});
				statComp.ClearOveredInfos();
			});
		}
	}
}