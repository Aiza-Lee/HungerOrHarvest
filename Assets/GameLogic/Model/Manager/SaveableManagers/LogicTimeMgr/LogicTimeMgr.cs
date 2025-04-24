using NSFrame;

namespace GameLogic
{ 
	public sealed class LogicTimeMgr : ISaveable<LogicTimeMgrSave>, IClearMgr {
		private LogicTimeMgr() { 
			EventSystem.AddListener(
				(int)ModelEvt.MgrInitAfterMono, 
				() => TickTrigger.Inst.AfterTick += AddTick, 
				NSFrame.EventType.Model
			);
		}
		~LogicTimeMgr() { TickTrigger.Inst.AfterTick -= AddTick; }
		public static LogicTimeMgr Inst { get; } = new();

		private ulong _tickSum;
		private ulong _todayTick;
		private bool _inDay = true;
		private ulong _days;

		private float _lastDaySpeed;

		private ulong DAY_TICKS => ConstMgr.Inst.Config.DAY_TICKS;
		private ulong NIGHT_TICKS => ConstMgr.Inst.Config.NIGHT_TICKS;

		public ulong TickSum => _tickSum;
		public ulong TodayTick => _todayTick;
		public bool IsDay => _inDay;
		public ulong Days => _days;
		public float DayProcess => 1f * _todayTick / DAY_TICKS;
		public float NightProcess => 1f * (_todayTick - DAY_TICKS) / NIGHT_TICKS;

		private void AddTick() {
			++_tickSum;
			++_todayTick;
			if (_inDay) {
				if (_todayTick == DAY_TICKS) {
					_inDay = false;
					EventSystem.Invoke((int)ModelEvt.NightStart_0, NSFrame.EventType.Model);
					_lastDaySpeed = TickTrigger.Inst.Speed;
					TickTrigger.Inst.Pause = true;
				}
			} else {
				if (_todayTick == DAY_TICKS + NIGHT_TICKS) {
					_inDay = true;
					_days++;
					_todayTick = 0;
					EventSystem.Invoke((int)ModelEvt.DayStart_0, NSFrame.EventType.Model);
					TickTrigger.Inst.Speed = _lastDaySpeed;
				}
			}
		}

		#region PublicMethods
		public void PassNight() {
			if (_inDay) return;
			var speed = ConstMgr.GetConfig.NIGHT_TIME_SPEED;
			TickTrigger.Inst.Speed = speed;
			TickTrigger.Inst.Pause = false;
		}
		#endregion


		public LogicTimeMgrSave GetSave() {
			return new LogicTimeMgrSave() {
				TickSum 		= _tickSum,
				TodayTick 		= _todayTick,
				InDay 			= _inDay,
				Days 			= _days,
			};
		}
		public void InitFromSave(LogicTimeMgrSave save) {
			_tickSum 				= save.TickSum;
			_todayTick 				= save.TodayTick;
			_inDay 					= save.InDay;
			_days 					= save.Days;
		}

		public void ClearMgr() { }
	}
}