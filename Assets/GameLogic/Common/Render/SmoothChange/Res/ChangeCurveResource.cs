using System;
using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Common.Render {
	public enum ChangeCurveType {
		/// <summary>直接跳转到目标值</summary>
		Directive,
		/// <summary>线性，无缓动，匀速</summary>
		Linear,
		/// <summary>正弦缓入，开始慢，后面快</summary>
		SineIn,
		/// <summary>正弦缓出，开始快，后面慢</summary>
		SineOut,
		/// <summary>正弦缓入缓出，两头慢中间快</summary>
		SineInOut,
		/// <summary>二次方缓入，开始慢，后面快</summary>
		QuadIn,
		/// <summary>二次方缓出，开始快，后面慢</summary>
		QuadOut,
		/// <summary>二次方缓入缓出，两头慢中间快</summary>
		QuadInOut,
		/// <summary>三次方缓入，开始更慢，后面更快</summary>
		CubicIn,
		/// <summary>三次方缓出，开始更快，后面更慢</summary>
		CubicOut,
		/// <summary>三次方缓入缓出，两头更慢中间更快</summary>
		CubicInOut,
		/// <summary>四次方缓入，极慢起步，后面极快</summary>
		QuartIn,
		/// <summary>四次方缓出，极快起步，后面极慢</summary>
		QuartOut,
		/// <summary>四次方缓入缓出，两头极慢中间极快</summary>
		QuartInOut,
		/// <summary>五次方缓入，超慢起步，后面超快</summary>
		QuintIn,
		/// <summary>五次方缓出，超快起步，后面超慢</summary>
		QuintOut,
		/// <summary>五次方缓入缓出，两头超慢中间超快</summary>
		QuintInOut,
		/// <summary>指数缓入，极慢起步，后面极快（更极端）</summary>
		ExpoIn,
		/// <summary>指数缓出，极快起步，后面极慢（更极端）</summary>
		ExpoOut,
		/// <summary>指数缓入缓出，两头极慢中间极快（更极端）</summary>
		ExpoInOut,
		/// <summary>圆形缓入，开始慢，后面快（圆弧效果）</summary>
		CircIn,
		/// <summary>圆形缓出，开始快，后面慢（圆弧效果）</summary>
		CircOut,
		/// <summary>圆形缓入缓出，两头慢中间快（圆弧效果）</summary>
		CircInOut,
		/// <summary>回弹缓入，先反向回弹再加速前进</summary>
		BackIn,
		/// <summary>回弹缓出，先超出目标再回弹收敛</summary>
		BackOut,
		/// <summary>回弹缓入缓出，两头回弹</summary>
		BackInOut,
		/// <summary>弹性缓入，先大幅反向弹跳再加速前进</summary>
		ElasticIn,
		/// <summary>弹性缓出，先超出目标大幅弹跳再收敛</summary>
		ElasticOut,
		/// <summary>弹性缓入缓出，两头弹跳</summary>
		ElasticInOut,
		/// <summary>弹跳缓入，先多次小幅弹跳再到达目标</summary>
		BounceIn,
		/// <summary>弹跳缓出，先到达目标再多次小幅弹跳</summary>
		BounceOut,
		/// <summary>弹跳缓入缓出，两头弹跳</summary>
		BounceInOut
	}
	public class ChangeCurveResource : IResource {
		public Dictionary<ChangeCurveType, Func<float, float>> PresetCurves;

		public ChangeCurveResource() {
			PresetCurves = new Dictionary<ChangeCurveType, Func<float, float>> {
				{ ChangeCurveType.Directive,   t => 1f },

				{ ChangeCurveType.Linear,      t => t },
				{ ChangeCurveType.SineIn,      t => 1f - (float)Math.Cos((t * Math.PI) / 2f) },
				{ ChangeCurveType.SineOut,     t => (float)Math.Sin((t * Math.PI) / 2f) },
				{ ChangeCurveType.SineInOut,   t => -0.5f * ((float)Math.Cos(Math.PI * t) - 1f) },

				{ ChangeCurveType.QuadIn,      t => t * t },
				{ ChangeCurveType.QuadOut,     t => t * (2f - t) },
				{ ChangeCurveType.QuadInOut,   t => t < 0.5f ? 2f * t * t : -1f + (4f - 2f * t) * t },

				{ ChangeCurveType.CubicIn,     t => t * t * t },
				{ ChangeCurveType.CubicOut,    t => 1f + (--t) * t * t },
				{ ChangeCurveType.CubicInOut,  t => t < 0.5f ? 4f * t * t * t : (t - 1f) * (2f * t - 2f) * (2f * t - 2f) + 1f },

				{ ChangeCurveType.QuartIn,     t => t * t * t * t },
				{ ChangeCurveType.QuartOut,    t => 1f - (--t) * t * t * t },
				{ ChangeCurveType.QuartInOut,  t => t < 0.5f ? 8f * t * t * t * t : 1f - 8f * (--t) * t * t * t },

				{ ChangeCurveType.QuintIn,     t => t * t * t * t * t },
				{ ChangeCurveType.QuintOut,    t => 1f + (--t) * t * t * t * t },
				{ ChangeCurveType.QuintInOut,  t => t < 0.5f ? 16f * t * t * t * t * t : 1f + 16f * (--t) * t * t * t * t },

				{ ChangeCurveType.ExpoIn,      t => (float)Math.Pow(2f, 10f * (t - 1f)) },
				{ ChangeCurveType.ExpoOut,     t => 1f - (float)Math.Pow(2f, -10f * t) },
				{ ChangeCurveType.ExpoInOut,   t => t < 0.5f ? 0.5f * (float)Math.Pow(2f, 20f * t - 10f) : 1f - 0.5f * (float)Math.Pow(2f, -20f * t + 10f) },

				{ ChangeCurveType.CircIn,      t => 1f - (float)Math.Sqrt(1f - t * t) },
				{ ChangeCurveType.CircOut,     t => (float)Math.Sqrt(1f - (--t) * t) },
				{ ChangeCurveType.CircInOut,   t => t < 0.5f ? 0.5f * (1f - (float)Math.Sqrt(1f - 4f * t * t)) : 0.5f * ((float)Math.Sqrt(1f - 4f * (--t) * t) + 1f) },

				{ ChangeCurveType.BackIn,      t => t * t * t - t * (float)Math.Sin(t * Math.PI) },
				{ ChangeCurveType.BackOut,     t => 1f + (--t) * t * t + t * (float)Math.Sin(t * Math.PI) },
				{ ChangeCurveType.BackInOut,   t => t < 0.5f ? 2f * t * t * t - t * (float)Math.Sin(2f * t * Math.PI) : 1f + 2f * (--t) * t * t + t * (float)Math.Sin(2f * t * Math.PI) },

				{ ChangeCurveType.ElasticIn,   t => (float)(-Math.Pow(2, 10 * (t - 1)) * Math.Sin((t - 1.075) * (2 * Math.PI) / 0.3)) },
				{ ChangeCurveType.ElasticOut,  t => (float)(Math.Pow(2, -10 * t) * Math.Sin((t - 0.075) * (2 * Math.PI) / 0.3) + 1) },
				{ ChangeCurveType.ElasticInOut, t => t < 0.5f ? (float)(-0.5 * Math.Pow(2, 20 * t - 10) * Math.Sin((20 * t - 11.125) * (2 * Math.PI) / 0.45)) : (float)(0.5 * Math.Pow(2, -20 * t + 10) * Math.Sin((20 * t - 11.125) * (2 * Math.PI) / 0.45) + 1) },

				{ ChangeCurveType.BounceIn,    t => 1f - BounceOut(1f - t) },
				{ ChangeCurveType.BounceOut,   t => BounceOut(t) },
				{ ChangeCurveType.BounceInOut, t => t < 0.5f ? 0.5f * (1f - BounceOut(1f - 2f * t)) : 0.5f * BounceOut(2f * t - 1f) + 0.5f }
			};
		}

		private static float BounceOut(float t) {
			if (t < (1f / 2.75f)) {
				return 7.5625f * t * t;
			} else if (t < (2f / 2.75f)) {
				t -= 1.5f / 2.75f;
				return 7.5625f * t * t + 0.75f;
			} else if (t < (2.5f / 2.75f)) {
				t -= 2.25f / 2.75f;
				return 7.5625f * t * t + 0.9375f;
			} else {
				t -= 2.625f / 2.75f;
				return 7.5625f * t * t + 0.984375f;
			}
		}

		public void CopyFrom(IResource other) {
			if (other is ChangeCurveResource otherRes) {
				PresetCurves = new Dictionary<ChangeCurveType, Func<float, float>>(otherRes.PresetCurves);
			} else {
				throw new ArgumentException($"Cannot copy from {other.GetType()} to {GetType()}");
			}
		}
	}
}