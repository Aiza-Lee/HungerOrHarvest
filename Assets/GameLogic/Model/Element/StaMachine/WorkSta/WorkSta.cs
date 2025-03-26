using System.Collections.Generic;

namespace GameLogic 
{
	public class WorkSta : StaBase { 
		public override StaType StaType { get => StaType.Work; }
		private bool _isArrived;
		private List<Coord> _route;
		private int _idx;
		private int _timer;

		private VillLogicBase Vill => AttachedVill;

		public override void Enter() {
			var arch = WorldMgr.Inst.FindArch(Vill.ArchToEnter);
			_route = RouteMgr.Inst.GetRoute(Vill.Coord, arch.Coord);
			_isArrived = false;
		}
		public override void Execute() {
			if (_isArrived) return;
			++_timer;
			if (_timer >= ConstMgr.Inst.Config.VILL_ONE_MOVE_TICK) {
				_timer = 0;
				Vill.Move(Vill.Coord.DirectionTo(_route[_idx]));
			}
			if (_route[_idx] == Vill.Coord) {
				_idx++;
				if (_idx == _route.Count) {
					_isArrived = true; 
					_route = null;
				}
			}
		}

		public override void Exit() {
		}

		#region IPooledObject
		protected override void DerivedDestroyForPool() {
			_route = null;
		}
		protected override void DerivedInitForPool() {}
		#endregion

		#region ISaveable
		protected override StaSaveBase GetDerivedSave() {
			return new WorkStaSave() {
				IsArrived = _isArrived,
				Route = _route == null ? null : new(_route),
				Timer = _timer,
				Idx = _idx,
			};
		}
		protected override void InitDerivedFromSave(StaSaveBase save) {
			var sv = save as WorkStaSave;
			_isArrived = sv.IsArrived;
			if (_isArrived) return;
			_route = sv.Route;
			_timer = sv.Timer;
			_idx = sv.Idx;
		}
		#endregion
	}
}