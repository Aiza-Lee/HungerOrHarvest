using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
    public class RecoverState : StateBase {
        public override State StaType => State.Recover;
        
        public RecoverState() {
            Transitions.Add(new(ToWork, State.Work));
            Transitions.Add(new(ToLowVit, State.LowVit));
        }
        
        public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

        private bool ToWork() {
            // 判断是否恢复足够可以工作
            if (VitHelper.CurVitProportion > 0.5f) { // 使用一个临时的恢复阈值
                return true;
            }
            return false;
        }

        private bool ToLowVit() {
            // 判断是否恢复失败进入低体力状态
            if (VitHelper.CurVitProportion < ConfigMgr.Config.VitConfig.LowVitThreshold) {
                return true;
            }
            return false;
        }

        public override void Execute() {
            // 实现恢复状态的主要逻辑
            // TODO: 实现体力恢复逻辑
        }

        protected override void LogicDestroy_Derived() {
            // 实现清理逻辑
        }

        public override void OnEnd() {
            // 实现状态结束时的逻辑
        }

        public override void OnEnter() {
            // 实现状态进入时的逻辑
            // 初始化恢复相关的参数
        }
    }
}