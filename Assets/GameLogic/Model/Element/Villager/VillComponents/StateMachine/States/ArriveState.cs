using System;
using System.Collections.Generic;

namespace GameLogic.Model.Element.Vill {
	public class ArriveState : StateBase {
		public override State StaType => State.Arrive;

		public ArriveState() {
			// 在这里添加状态转移
			Transitions.Add(new(ToWork, State.Work));
			Transitions.Add(new(ToRecover, State.Recover));
			Transitions.Add(new(ToDie, State.Die));
			Transitions.Add(new(ToSleep, State.Sleep));
			Transitions.Add(new(ToMoving, State.Moving));
		}
		public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

		private bool ToWork() => StateMachine.MoveToTarget == MoveToTargetType.Work;
		private bool ToRecover() => StateMachine.MoveToTarget == MoveToTargetType.Recover;
		private bool ToDie() => StateMachine.MoveToTarget == MoveToTargetType.Die;
		private bool ToSleep() => StateMachine.MoveToTarget == MoveToTargetType.Sleep;
		private bool ToMoving() => StateMachine.MoveToTarget == MoveToTargetType.Random;

		public override void Execute() { }

		public override void OnEnd() {
			if (StateMachine.MoveToTarget != MoveToTargetType.Random) {
				StateMachine.MoveToTarget = null; // 清除移动目标
			}
		}

		public override void OnEnter() {}
	}
}