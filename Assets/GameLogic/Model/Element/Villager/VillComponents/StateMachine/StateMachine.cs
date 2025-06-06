using GameLogic.Model.Mgr;
using GameLogic.Utilities;

namespace GameLogic.Model.Element.Vill {
	public class StateMachine : IStateMachine, ISaveable<StateMachineSave> {

		private readonly LogicImpler _impler;
		public StateMachine(LogicImpler impler) {
			_impler = impler;
			_impler.TickUpdate += Execute;
		}

		private StateBase _curState;

		public State CurStaType => _curState.StaType;
		public string CurStateDescription {
			get {
				return null;
			}
		}

		public int RecoverChance { get; set; }
		public MoveToTargetType? MoveToTarget { get; set; }
		public bool IsDying { get; set; }
		public Coord? MoveTargetCoord { get; set; }

		public void LogicDestroy() {
			_impler.TickUpdate -= Execute;
			_curState?.LogicDestroy();
			_curState = null;
		}

		/// <summary>
		/// 检查当前状态是否需要转移
		/// </summary>
		private void DoTransit() {
			_curState.Transitions.ForEach(pair => {
				if (pair.Key()) {
					ChangeState(pair.Value);
				}
			});
		}

		private void ChangeState(State state) {
			if (_curState != null) {
				_curState.OnEnd();
				_curState.LogicDestroy();
			}
			_curState = StateFactory.Inst.CreateState(state, _impler);
			_curState.OnEnter();
			// 执行转移逻辑，确保在状态执行Execute之前进行状态转移
			DoTransit();
		}

		private void Execute() {
			DoTransit();
			_curState.Execute();
		}

		public StateMachineSave GetSave() {
			return new StateMachineSave {
				MoveToTarget = new(MoveToTarget),
				RecoverChance = RecoverChance,
				CurStaType = new(CurStaType),
				MoveTargetCoord = new(MoveTargetCoord),
				IsDying = IsDying,
			};
		}
		public void InitFromSave(StateMachineSave save) {
			try {
				RecoverChance = save.RecoverChance;
				MoveToTarget = save.MoveToTarget.ToNullable();
				MoveTargetCoord = save.MoveTargetCoord.ToNullable();
				IsDying = save.IsDying;
				ChangeState(save.CurStaType.ToEnum().Value);
			} catch {
				// 加载失败就直接加载默认的状态
				RecoverChance = ConfigMgr.Config.VitConfig.RecoverChancePerDay;
				MoveToTarget = MoveToTargetType.Random;
				MoveTargetCoord = null;
				IsDying = false;
				ChangeState(State.Moving);
			}
		}

	}
}