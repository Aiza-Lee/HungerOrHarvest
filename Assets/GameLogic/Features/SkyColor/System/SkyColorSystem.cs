using GameLogic.Features.TickCounter;
using NsEcsFrame.Core;

namespace GameLogic.Features.SkyColorChange {
	/// <summary>
	/// SkyColorSystem 负责天空颜色的变化
	/// </summary>
	public class SkyColorSystem : ISystem {
		public int Priority => 100;
		public bool Enabled { get; set; }

		private IWorld _world;
		private SkyColorResource _skyColorResource;
		private SkyColorResource Resource => _skyColorResource ??= _world.GetResource<SkyColorResource>();

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) { }
		public void OnRenderUpdate(float _) {
			var process = TickCounterQueryAPI.GetWholeProcess();
			foreach (var pair in Resource.SkySprites) {
				var spriteRenderer = pair.Key;
				var curve = pair.Value;

				if (spriteRenderer != null && curve != null) {
					var color = spriteRenderer.color;
					color.a = curve.Evaluate(process);
					spriteRenderer.color = color;
				}
			}

			var light = Resource.EnvironmentLight;
			light.intensity = Resource.LightIntensity * Resource.LightIntensityFactorCurve.Evaluate(process);
			light.color = Resource.ColorGradient.Evaluate(process);
		}
	} 
}