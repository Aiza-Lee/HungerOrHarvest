using System;
using System.Collections.Generic;
using GameLogic.Model.Mgr;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	public class MovingState : StateBase {
		public override State StaType => State.Moving;

		public MovingState() {
			Transitions.Add(new(ToArrive, State.Arrive));
			Transitions.Add(new(ToMoving, State.Moving));
		}

		public override List<Pair<Func<bool>, State>> Transitions { get; } = new();

		private MoveToTargetType _originalMoveToTarget;

		private List<Coord> _path;
		private int _curPathIndex;

		/// <summary>
		/// 移动状态的计时器，用于控制移动的速率。
		/// </summary>
		private int _moveTick;
		private readonly int MOVE_INTERVAL = ConfigMgr.Config.VILL_ONE_MOVE_TICK_NORMAL;

		private bool ToArrive() {
			if (_impler.Coord == StateMachine.MoveTargetCoord) {
				return true;
			}
			return false;
		}

		private bool ToMoving() {
			// 判断是否需要移动到新的位置
			// 比如切换了新的工作的时候，应该要结束当前的移动移动到新的工作地点
			if (StateMachine.MoveToTarget.HasValue && StateMachine.MoveToTarget.Value != _originalMoveToTarget) {
				return true;
			}
			return false;
		}

		public override void Execute() {
			if (_path == null || _curPathIndex >= _path.Count) {
				Debug.LogError("MovingState Execute: 路径完成仍然停留在MovingState");
				return;
			}
			_moveTick++;
			if (_moveTick < MOVE_INTERVAL) {
				return; // 还没到达移动间隔，不执行移动
			}
			_moveTick = 0;
			// 执行移动逻辑
			var nextCoord = _path[_curPathIndex];
			if (_impler.Coord == nextCoord) {
				_curPathIndex++;
				if (_curPathIndex >= _path.Count) {
					return; // 到达路径终点，等待状态转移
				}
				nextCoord = _path[_curPathIndex];
			}
			_impler.Move(_impler.Coord.DirectionTo(nextCoord));
		}

		protected override void LogicDestroy_Derived() {
			base.LogicDestroy_Derived();
			_path = null;
		}

		public override void OnEnd() {
			_path = null;
		}

		public override void OnEnter() {
			if (!StateMachine.MoveToTarget.HasValue) {
				Debug.LogError("MovingState OnEnter: 上一个状态转移来之前没有设置移动目标，无法进入MovingState。");
				return;
			}

			_moveTick = 0;
			_originalMoveToTarget = StateMachine.MoveToTarget.Value;

			switch (StateMachine.MoveToTarget) {
				case MoveToTargetType.Random:
					StateMachine.MoveTargetCoord = RouteMgr.Inst.GetRandomVillSpareCoord();
					_path = RouteMgr.Inst.GetRoute(StateMachine.MoveTargetCoord.Value, _impler.Coord);
					_curPathIndex = 0;
					break;
				case MoveToTargetType.Sleep:
				case MoveToTargetType.Recover:
					StateMachine.MoveTargetCoord = WorldMgr.Inst.FindArch(BondArchHelper.HomeID).Coord;
					_path = RouteMgr.Inst.GetRoute(StateMachine.MoveTargetCoord.Value, _impler.Coord);
					_curPathIndex = 0;
					break;
				case MoveToTargetType.Work:
					StateMachine.MoveTargetCoord = WorldMgr.Inst.FindArch(BondArchHelper.BondedWorkArchID).Coord;
					_path = RouteMgr.Inst.GetRoute(StateMachine.MoveTargetCoord.Value, _impler.Coord);
					_curPathIndex = 0;
					break;
				case MoveToTargetType.Die:
					// Todo: 目前死亡是回到家中死亡，以后估计需要修改为走到村庄外某位置
					StateMachine.MoveTargetCoord = WorldMgr.Inst.FindArch(BondArchHelper.HomeID).Coord;
					_path = RouteMgr.Inst.GetRoute(StateMachine.MoveTargetCoord.Value, _impler.Coord);
					_curPathIndex = 0;
					break;
			}
		}
	}
}