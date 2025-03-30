using System.Collections.Generic;

namespace GameLogic
{
	public class MoveToTask : TaskBase {
		public override TaskType TaskType => TaskType.MoveTo;

		private Coord _target;
		private List<Coord> _route;
		private int _idx;
		private int _timer;

		public override void End() { }
		public override void Enter() {
			if (AttachedVill.Coord != _target) {
				_idx = 0;
				_timer = 0;
				_route = RouteMgr.Inst.GetRoute(AttachedVill.Coord, _target);
			} else {
				IsEnded = true;
			}
		}

		public override void Execute() {
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
					End();
				}
			}
		}

		protected override void CleanBeforePush_Derived() { _route.Clear(); }
		protected override void InitAfterPop_Derived() { }

		protected override TaskSaveBase GetSave_Derived() {
			return new MoveToTaskSave() {
				Target = _target,
				Route = new(_route),
				Timer = _timer,
				Idx = _idx,
			};
		}


		protected override void InitFromSave_Derived(TaskSaveBase save) {
			var sv = save as MoveToTaskSave;
			_target = sv.Target;
			_route = sv.Route;
			_timer = sv.Timer;
			_idx = sv.Idx;
		}

	}
}