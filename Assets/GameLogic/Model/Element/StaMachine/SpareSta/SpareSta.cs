// using System.Collections.Generic;

// namespace GameLogic
// {
// 	public class SpareSta : StaBase { 
// 		public override StaType StaType { get => StaType.Spare; }

// 		private Coord _target;
// 		private bool _isArrived;
// 		private List<Coord> _route;

// 		private int _idx;
// 		private int _timer;

// 		private VillLogicBase Vill => AttachedVill;


// 		public override void Enter() {}
// 		public override void Execute() {
// 			if (_isArrived || _route.Count == 0) {
				

// 				do {
// 					_target = RouteMgr.Inst.GetRandomVillSpareCoord();
// 				} while(_target == Vill.Coord);
				
// 				_route = RouteMgr.Inst.GetRoute(Vill.Coord, _target);
// 				_isArrived = false;
// 				_idx = 0;
// 				_timer = 0;
// 			}
// 			++_timer;
// 			if (_timer >= ConstMgr.Inst.Config.VILL_ONE_MOVE_TICK) {
// 				_timer = 0;
// 				Vill.Move(Vill.Coord.DirectionTo(_route[_idx]));
// 			}
// 			if (_route[_idx] == Vill.Coord) {
// 				_idx++;
// 				if (_idx == _route.Count) { _isArrived = true; }
// 			}
// 		}
// 		public override void Exit() {}

// 		#region IPooledObject
// 		protected override void InitAfterPop_Derived() {}
// 		protected override void CleanBeforePush_Derived() {
// 			_route = null;
// 		}
// 		#endregion

// 		#region ISaveable
// 		protected override StaSaveBase GetDerivedSave() {
// 			return new SpareStaSave() {
// 				Target = _target,
// 				IsArrived = _isArrived,
// 				Route = new(_route),
// 				Timer = _timer,
// 				Idx = _idx
// 			};
// 		}
// 		protected override void InitDerivedFromSave(StaSaveBase save) {
// 			var sv = save as SpareStaSave;
// 			_target = sv.Target;
// 			_isArrived = sv.IsArrived;
// 			_route = sv.Route;
// 			_timer = sv.Timer;
// 			_idx = sv.Idx;
// 		}
// 		#endregion

// 	}
// }