using NSFrame;

namespace GameLogic
{
	public abstract class StaBase : ISaveable<StaSaveBase>, IPooledObject {

		protected StaMachine _staMachine;

		public abstract StaType StaType { get; }
		public abstract void Enter();
		public abstract void Execute();
		public abstract void Exit();


		public void SetStaMachine(StaMachine sm) { _staMachine = sm; }


		#region IPooledObject
		protected abstract void DerivedInitForPool();
		public void InitForPool() {
			DerivedInitForPool();
			EventSystem.AddListener((int)LogicEvt.Tick, Execute);
		}
		protected abstract void DerivedDestroyForPool();
		public void DestroyForPool() {
			DerivedDestroyForPool();
			_staMachine = null;
			EventSystem.RemoveListener((int)LogicEvt.Tick, Execute);
		}
		#endregion
		
		#region ISaveable
		protected abstract StaSaveBase GetDerivedSave();
		public StaSaveBase GetSave() {
			var save = GetDerivedSave();
			save.StaType = StaType;
			return save;
		}

		protected abstract void InitDerivedFromSave(StaSaveBase save);
		public void InitFromSave(StaSaveBase save) {
			InitDerivedFromSave(save);
		}
		#endregion
	}
}