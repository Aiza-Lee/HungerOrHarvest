using UnityEngine;

namespace OldGameLogic.View
{
	[RequireComponent(typeof(SpriteRenderer))]
	public class SmoothFade : SmoothChangeBase<float> {
		public SpriteRenderer Renderer { get; private set; }
		private Material Material => Renderer.material;
		const string VALUE_NAME = "_Alpha";

		private void Awake() {
			Renderer = GetComponent<SpriteRenderer>();
		}
		public override float GetCurVal() { 
			return Material.GetFloat(VALUE_NAME);
		}

		protected override void SetCurVal_Derived(float val) { 
			Material.SetFloat(VALUE_NAME, val);
		}

		protected override float Add(float lhv, float rhv) => lhv + rhv;
		protected override float Mul(float lhv, float rhv) => lhv * rhv;
		protected override float Sub(float lhv, float rhv) => lhv - rhv;

	}
}