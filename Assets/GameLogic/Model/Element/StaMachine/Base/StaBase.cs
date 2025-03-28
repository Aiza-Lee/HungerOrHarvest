// using NSFrame;

// namespace GameLogic
// {
// 	/// <summary>
// 	/// <para>状态机的状态基类</para>
// 	/// <para>不太清楚设计是否合理，就是某个状态不应该获得除了状态持有者之外的其他信息</para>
// 	/// </summary>
// 	public abstract class StaBase : ISaveable<StaSaveBase>, IPooledObject {

// 		protected StaMachine _staMachine;
// 		protected VillLogicBase AttachedVill => _staMachine.AttachedVill;
// 		private bool _entered;
// 		public bool Entered => _entered;

// 		public abstract StaType StaType { get; }
// 		public virtual void Enter() {
// 			_entered = true;
// 		}
// 		public abstract void Execute();
// 		public abstract void Exit();


// 		public void SetStaMachine(StaMachine sm) { _staMachine = sm; }


// 		#region IPooledObject
// 		protected abstract void InitAfterPop_Derived();
// 		public void InitAfterPop() {
// 			InitAfterPop_Derived();
// 			EventSystem.AddListener((int)LogicEvt.Tick, Execute);
// 		}
// 		protected abstract void CleanBeforePush_Derived();
// 		public void CleanBeforePush() {
// 			CleanBeforePush_Derived();
// 			_staMachine = null;
// 			EventSystem.RemoveListener((int)LogicEvt.Tick, Execute);
// 		}
// 		#endregion
		
// 		#region ISaveable
// 		protected abstract StaSaveBase GetDerivedSave();
// 		public StaSaveBase GetSave() {
// 			var save = GetDerivedSave();
// 				save.StaType = StaType;
// 				save.Entered = _entered;
// 			return save;
// 		}

// 		protected abstract void InitDerivedFromSave(StaSaveBase save);
// 		public void InitFromSave(StaSaveBase save) {
// 			_entered = save.Entered;
// 			InitDerivedFromSave(save);
// 		}
// 		#endregion
// 	}
// }