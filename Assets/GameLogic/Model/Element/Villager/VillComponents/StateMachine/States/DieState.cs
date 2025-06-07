using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
    public class DieState : StateBase {
        public override State StaType => State.Die;
        
        public DieState() {
            // 死亡状态是终止状态，不需要转移
        }
        
        public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

        public override void Execute() {
            // 实现死亡状态的主要逻辑
        }

        protected override void LogicDestroy_Derived() {
            // 实现清理逻辑
        }

        public override void OnEnd() {
            // 实现状态结束时的逻辑
        }

        public override void OnEnter() {
            // 实现状态进入时的逻辑
            // 可能需要触发死亡相关的游戏事件
        }
    }
}