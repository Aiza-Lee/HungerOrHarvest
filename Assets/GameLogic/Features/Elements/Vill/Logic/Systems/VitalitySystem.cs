using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.Events;
using GameLogic.Features.Vill;
using GameLogic.World;
using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.Elements.Vill {
	/// <summary>
	/// 负责处理体力值请求, 会过滤掉无法满足请求的体力值消耗
	/// 例如体力值不足以满足请求的消耗时, 会移除该请求
	/// </summary>
	public class VitalitySystem : ISystem {
		public int Priority => 1500;
		public bool Enabled { get; set; }

		private IWorld _world;
		private EntityQueryBuilder _costQuery, _gainQuery;
		private readonly Dictionary<VillType, VitConfig> _vitConfigs = new();

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
			_costQuery = world.CreateQueryBuilder().WithAll<VillCostVitRequestComponent>();
			_gainQuery = world.CreateQueryBuilder().WithAll<VillGainVitRequestComponent>();
			var configRes = GameWorldMono.MainWorld.GetResource<VillConfigResource>();
			foreach (VillType type in System.Enum.GetValues(typeof(VillType))) {
				try {
					_vitConfigs[type] = configRes.GetConfig(type).VitConfig;
				} catch { }
			}
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			_costQuery.Build().ForEach(entity => {
				var request = entity.GetComponent<VillCostVitRequestComponent>();
				var vitComp = entity.GetComponent<VillVitalityComponent>();
				if (vitComp.Vit >= request.VitCost) {
					vitComp.Vit -= request.VitCost;
				} else {
					entity.RemoveComponent<VillCostVitRequestComponent>();
				}
			});
			_gainQuery.Build().ForEach(entity => {
				var request = entity.GetComponent<VillGainVitRequestComponent>();
				var vitComp = entity.GetComponent<VillVitalityComponent>();
				var vitConfig = _vitConfigs[entity.GetComponent<VillIdentityComponent>().Type];
				vitComp.Vit = Mathf.Min(vitComp.Vit + request.VitGain, vitConfig.MaxVit);
			});
		}
		public void OnRenderUpdate(float _) { }
	} 
}