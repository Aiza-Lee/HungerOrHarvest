using System.Collections.Generic;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NSFrame;
using UnityEngine;

namespace GameLogic.Common.View {
	/// <summary>
	/// 平滑变化RectTransformComponent、TransformComponent、SpriteRendererComponent等组件的属性。
	/// <para>该系统会根据SmoothChangeStatComponent中的信息，平滑地改变目标</para>
	/// </summary>
	public class SmoothChangeSystem : ISystem {
		public int Priority => 10;

		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }

		private void ProcessSmoothChange(float deltaTime, bool isLogicTime) {
			var query = _world.CreateQueryBuilder()
					.WithAll<SmoothChangeStatComponent>()
					.Build();
			var curveRes = _world.GetResource<ChangeCurveResource>();
			query.ForEach(e => {
				var changInfos = e.GetComponent<SmoothChangeStatComponent>().SmoothChangeInfos;
				if (changInfos == null) return;
				var toRemove = new List<SmoothChangeInfo>();
				changInfos.ForEach(info => {
					if (info.IsLogicTime != isLogicTime) return;
					info.ElapsedTime += deltaTime;
					var progress = Mathf.Clamp01(info.ElapsedTime / info.TotalTime);
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
					if (info.ElapsedTime >= info.TotalTime) {
						toRemove.Add(info);
					}
				});
				toRemove.ForEach(info => {
					changInfos.Remove(info);
					PoolSystem.PushObj(info);
				});
			});
		}

		public void OnLogicUpdate(float deltaTime) {
			ProcessSmoothChange(deltaTime, true);
		}

		public void OnRenderUpdate(float deltaTime) {
			ProcessSmoothChange(deltaTime, false);
		}
	}
}