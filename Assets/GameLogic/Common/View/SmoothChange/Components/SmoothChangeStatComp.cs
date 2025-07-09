using System;
using System.Collections.Generic;
using NsEcsFrame.Core;
using NSFrame;
using UnityEngine;

namespace GameLogic.Common.View {

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
		public SmoothValue(object value) {
			if (value is float f) {
				ValueType = SmoothValueType.Float;
				FloatValue = f;
				Vector2Value = default;
				Vector3Value = default;
			} else if (value is Vector2 v2) {
				ValueType = SmoothValueType.Vector2;
				FloatValue = default;
				Vector2Value = v2;
				Vector3Value = default;
			} else if (value is Vector3 v3) {
				ValueType = SmoothValueType.Vector3;
				FloatValue = default;
				Vector2Value = default;
				Vector3Value = v3;
			} else {
				throw new ArgumentException("Unsupported SmoothValue type");
			}
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
		public List<SmoothChangeInfo> SmoothChangeInfos = new();

		public void AddNewChange(
			bool isLogicTime,
			ChangeTargetType targetType,
			ChangeCurveType curveType,
			float totalTime,
			object targetValue
		) {
			var info = PoolSystem.PopObj<SmoothChangeInfo>();
			info.IsLogicTime = isLogicTime;
			info.ChangeTargetType = targetType;
			info.ChangeCurveType = curveType;
			info.TotalTime = totalTime;
			info.ElapsedTime = 0f;
			info.Started = false;
			info.TargetValue = new(targetValue);
			SmoothChangeInfos.Add(info);
		}

		/// <summary>
		/// 移除指定类型的平滑变化信息
		/// </summary>
		public void RemoveChangeOfType(ChangeTargetType targetType) {
			for (int i = SmoothChangeInfos.Count - 1; i >= 0; i--) {
				if (SmoothChangeInfos[i].ChangeTargetType == targetType) {
					var info = SmoothChangeInfos[i];
					SmoothChangeInfos.RemoveAt(i);
					PoolSystem.PushObj(info);
				}
			}
		}

		/// <summary>
		/// 清除已完成的平滑变化信息
		/// </summary>
		public void ClearOveredInfos() {
			for (int i = SmoothChangeInfos.Count - 1; i >= 0; i--) {
				var info = SmoothChangeInfos[i];
				if (info.ElapsedTime >= info.TotalTime) {
					SmoothChangeInfos.RemoveAt(i);
					PoolSystem.PushObj(info);
				}
			}
		}
	}

	/// <summary>
	/// 单个平滑变化的信息
	/// </summary>
	[Serializable]
	public class SmoothChangeInfo : IPooledObject {
		[RuntimeInitializeOnLoadMethod]
		static void InitPool() {
			PoolSystem.InitObjectPool<SmoothChangeInfo>();
		}
		public bool IsLogicTime = true;
		public ChangeTargetType ChangeTargetType = ChangeTargetType.Transform_Position;
		public ChangeCurveType ChangeCurveType = ChangeCurveType.Linear;
		public float TotalTime = 1f;
		public float ElapsedTime = 0f;

		public bool Started = false;

		public SmoothValue StartValue;
		public SmoothValue TargetValue;

		public void CleanBeforePush() { }
		public void InitAfterPop() { }
	}
}