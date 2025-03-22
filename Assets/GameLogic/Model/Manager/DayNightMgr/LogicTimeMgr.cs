using NSFrame;

namespace GameLogic
{
	public sealed class LogicTimeMgr : ISaveable<LogicTimeMgrSave> {
		private LogicTimeMgr() { EventSystem.AddListener((int)LogicEvt.InitAllManager, () => TickTrigger.Inst.AfterTick += AddTick); }
		~LogicTimeMgr() { TickTrigger.Inst.AfterTick -= AddTick; }
		public static LogicTimeMgr Inst { get; } = new();

		private ulong _tickSum;
		private ulong _todayTick;
		private bool _inDay = true;
		private ulong _days;

		public ulong TickSum => _tickSum;
		public ulong TodayTick => _todayTick;
		public bool Day => _inDay;
		public ulong Days => _days;


		private void AddTick() {
			++_tickSum;
			++_todayTick;
			if (_inDay) {
				if (_todayTick == ConstMgr.Inst.Config.DAY_TICKS) {
					_inDay = false;
					EventSystem.Invoke((int)LogicEvt.NightStart);
				} 
			} else {
				if (_todayTick == ConstMgr.Inst.Config.DAY_TICKS + ConstMgr.Inst.Config.NIGHT_TICKS) {
					_inDay = true;
					_days++;
					_todayTick = 0;
					EventSystem.Invoke((int)LogicEvt.DayStart);
				}
			}
		}


		public LogicTimeMgrSave GetSave() {
			return new LogicTimeMgrSave() {
				TickSum = _tickSum,
				TodayTick = _todayTick,
				InDay = _inDay,
				Days = _days,
			};
		}
		public void InitFromSave(LogicTimeMgrSave save) {
			_tickSum = save.TickSum;
			_todayTick = save.TodayTick;
			_inDay = save.InDay;
			_days = save.Days;
		}
	}
}