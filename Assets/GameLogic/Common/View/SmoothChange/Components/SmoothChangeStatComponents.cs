using GameLogic.Common.UnityComponentsBridge;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Common.View {
	[System.Serializable]
	public class SmoothPositionStatComponent : SmoothChangeStatCompBase<SimpleVector3> {
		public SmoothPositionStatComponent(ChangeInfo changeInfo) : base(changeInfo) { }
		public SmoothPositionStatComponent(float totalTime, ChangeCurveType curveType, bool useLogicTime)
			: base(totalTime, curveType, useLogicTime) { }

		public override void SetStartValueToCurValue(Entity entity) {
			var transComp = entity.GetComponent<TransformComponent>();
			StartValue = transComp.LocalPosition;
		}
		public override void ApplyChange(Entity entity) {
			var transComp = entity.GetComponent<TransformComponent>();
			if (TotalTime <= 0f) {
				transComp.LocalPosition = TargetValue;
			} else {
				transComp.LocalPosition = StartValue + (TargetValue - StartValue) * ChangeCurves.GetCurve(CurveType)(ElapsedTime / TotalTime);
			}
			transComp.Dirty = true;
		}
	}
	public class SmoothRotationStatComponent : SmoothChangeStatCompBase<SimpleQuaternion> {
		public SmoothRotationStatComponent(ChangeInfo changeInfo) : base(changeInfo) { }
		public SmoothRotationStatComponent(float totalTime, ChangeCurveType curveType, bool useLogicTime) 
			: base(totalTime, curveType, useLogicTime) { }
		public override void SetStartValueToCurValue(Entity entity) {
			var transComp = entity.GetComponent<TransformComponent>();
			StartValue = transComp.LocalRotation;
		}
		public override void ApplyChange(Entity entity) {
			var transComp = entity.GetComponent<TransformComponent>();
			if (TotalTime <= 0f) {
				transComp.LocalRotation = TargetValue;
			} else {
				transComp.LocalRotation = StartValue + (TargetValue - StartValue) * ChangeCurves.GetCurve(CurveType)(ElapsedTime / TotalTime);
			}
			transComp.Dirty = true;
		}
	}
	public class SmoothScaleStatComponent : SmoothChangeStatCompBase<SimpleVector3> {
		public SmoothScaleStatComponent(ChangeInfo changeInfo) : base(changeInfo) { }
		public SmoothScaleStatComponent(float totalTime, ChangeCurveType curveType, bool useLogicTime) 
			: base(totalTime, curveType, useLogicTime) { }
		public override void SetStartValueToCurValue(Entity entity) {
			var transComp = entity.GetComponent<TransformComponent>();
			StartValue = transComp.LocalScale;
		}
		public override void ApplyChange(Entity entity) {
			var transComp = entity.GetComponent<TransformComponent>();
			if (TotalTime <= 0f) {
				transComp.LocalScale = TargetValue;
			} else {
				transComp.LocalScale = StartValue + (TargetValue - StartValue) * ChangeCurves.GetCurve(CurveType)(ElapsedTime / TotalTime);
			}
			transComp.Dirty = true;
		}
	}
	public class SmoothAlphaStatComponent : SmoothChangeStatCompBase<float> {
		public SmoothAlphaStatComponent(ChangeInfo changeInfo) : base(changeInfo) { }
		public SmoothAlphaStatComponent(float totalTime, ChangeCurveType curveType, bool useLogicTime) 
			: base(totalTime, curveType, useLogicTime) { }
		public override void SetStartValueToCurValue(Entity entity) {
			var rendererComp = entity.GetComponent<SpriteRendererComponent>();
			StartValue = rendererComp.Color.a;
		}
		public override void ApplyChange(Entity entity) {
			var rendererComp = entity.GetComponent<SpriteRendererComponent>();
			float alpha;
			if (TotalTime <= 0f) {
				alpha = TargetValue;
			} else {
				alpha = StartValue + (TargetValue - StartValue) * ChangeCurves.GetCurve(CurveType)(ElapsedTime / TotalTime);
			}
			rendererComp.Color.ModifyAlpha(alpha);
			rendererComp.Dirty = true;
		}
	}
	public class SmoothOffsetMinStatComponent : SmoothChangeStatCompBase<SimpleVector2> {
		public SmoothOffsetMinStatComponent(ChangeInfo changeInfo) : base(changeInfo) { }
		public SmoothOffsetMinStatComponent(float totalTime, ChangeCurveType curveType, bool useLogicTime) 
			: base(totalTime, curveType, useLogicTime) { }
		public override void SetStartValueToCurValue(Entity entity) {
			var rectTransComp = entity.GetComponent<RectTransformComponent>();
			StartValue = rectTransComp.OffsetMin;
		}
		public override void ApplyChange(Entity entity) {
			var rectTransComp = entity.GetComponent<RectTransformComponent>();
			if (TotalTime <= 0f) {
				rectTransComp.OffsetMin = TargetValue;
			} else {
				rectTransComp.OffsetMin = StartValue + (TargetValue - StartValue) * ChangeCurves.GetCurve(CurveType)(ElapsedTime / TotalTime);
			}
			rectTransComp.Dirty = true;
		}
	}
	public class SmoothOffsetMaxStatComponent : SmoothChangeStatCompBase<SimpleVector2> {
		public SmoothOffsetMaxStatComponent(ChangeInfo changeInfo) : base(changeInfo) { }
		public SmoothOffsetMaxStatComponent(float totalTime, ChangeCurveType curveType, bool useLogicTime) 
			: base(totalTime, curveType, useLogicTime) { }
		public override void SetStartValueToCurValue(Entity entity) {
			var rectTransComp = entity.GetComponent<RectTransformComponent>();
			StartValue = rectTransComp.OffsetMax;
		}
		public override void ApplyChange(Entity entity) {
			var rectTransComp = entity.GetComponent<RectTransformComponent>();
			if (TotalTime <= 0f) {
				rectTransComp.OffsetMax = TargetValue;
			} else {
				rectTransComp.OffsetMax = StartValue + (TargetValue - StartValue) * ChangeCurves.GetCurve(CurveType)(ElapsedTime / TotalTime);
			}
			rectTransComp.Dirty = true;
		}
	}
	public class SmoothCameraSizeStatComponent : SmoothChangeStatCompBase<float> {
		public SmoothCameraSizeStatComponent(ChangeInfo changeInfo) : base(changeInfo) { }
		public SmoothCameraSizeStatComponent(float totalTime, ChangeCurveType curveType, bool useLogicTime) 
			: base(totalTime, curveType, useLogicTime) { }
		public override void SetStartValueToCurValue(Entity entity) {
			var cameraComp = entity.GetComponent<CameraComponent>();
			StartValue = cameraComp.FeildOfView;
		}
		public override void ApplyChange(Entity entity) {
			var cameraComp = entity.GetComponent<CameraComponent>();
			if (TotalTime <= 0f) {
				cameraComp.FeildOfView = TargetValue;
			} else {
				cameraComp.FeildOfView = StartValue + (TargetValue - StartValue) * ChangeCurves.GetCurve(CurveType)(ElapsedTime / TotalTime);
			}
			cameraComp.Dirty = true;
		}
	}
	public class SmoothAudioVolumeStatComponent : SmoothChangeStatCompBase<float> {
		public SmoothAudioVolumeStatComponent(ChangeInfo changeInfo) : base(changeInfo) { }
		public SmoothAudioVolumeStatComponent(float totalTime, ChangeCurveType curveType, bool useLogicTime) 
			: base(totalTime, curveType, useLogicTime) { }
		public override void SetStartValueToCurValue(Entity entity) {
			var audioComp = entity.GetComponent<AudioSourceComponent>();
			StartValue = audioComp.Volume;
		}
		public override void ApplyChange(Entity entity) {
			var audioComp = entity.GetComponent<AudioSourceComponent>();
			if (TotalTime <= 0f) {
				audioComp.Volume = TargetValue;
			} else {
				audioComp.Volume = StartValue + (TargetValue - StartValue) * ChangeCurves.GetCurve(CurveType)(ElapsedTime / TotalTime);
			}
			audioComp.Dirty = true;
		}
	}
}