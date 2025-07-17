using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameLogic.Features.SkyColorChange {
	[System.Serializable]
	public class SkyColorResource : IResource {
		[Header("天空及其在一整天时间中颜色透明度变化的曲线")]
		public List<SerializablePair<SpriteRenderer, AnimationCurve>> SkySprites = new();

		[Header("环境光照明")]
		public Light2D EnvironmentLight;
		public Gradient ColorGradient;
		public float LightIntensity;
		public AnimationCurve LightIntensityFactorCurve;

	}
}