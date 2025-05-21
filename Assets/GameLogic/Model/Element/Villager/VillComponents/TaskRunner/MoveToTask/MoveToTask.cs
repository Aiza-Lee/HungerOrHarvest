using System.Collections.Generic;

namespace GameLogic.Model.Element.Vill
{
	public class MoveToTask : TaskBase {
		public override TaskType TaskType => TaskType.MoveTo;

		/// <summary>
		/// 目标坐标
		/// </summary>
		private Coord _target;
		/// <summary>
		/// 进入任务时，计算的路径
		/// </summary>
		private List<Coord> _route;
		/// <summary>
		/// 当前路径的索引
		/// </summary>
		private int _idx;
		/// <summary>
		/// 计时器，用于控制移动速度，判定当前逻辑帧是否可以移动
		/// </summary>
		private int _timer;
		/// <summary>
		/// 移动目标的类型
		/// </summary>
		private MoveToTargetType _targetType;
		public MoveToTargetType TargetType => _targetType;

		public override void TaskEnd() { }
		public override void TaskEnter() {
			if (AttachedVill.Coord != _target) {
				_idx = 0;
				_timer = 0;
				_route = RouteMgr.Inst.GetRoute(AttachedVill.Coord, _target);
			} else {
				IsEnded = true;
			}
		}

		public override void TaskExecute() {
			if (IsEnded) { return; }
			++_timer;
			if (_timer >= ConstMgr.Inst.Config.VILL_ONE_MOVE_TICK) {
				_timer = 0;
				AttachedVill.Move(AttachedVill.Coord.DirectionTo(_route[_idx]));
			}
			if (_route[_idx] == AttachedVill.Coord) {
				++_idx;
				if (_idx >= _route.Count) {
					IsEnded = true;
					TaskEnd();
				}
			}
		}

		protected override void CleanBeforePush_Derived() { _route.Clear(); }
		protected override void InitAfterPop_Derived() { }

		protected override TaskSaveBase GetSave_Derived() {
			return new MoveToTaskSave() {
				Target 		= _target,
				Route 		= new(_route),
				Timer 		= _timer,
				Idx 		= _idx,
				TargetType 	= _targetType
			};
		}
		protected override void InitFromSave_Derived(TaskSaveBase save) {
			var sv = save as MoveToTaskSave;
			_target 	= sv.Target;
			_route 		= sv.Route;
			_timer 		= sv.Timer;
			_idx 		= sv.Idx;
			_targetType = sv.TargetType;
		}

	}
}