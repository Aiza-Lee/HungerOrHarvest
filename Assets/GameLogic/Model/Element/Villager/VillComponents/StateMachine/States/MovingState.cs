using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;

namespace GameLogic.Model.Element.Vill {
    public class MovingState : StateBase {
        public override State StaType => State.Moving;
        
        public MovingState() {
            Transitions.Add(new(ToArrive, State.Arrive));
            Transitions.Add(new(ToLowVit, State.LowVit));
        }
        
        public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

        private bool ToArrive() {
            // 判断是否到达目标位置
            return false;
        }

        private bool ToLowVit() {
            // 判断移动过程中是否体力不足
            if (VitHelper.VitPercentage < ConfigMgr.Config.VitConfig.LowVitThreshold) {
                return true;
            }
            return false;
        }

        public override void Execute() {
            // 实现移动状态的主要逻辑
            // 更新位置，消耗体力等
        }

        public override void LogicDestroy() {
            // 实现清理逻辑
        }

        public override void OnEnd() {
            // 实现状态结束时的逻辑
        }

        public override void OnEnter() {
            // 实现状态进入时的逻辑
            // 初始化移动目标和路径等
        }
    }
}