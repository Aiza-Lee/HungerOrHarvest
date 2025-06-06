using System;
using System.Collections.Generic;

namespace GameLogic.Model.Element.Vill {
    public class ArriveState : StateBase {
        public override State StaType => State.Arrive;
        
        public ArriveState() {
            // 在这里添加状态转移
            Transitions.Add(new(ToWork, State.Work));
            Transitions.Add(new(ToMove, State.Moving));
        }
        
        public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

        private bool ToWork() {
            // 添加转换到工作状态的逻辑
            return false;
        }

        private bool ToMove() {
            // 添加转换到移动状态的逻辑
            return false;
        }

        public override void Execute() {
            // 实现到达状态的主要逻辑
        }

        public override void OnEnd() {
            // 实现状态结束时的逻辑
        }

        public override void OnEnter() {
            // 实现状态进入时的逻辑
        }
    }
}