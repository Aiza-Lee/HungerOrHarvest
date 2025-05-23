using System.Collections.Generic;
using GameLogic.Model.Mgr;
using NSFrame;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameLogic.View 
{
	public class LightMgr : MonoSingleton<LightMgr> {
		[SerializeField] private List<Light2D> _environmentLight;
		
		private Gradient _dayColor;
		private Gradient _nightColor;

		private void Start() {
			_dayColor = ViewConstMgr.GetConfig.EnvironmentLightColor_Day;
			_nightColor = ViewConstMgr.GetConfig.EnvironmentLightColor_Night;
		}

		private void Update() {
			if (LogicTimeMgr.Inst.IsDay) {
				foreach (var light in _environmentLight) {
					light.color = _dayColor.Evaluate(LogicTimeMgr.Inst.DayProcess);
				}
			} else {
				foreach (var light in _environmentLight) {
					light.color = _nightColor.Evaluate(LogicTimeMgr.Inst.NightProcess);
				}
			}
		}

	}
}