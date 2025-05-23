using NSFrame;
using UnityEngine;

namespace GameLogic.Model.Mgr
{
	/// <summary>
	/// 常量管理器，负责管理正常游戏逻辑运行需要的常量，以及游戏数值的配置
	/// </summary>
	public sealed class ConfigMgr : MonoSingleton<ConfigMgr> {

		[Header("挂载")][SerializeField] private ModelConfig _config;
		public static ModelConfig Config => Inst._config;


		protected override void Awake() {
			base.Awake();
			_config.SetConfig();
		}
	}
}