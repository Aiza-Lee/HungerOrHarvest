using GameLogic.Utilities;
using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	public enum VitBuffType { Cons, Recover }
	public class VitBuffAdder : ISaveable<VitBuffAdderSave> {

		private readonly VitHelper _vitHelper;
		private DelayTrigger _trigger;
		private float _buff;
		// 记录buff类型，用于处理buff的移除
		private VitBuffType _buffType;


		public VitBuffAdder() {}
		public VitBuffAdder(VitHelper vithelper, VitBuffType buffType, float buff, int ticks) {
			_vitHelper = vithelper;
			_buffType = buffType;
			_buff = buff;
			_trigger = DelayTrigger.Run(() => Callback(), ticks);
		}
		public void Stop() {
			_trigger?.Stop();
			_trigger = null;
		}

		private void Callback() {
			if (_buffType == VitBuffType.Cons) {
				_vitHelper.AddConsBuff_Eternal(_buff);
				_vitHelper.ConsBuffAdders.Remove(this);
			} else if (_buffType == VitBuffType.Recover) {
				_vitHelper.AddRecoverBuff_Eternal(_buff);
				_vitHelper.RecoverBuffAdders.Remove(this);
			}
			// trigger在执行后会自己把自己放到对象池中，这里要清空引用
			_trigger = null;
		}

		public VitBuffAdderSave GetSave() {
			return new() {
				Buff = _buff,
				BuffType = _buffType,
				RestTicks = _trigger.RestTicks
			};
		}
		public void InitFromSave(VitBuffAdderSave _) {
			Debug.LogWarning("Aiza:VitBuffAdder.InitFromSave() should not be called.");
		}
	}
}