using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Model.Mgr;
using NSFrame;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// 当前的体力值状态
	/// </summary>
	public enum VitState {
		/// <summary>
		/// 正常工作
		/// <para>行为描述:正常走路的时候Config中的速率消耗vit，在建筑中工作的时候按照所在建筑的配置消耗vit</para>
		/// </summary>
		Normal,
		/// <summary>
		/// 低体力状态
		/// <para>触发条件:Normal状态下，vit低于*Low阈值*触发</para>
		/// <para>行为描述:回家吃饭，进入家开始恢复vit时进入Recovering状态</para>
		/// </summary>
		Low,
		/// <summary>
		/// vit正在回复中
		/// <para>触发条件:在vit低于*Low阈值*时，村民会去吃饭，开始吃饭任务后进入此状态</para>
		/// <para>行为描述:按照Config消耗食物，回复vit。在回复到*Recovery阈值*后，或者没有食物后，村民会返回工作（回到Normal状态）</para>
		/// </summary>
		Recovering,
		/// <summary>
		/// 饥饿状态
		/// <para>触发条件:vit低于*Hungry阈值*</para>
		/// <para>行为描述:所有效率降低（同时改变消耗资源的buff和产出资源的buff）</para>
		/// </summary>
		Hungry,
		/// <summary>
		/// 快要死了的状态
		/// <para>触发条件:晚上吃完饭后vit低于*Hungry阈值*</para>
		/// <para>行为描述:不工作，进行不消耗体力的随机游走，如果在第二天结束时进食后vit仍然无法回复到*Low阈值*，则离开村庄</para>
		/// </summary>
		Dying
	}
	/// <summary>
	/// 村民体力值管理器
	/// </summary>
	public class VitHelper : ISaveable<VitHelperSave>, IVillVit {
		private readonly LogicImpler _impler;
		private VitConfig VitConfig => ConfigMgr.Config.VitConfig;
		public VitHelper(LogicImpler impler) {
			_impler = impler;
			_impler.TickUpdate += TickUpdate;
			MaxVit = VitConfig.MaxVit;
		}
		public void LogicDestroy() { }


		public List<VitBuffAdder> ConsBuffAdders { get; private set; } = new();
		public List<VitBuffAdder> RecoverBuffAdders { get; private set; } = new();

		private void TickUpdate() {
			// todo: 
		}

		#region IVillVit
		public float MaxVit { get; }
		public float CurVit { get; private set; }
		public float ConsBuff { get; private set; }
		public float RecoverBuff { get; private set; }
		public VitState CurState { get; private set; }

		public float VitPercentage => CurVit / MaxVit;

		public event Action<float> OnVitChanged;
		public event Action OnLowVit;
		public event Action OnVitRecovered;

		public void AddConsBuff_Eternal(float buff) => ConsBuff += buff;
		public void AddConsBuff_Temp(float buff, int ticks) {
			ConsBuff += buff;
			ConsBuffAdders.Add(new(this, VitBuffType.Cons, -buff, ticks));
		}

		public void AddRecoverBuff_Eternal(float buff) => RecoverBuff += buff;
		public void AddRecoverBuff_Temp(float buff, int ticks) {
			RecoverBuff += buff;
			RecoverBuffAdders.Add(new(this, VitBuffType.Recover, buff, ticks));
		}

		public void AddVit(float vit) {
			if (CurVit >= MaxVit) return;
			CurVit = Mathf.Min(CurVit + vit * (1 + RecoverBuff), MaxVit);
			OnVitChanged?.Invoke(CurVit);
		}
		public bool TryConsVit(float vit) {
			var realCost = vit * (1 - ConsBuff);
			if (CurVit >= realCost) {
				CurVit -= realCost;
				OnVitChanged?.Invoke(CurVit);
				return true;
			}
			return false;
		}
		#endregion

		#region ISaveable
		public VitHelperSave GetSave() {
			return new() {
				CurVit = CurVit,
				CurState = CurState,
				ConsBuffAdders = ConsBuffAdders.Select((x) => x.GetSave()).ToList(),
				RecoverBuffAdders = RecoverBuffAdders.Select((x) => x.GetSave()).ToList(),
			};
		}
		public void InitFromSave(VitHelperSave save) {
			CurVit = save.CurVit;
			CurState = save.CurState;
			ConsBuffAdders.ForEach(adder => adder.Stop());
			RecoverBuffAdders.ForEach(adder => adder.Stop());

			ConsBuffAdders = save.ConsBuffAdders
							.Select(x => new VitBuffAdder(this, x.BuffType, x.Buff, x.RestTicks))
							.ToList();
			RecoverBuffAdders = save.RecoverBuffAdders
								.Select(x => new VitBuffAdder(this, x.BuffType, x.Buff, x.RestTicks))
								.ToList();
		}
		#endregion
	}
}