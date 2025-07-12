using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	/// <summary>
	/// VillAiSystem 负责处理村民的 AI 行为逻辑。
	/// </summary>
	public class VillAiSystem : ISystem {
		public int Priority => 800;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }

		public void OnDestroy() { }

		public void OnLogicUpdate(float deltaTime) {
			var query = _world.CreateQueryBuilder()
				.WithAll<VillBehaviourTreeComponent>().Build();
			query.ForEach(entity => {
				entity.GetComponent<VillBehaviourTreeComponent>().BehaviourTree.Think();
			});
		}

		public void OnRenderUpdate(float _) { }
	}
}