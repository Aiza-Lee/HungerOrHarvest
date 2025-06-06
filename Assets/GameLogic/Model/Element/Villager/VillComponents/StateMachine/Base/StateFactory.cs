using NSFrame;

namespace GameLogic.Model.Element.Vill {
	public sealed class StateFactory {
		private StateFactory() {
			PoolSystem.InitObjectPool<WorkState>(20);
			PoolSystem.InitObjectPool<SleepState>(20);
			PoolSystem.InitObjectPool<LowVitState>(20);
			PoolSystem.InitObjectPool<MovingState>(20);
			PoolSystem.InitObjectPool<ArriveState>(20);
			PoolSystem.InitObjectPool<RecoverState>(20);
			PoolSystem.InitObjectPool<DieState>(20);
		}
		public static StateFactory Inst { get; } = new();

		public StateBase CreateState(State state, LogicImpler impler) {
			return state switch {
				State.Work => PoolSystem.PopObj<WorkState>().Init(impler),
				State.Sleep => PoolSystem.PopObj<SleepState>().Init(impler),
				State.LowVit => PoolSystem.PopObj<LowVitState>().Init(impler),
				State.Moving => PoolSystem.PopObj<MovingState>().Init(impler),
				State.Arrive => PoolSystem.PopObj<ArriveState>().Init(impler),
				State.Recover => PoolSystem.PopObj<RecoverState>().Init(impler),
				State.Die => PoolSystem.PopObj<DieState>().Init(impler),
				_ => throw new System.ArgumentOutOfRangeException(nameof(state), state, null)
			};
		}
	}
}