using NSFrame;

namespace GameLogic
{
	/// <summary>
	/// 游戏从始至末的管理
	/// </summary>
	public class GameMgr : MonoSingleton<GameMgr> {

		protected void Start(){
			ToggleSingletons();
			EventSystem.Invoke((int)LogicEvt.InitAllManager);
		}

		private void ToggleSingletons() {
			/* 触发所有单例 */
			var dayNightMgr = LogicTimeMgr.Inst;
			var disasterMgr = DisasterMgr.Inst;
			var idMgr = IDMgr.Inst;
			var mapMgr = MapMgr.Inst;
			var repoMgr = RepoMgr.Inst;
			var routeMgr = RouteMgr.Inst;
			var saveMgr = SaveMgr.Inst;
			var worldMgr = WorldMgr.Inst;
		}

	}
}