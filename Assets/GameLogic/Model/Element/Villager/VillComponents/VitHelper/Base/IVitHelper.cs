using System;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill
{
	public interface IVitHelper : ILogicDestroy {

		float MaxVit { get; }
		float CurVit { get; }
		/// <summary>
		/// 当前体力百分比
		/// </summary>
		float CurVitProportion => CurVit / MaxVit;
		/// <summary>
		/// 是否处于饥饿状态
		/// </summary>
		bool IsHungry => CurVitProportion < ConfigMgr.Config.VitConfig.LowVitThreshold;

		/// <summary>
		/// 减少vit消耗的buff
		/// </summary>
		float ConsBuff { get; }
		/// <summary>
		/// 增加vit恢复的buff
		/// </summary>
		float RecoverBuff { get; }
		/// <summary>
		/// 增加体力，包含buff的计算在内
		/// </summary>
		void AddVit(float vit);
		/// <summary>
		/// 消耗体力，包含buff的计算在内
		/// </summary>
		/// <returns>是否成功消耗</returns>
		bool TryConsVit(float vit);
		/// <summary>
		/// 增加体力消耗*永久*buff
		/// </summary>
		/// <param name="buff"></param>
		void AddConsBuff_Eternal(float buff);
		/// <summary>
		/// 增加体力恢复*永久*buff
		/// </summary>
		void AddRecoverBuff_Eternal(float buff);
		/// <summary>
		/// 增加体力消耗*临时*buff
		/// </summary>
		void AddConsBuff_Temp(float buff, int ticks);
		/// <summary>
		/// 增加体力恢复*临时*buff
		/// </summary>
		void AddRecoverBuff_Temp(float buff, int ticks);

		event Action<float> OnVitChanged;
		event Action OnLowVit;
		event Action OnVitRecovered;
		
	}
}