using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
    public class LowVitState : StateBase {
        public override State StaType => State.LowVit;
        
        public LowVitState() {
            Transitions.Add(new(ToWork, State.Work));
            Transitions.Add(new(ToMove, State.Moving));
        }
        
        public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

        private bool ToWork() {
            // 判断体力是否恢复到可以工作的水平
            if (VitHelper.VitPercentage > ConfigMgr.Config.VitConfig.LowVitThreshold) {
                return true;
            }
            return false;
        }

        private bool ToMove() {
            // 判断是否需要移动到休息地点
            return false;
        }

        public override void Execute() {
            // 实现低体力状态的主要逻辑
            // 可能包括逐渐恢复体力或消耗资源
        }

        public override void LogicDestroy() {
            // 实现清理逻辑
        }

        public override void OnEnd() {
            // 实现状态结束时的逻辑
        }

        public override void OnEnter() {
            // 实现状态进入时的逻辑
            // 可能需要初始化体力恢复相关的参数
        }
    }
}