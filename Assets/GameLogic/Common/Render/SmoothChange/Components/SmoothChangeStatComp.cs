using System;
using System.Collections.Generic;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Common.Render {

	/// <summary>
	/// 平滑变化的目标类型
	/// </summary>
	public enum ChangeTargetType {
		Transform_Position,
		Transform_Rotation,
		Transform_Scale,
		Renderer_Alpha,
		RectTransform_OffsetMin,
		RectTransform_OffsetMax,
	}
	public enum SmoothValueType { Float, Vector2, Vector3 }

	[Serializable]
	public struct SmoothValue {
		public SmoothValueType ValueType;
		public float FloatValue;
		public Vector2 Vector2Value;
		public Vector3 Vector3Value;

		public SmoothValue(float value) {
			ValueType = SmoothValueType.Float;
			FloatValue = value;
			Vector2Value = default;
			Vector3Value = default;
		}
		public SmoothValue(Vector2 value) {
			ValueType = SmoothValueType.Vector2;
			FloatValue = default;
			Vector2Value = value;
			Vector3Value = default;
		}
		public SmoothValue(Vector3 value) {
			ValueType = SmoothValueType.Vector3;
			FloatValue = default;
			Vector2Value = default;
			Vector3Value = value;
		}

		public readonly object GetValue() {
			return ValueType switch {
				SmoothValueType.Float => FloatValue,
				SmoothValueType.Vector2 => Vector2Value,
				SmoothValueType.Vector3 => Vector3Value,
				_ => null
			};
		}

		public static SmoothValue operator +(SmoothValue a, SmoothValue b) {
			if (a.ValueType != b.ValueType) throw new InvalidOperationException("SmoothValue type mismatch");
			return a.ValueType switch {
				SmoothValueType.Float => new SmoothValue(a.FloatValue + b.FloatValue),
				SmoothValueType.Vector2 => new SmoothValue(a.Vector2Value + b.Vector2Value),
				SmoothValueType.Vector3 => new SmoothValue(a.Vector3Value + b.Vector3Value),
				_ => throw new NotSupportedException(),
			};
		}

		public static SmoothValue operator -(SmoothValue a, SmoothValue b) {
			if (a.ValueType != b.ValueType) throw new InvalidOperationException("SmoothValue type mismatch");
			return a.ValueType switch {
				SmoothValueType.Float => new SmoothValue(a.FloatValue - b.FloatValue),
				SmoothValueType.Vector2 => new SmoothValue(a.Vector2Value - b.Vector2Value),
				SmoothValueType.Vector3 => new SmoothValue(a.Vector3Value - b.Vector3Value),
				_ => throw new NotSupportedException(),
			};
		}

		public static SmoothValue operator *(SmoothValue a, float scalar) {
			return a.ValueType switch {
				SmoothValueType.Float => new SmoothValue(a.FloatValue * scalar),
				SmoothValueType.Vector2 => new SmoothValue(a.Vector2Value * scalar),
				SmoothValueType.Vector3 => new SmoothValue(a.Vector3Value * scalar),
				_ => throw new NotSupportedException(),
			};
		}

		public static SmoothValue operator *(float scalar, SmoothValue a) => a * scalar;
	}
	/// <summary>
	/// 平滑变化的状态
	/// </summary>
	[Serializable]
	public class SmoothChangeStatComponent : IComponent {
		public List<SmoothChangeInfo> SmoothChangeInfos;

		public void CopyFrom(IComponent other) {
			if (other is SmoothChangeStatComponent otherComp) {
				SmoothChangeInfos = new List<SmoothChangeInfo>(otherComp.SmoothChangeInfos.Count);
				foreach (var info in otherComp.SmoothChangeInfos) {
					SmoothChangeInfos.Add(new SmoothChangeInfo(info));
				}
			} else {
				throw new ArgumentException("Cannot copy from non-SmoothChangeStatComp component");
			}
		}
	}

	/// <summary>
	/// 单个平滑变化的信息
	/// </summary>
	[Serializable]
	public class SmoothChangeInfo {
		public bool IsLogicTime;
		public ChangeTargetType ChangeTargetType;
		public ChangeCurveType ChangeCurveType;
		public float TotalTime;
		public float ElapsedTime;
		public bool IsChanging;

		public SmoothValue StartValue;
		public SmoothValue TargetValue;

		public SmoothChangeInfo() {
			IsLogicTime = true;
			ChangeTargetType = ChangeTargetType.Transform_Position;
			ChangeCurveType = ChangeCurveType.Linear;
			TotalTime = 1f;
			ElapsedTime = 0f;
			IsChanging = false;
			StartValue = new SmoothValue(Vector3.zero);
			TargetValue = new SmoothValue(Vector3.zero);
		}
		public SmoothChangeInfo(SmoothChangeInfo other) {
			IsLogicTime = other.IsLogicTime;
			ChangeTargetType = other.ChangeTargetType;
			ChangeCurveType = other.ChangeCurveType;
			TotalTime = other.TotalTime;
			ElapsedTime = other.ElapsedTime;
			IsChanging = other.IsChanging;
			StartValue = other.StartValue;
			TargetValue = other.TargetValue;
		}
	}
}