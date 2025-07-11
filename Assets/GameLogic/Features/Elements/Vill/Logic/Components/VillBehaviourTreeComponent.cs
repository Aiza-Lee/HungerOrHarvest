using GameLogic.Common.Logic;
using GameLogic.Common.View;
using GameLogic.Features.SaveLoadData;
using NsEcsFrame.Core;
using NSFrame.BehaviourTree;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// VillBehaviourTreeComponent 用于存储村民的行为树对象
	/// </summary>
	public class VillBehaviourTreeComponent : IComponent, ISaveIgnoreComponent {
		public BehaviourTree<VillAiBlackboard> BehaviourTree { get; set; }
		public VillBehaviourTreeComponent(Entity entity) {
			BehaviourTree = BehaviourTreeFactory.CreateVillBehaviourTree(new(entity));
		}
	}

	/// <summary>
	/// VillAiBlackboard 用于存储村民行为树的黑板数据
	/// </summary>
	public class VillAiBlackboard : IBlackboard {
		public Entity Entity { get; }
		public IWorld World => Entity.World;

		private GidComponent _gidComp;
		public GidComponent GidComp => _gidComp ??= Entity.GetComponent<GidComponent>();

		private VillIdentityComponent _identityComp;
		public VillIdentityComponent IdentityComp => _identityComp ??= Entity.GetComponent<VillIdentityComponent>(); 

		private VillConfigBase _config;
		public VillConfigBase Config => _config = _config != null ? _config : World.GetResource<VillConfigResource>().GetConfig(IdentityComp.Type);

		private VitConfig _vitConfig;
		public VitConfig VitConfig => _vitConfig = _vitConfig != null ? _vitConfig : Config.VitConfig;

		private VillVitalityComponent _vitalityComp;
		public VillVitalityComponent VitalityComp => _vitalityComp ??= Entity.GetComponent<VillVitalityComponent>();

		private RoutePlanComponent _routePlanComp;
		public RoutePlanComponent RoutePlanComp => _routePlanComp ??= Entity.GetComponent<RoutePlanComponent>();

		private VillMoveComponent _moveComp;
		public VillMoveComponent MoveComp => _moveComp ??= Entity.GetComponent<VillMoveComponent>();

		private SmoothedCoordComponent _smoothedCoordComp;
		public SmoothedCoordComponent SmoothedCoordComp => _smoothedCoordComp ??= Entity.GetComponent<SmoothedCoordComponent>();

		public float VitPercent => VitalityComp.Vit / VitConfig.MaxVit;
		public bool IsHungry => VitPercent < VitConfig.LowVitThreshold;

		public VillAiBlackboard(Entity entity) {
			Entity = entity;
		}
		public void Clear() { }
	}
}