using System;
using System.Collections.Generic;
using System.Linq;
using GameLogic.Model.Mgr;
using GameLogic.Utilities;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// 村民体力值管理器
	/// </summary>
	public class VitHelper : ISaveable<VitHelperSave>, IVitHelper {
		private readonly LogicImpler _impler;
		private VitConfig VitConfig => ConfigMgr.Config.VitConfig;
		public VitHelper(LogicImpler impler) {
			_impler = impler;
			MaxVit = VitConfig.MaxVit;
		}
		public void LogicDestroy() { }


		public List<BuffAdder> ConsBuffAdders { get; private set; } = new();
		public List<BuffAdder> RecoverBuffAdders { get; private set; } = new();

		#region IVillVit
		public float MaxVit { get; }
		public float CurVit { get; private set; }
		public float ConsBuff { get; private set; }
		public float RecoverBuff { get; private set; }

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
				ConsBuffAdders = ConsBuffAdders.Select((x) => x.GetSave()).ToList(),
				RecoverBuffAdders = RecoverBuffAdders.Select((x) => x.GetSave()).ToList(),
			};
		}
		public void InitFromSave(VitHelperSave save) {
			CurVit = save.CurVit;
			ConsBuffAdders.ForEach(adder => adder.Stop());
			RecoverBuffAdders.ForEach(adder => adder.Stop());

			ConsBuffAdders = save.ConsBuffAdders
							.Select(x => new BuffAdder(this, x.BuffType, x.Buff, x.RestTicks))
							.ToList();
			RecoverBuffAdders = save.RecoverBuffAdders
								.Select(x => new BuffAdder(this, x.BuffType, x.Buff, x.RestTicks))
								.ToList();
		}
		#endregion
	}
}