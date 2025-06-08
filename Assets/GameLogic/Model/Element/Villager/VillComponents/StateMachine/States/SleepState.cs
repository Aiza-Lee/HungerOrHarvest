using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
    public class SleepState : StateBase {
        public override State StaType => State.Sleep;
        
        public SleepState() {
            Transitions.Add(new(ToWork, State.Work));
            Transitions.Add(new(ToLowVit, State.LowVit));
        }
        
        public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

        private bool ToWork() {
            // 判断是否睡眠充足可以工作
            return false;
        }

        private bool ToLowVit() {
            // 判断是否因为某些原因进入低体力状态
            if (VitHelper.CurVitProportion < ConfigMgr.Config.VitConfig.LowVitThreshold) {
                return true;
            }
            return false;
        }

        public override void Execute() {
            // 实现睡眠状态的主要逻辑
            // 恢复体力等
        }

        protected override void LogicDestroy_Derived() {
            // 实现清理逻辑
        }

        public override void OnEnd() {
            // 实现状态结束时的逻辑
        }

        public override void OnEnter() {
            // 实现状态进入时的逻辑
            // 初始化睡眠相关的参数
        }
    }
}