using NsEcsFrame.Components;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Common.Render {
	public class SmoothChangeSystem : ISystem {
		public int Priority => throw new System.NotImplementedException();

		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() {}

		public void OnDestroy() {}

		public void OnLogicUpdate(float deltaTime) {
			var query = _world.CreateQueryBuilder()
							.WithAll<SmoothChangeStatComp>()
							.Build();
			var curveRes = _world.GetResource<ChangeCurveResource>();
			query.ForEach(e => {
				var changInfos = e.GetComponent<SmoothChangeStatComp>().SmoothChangeInfos;
				changInfos?.ForEach(info => {
					if (!info.IsLogicTime) return;
					if (!info.IsChanging) return;
					info.ElapsedTime += deltaTime;
					var progress = Mathf.Clamp01(info.ElapsedTime / info.TotalTime);
					var percent = curveRes.PresetCurves[info.ChangeCurveType](progress);
					var newValue = info.StartValue + (info.TargetValue - info.StartValue) * percent;
					switch (info.ChangeTargetType) {
						case ChangeTargetType.Transform_Position:
							e.GetComponent<TransformComponent>().LocalPosition = newValue.Vector3Value;
							break;
						case ChangeTargetType.Transform_Rotation:
							e.GetComponent<TransformComponent>().LocalRotation = Quaternion.Euler(newValue.Vector3Value);
							break;
						case ChangeTargetType.Transform_Scale:
							e.GetComponent<TransformComponent>().LocalScale = newValue.Vector3Value;
							break;
						case ChangeTargetType.Renderer_Alpha:
							e.GetComponent<SpriteRendererComponent>().Alpha = newValue.FloatValue;
							break;
						case ChangeTargetType.RectTransform_OffsetMin:
							e.GetComponent<RectTransformComponent>().OffsetMin = newValue.Vector2Value;
							break;
						case ChangeTargetType.RectTransform_OffsetMax:
							e.GetComponent<RectTransformComponent>().OffsetMax = newValue.Vector2Value;
							break;
					}
					if (info.ElapsedTime >= info.TotalTime) {
						info.IsChanging = false;
						info.ElapsedTime = 0f;
					}
				});
			});
		}

		public void OnRenderUpdate(float deltaTime) {
			var query = _world.CreateQueryBuilder()
							.WithAll<SmoothChangeStatComp>()
							.Build();
			var curveRes = _world.GetResource<ChangeCurveResource>();
			query.ForEach(e => {
				var changInfos = e.GetComponent<SmoothChangeStatComp>().SmoothChangeInfos;
				changInfos?.ForEach(info => {
					if (info.IsLogicTime) return;
					if (!info.IsChanging) return;
					info.ElapsedTime += deltaTime;
					var progress = Mathf.Clamp01(info.ElapsedTime / info.TotalTime);
					var percent = curveRes.PresetCurves[info.ChangeCurveType](progress);
					var newValue = info.StartValue + (info.TargetValue - info.StartValue) * percent;
					switch (info.ChangeTargetType) {
						case ChangeTargetType.Transform_Position:
							e.GetComponent<TransformComponent>().LocalPosition = newValue.Vector3Value;
							break;
						case ChangeTargetType.Transform_Rotation:
							e.GetComponent<TransformComponent>().LocalRotation = Quaternion.Euler(newValue.Vector3Value);
							break;
						case ChangeTargetType.Transform_Scale:
							e.GetComponent<TransformComponent>().LocalScale = newValue.Vector3Value;
							break;
						case ChangeTargetType.Renderer_Alpha:
							e.GetComponent<SpriteRendererComponent>().Alpha = newValue.FloatValue;
							break;
						case ChangeTargetType.RectTransform_OffsetMin:
							e.GetComponent<RectTransformComponent>().OffsetMin = newValue.Vector2Value;
							break;
						case ChangeTargetType.RectTransform_OffsetMax:
							e.GetComponent<RectTransformComponent>().OffsetMax = newValue.Vector2Value;
							break;
					}
					if (info.ElapsedTime >= info.TotalTime) {
						info.IsChanging = false;
						info.ElapsedTime = 0f;
					}
				});
			});
		}
	}
}