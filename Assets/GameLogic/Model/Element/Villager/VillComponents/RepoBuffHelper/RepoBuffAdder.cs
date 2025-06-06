using System;
using GameLogic.Utilities;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {

	public enum RepoBuffType {
		Prod,
		Cons
	}

	public class RepoBuffAdder : ISaveable<RepoBuffAdderSave> {


		public RepoBuffAdder(RTList<float> buffs, int ticks, RepoBuffType repoBuffType, Action<RepoBuffAdder> onTimeUp) {
			RepoBuffType = repoBuffType;
			_onTimeUp = onTimeUp;
			_buffs = buffs;
			_delayTrigger = DelayTrigger.Run(() => Callback(), ticks);
		}

		public RepoBuffType RepoBuffType { get; private set; }
		private readonly RTList<float> _buffs;
		private DelayTrigger _delayTrigger;
		private readonly Action<RepoBuffAdder> _onTimeUp;

		private void Callback() {
			_onTimeUp?.Invoke(this);
			_delayTrigger = null;
		}

		public void Stop() {
			_delayTrigger?.Stop();
			_delayTrigger = null;
		}

		#region ISaveable
		public RepoBuffAdderSave GetSave() {
			return new() {
				RepoBuffType = RepoBuffType,
				Buffs = _buffs.GetSave(),
				Ticks = _delayTrigger.RestTicks,
			};
		}
		public void InitFromSave(RepoBuffAdderSave _) {
			Debug.LogWarning("Aiza:VitBuffAdder.InitFromSave() should not be called.");
		}
		#endregion
	}
}