using OldGameLogic.Controller;
using OldGameLogic.Utilities;
using NSFrame;

namespace OldGameLogic.Model.Mgr
{ 
	public sealed class LogicTimeMgr : ISaveable<LogicTimeMgrSave>, IMananger {
		private LogicTimeMgr() { 
			EventSystem.AddListener(
				(int)ModelEvt.MgrInitAfterMonoMgr, 
				() => TickTrigger.Inst.BeforeTick += AddLogicTickBeforeTick, 
				NSFrame.EventType.Model
			);
		}
		~LogicTimeMgr() { TickTrigger.Inst.BeforeTick -= AddLogicTickBeforeTick; }
		public static LogicTimeMgr Inst { get; } = new();

		/// <summary>
		/// 是否正在加载存档，如果是的话，就不应该在当前自动存档的时候保存
		/// </summary>
		public bool IsLoadingNotStartingSave { get; set; }

		private ulong _tickSum;
		/// <summary>
		/// 表示当前正在进行第几个tick
		/// </summary>
		private ulong _todayTick;
		private bool _inDay = true;
		private ulong _days;

		private float _lastDaySpeed;

		private ulong DAY_TICKS => ConfigMgr.Config.DAY_TICKS;
		private ulong NIGHT_TICKS => ConfigMgr.Config.NIGHT_TICKS;

		public ulong TickSum => _tickSum;
		public ulong TodayTick => _todayTick;
		public bool IsDay => _inDay;
		public ulong Days => _days;
		public float DayProcess => 1f * _todayTick / DAY_TICKS;
		public float NightProcess => 1f * (_todayTick - DAY_TICKS) / NIGHT_TICKS;

		/// <summary>
		/// 每个tick触发一次的统计tick的方法，一定会在Tick引起的所有事件之前触发，保证逻辑正确
		/// </summary>
		private void AddLogicTickBeforeTick() {
			// if (_inDay) {
				if (_todayTick == DAY_TICKS) {
					if (!IsLoadingNotStartingSave) {
						CmdRunner.Run("/auto-save");
					} else {
						IsLoadingNotStartingSave = false;
					}
					_inDay = false;
					EventSystem.Invoke((int)ModelEvt.NightStart_0, NSFrame.EventType.Model);
					_lastDaySpeed = TickTrigger.Inst.Speed;
					TickTrigger.Inst.Pause = true;
				}
			// } else {
				if (_todayTick == DAY_TICKS + NIGHT_TICKS) {
					_inDay = true;
					_days++;
					_todayTick = 0;
					EventSystem.Invoke((int)ModelEvt.DayStart_0, NSFrame.EventType.Model);
					TickTrigger.Inst.Speed = _lastDaySpeed;
				}
			// }
			++_tickSum;
			++_todayTick;
		}

		#region PublicMethods
		public void PassNight() {
			if (_inDay) return;
			var speed = ConfigMgr.Config.NIGHT_TIME_SPEED;
			TickTrigger.Inst.Speed = speed;
			TickTrigger.Inst.Pause = false;
		}
		#endregion


		public LogicTimeMgrSave GetSave() {
			return new LogicTimeMgrSave() {
				TickSum 	= _tickSum,
				TodayTick 	= _todayTick,
				InDay 		= _inDay,
				Days 		= _days,
			};
		}
		public void InitFromSave(LogicTimeMgrSave save) {
			_tickSum 	= save.TickSum;
			_todayTick 	= save.TodayTick;
			_inDay 		= save.InDay;
			_days 		= save.Days;
		}

		public void ClearMgr() { }
	}
}