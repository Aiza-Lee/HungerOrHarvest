using GameLogic.Features.TickCounter;
using NsEcsFrame.Core;
using NSFrame;

namespace GameLogic.Features.SaveLoadData {
	/// <summary>
	/// DayEndAutoSaveSystem 负责在每个游戏日结束时自动保存游戏数据。
	/// </summary>
	public class DayEndAutoSaveSystem : ISystem {
		public int Priority => 100;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var tickCounter = _world.GetResource<TickCounterResource>();
			if (!tickCounter.IsDayLastTick) return;
			var saveInfo = _world.GetResource<SaveInfoResource>().SaveInfo;
			var gameSaveData = new GameSaveData(_world);
			SaveSystem.SaveObject(saveInfo, gameSaveData);
		}
		public void OnRenderUpdate(float _) { }
	} 
}