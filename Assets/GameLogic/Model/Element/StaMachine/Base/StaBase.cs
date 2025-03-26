using NSFrame;

namespace GameLogic
{
	public abstract class StaBase : ISaveable<StaSaveBase>, IPooledObject {

		protected StaMachine _staMachine;
		protected VillLogicBase AttachedVill => _staMachine.AttachedVill;
		private bool _entered;
		public bool Entered => _entered;

		public abstract StaType StaType { get; }
		public virtual void Enter() {
			_entered = true;
		}
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
				save.Entered = _entered;
			return save;
		}

		protected abstract void InitDerivedFromSave(StaSaveBase save);
		public void InitFromSave(StaSaveBase save) {
			_entered = save.Entered;
			InitDerivedFromSave(save);
		}
		#endregion
	}
}