using System.Collections.Generic;
using GameLogic.Model.Element.Arch;
using GameLogic.Model.Factory;
using GameLogic.Model.Mgr;
using NSFrame;
using UnityEngine;

namespace GameLogic.Model.Element.Vill {
	public class TaskRunner : ISaveable<TaskRunnerSave>, IVillTaskRun {
		private readonly LogicImpler _impler;
		public TaskRunner(LogicImpler impler) {
			_impler = impler;
			_impler.TickUpdate += TickUpdate;
			_impler.OnDayStart += OnDayStart;
			_impler.OnNightStart += OnNightStart;
		}
		public void LogicDestroy() { }


		/// <summary>
		/// 等待执行的 Task 队列
		/// </summary>
		private readonly Queue<TaskBase> _tasks = new();
		/// <summary>
		/// 当前正在执行的 Task
		/// </summary>
		private TaskBase _curTask;

		private bool GoWork() {
			if (_impler.BondArchHelper.BondedWorkArchID == 0) { return false; }
			var arch = WorldMgr.Inst.FindArch(_impler.BondArchHelper.BondedWorkArchID);
			return SetGoWorkTasks(arch);
		}
		private void OnDayStart() {
			if (!GoWork()) { ResetTasks(); }
		}
		private void OnNightStart() {
			if (!SetGoSleepTasks()) {
				_impler.LogicDestroy();
			}
		}
		private void TickUpdate() {
			if (_curTask == null || _curTask.IsEnded) {
				if (_curTask != null) {
					PoolSystem.PushObj(_curTask.GetType(), _curTask);
					_curTask = null;
				}
				_tasks.TryDequeue(out _curTask);
				if (_curTask == null) {
					Coord target;
					do {
						// 如果没有任务，则随机一个空闲的村民位置作为目标位置
						target = RouteMgr.Inst.GetRandomVillSpareCoord();
					} while (target == _impler.Coord);
					_curTask = LogicFctry.Inst.NewMoveToTask(target, MoveToTargetType.Spare);
					_curTask.SetVill(_impler);
				}
				_curTask.TaskEnter();
			}

			_curTask.TaskExecute();
		}

		/// <summary>
		/// 在当前任务列表后追加一个任务
		/// </summary>
		private void AppendTask(TaskBase task) {
			task.SetVill(_impler);
			_tasks.Enqueue(task);
		}


		#region IVillTaskRun
		public TaskType? CurTaskType => _curTask?.TaskType ?? null;
		public MoveToTargetType? CurMoveToTargetType {
			get {
				if (_curTask is MoveToTask moveToTask) {
					return moveToTask.TargetType;
				}
				return null;
			}
		}
		public void ResetTasks(params TaskBase[] task) {

			// 这样写似乎可以保证在End中调用ResetTasks不会出错
			if (_curTask != null) {
				var tmpTask = _curTask;
				_curTask = null;
				tmpTask.TaskEnd();
				PoolSystem.PushObj(tmpTask.GetType(), tmpTask);
			}

			foreach (var t in _tasks) { PoolSystem.PushObj(t.GetType(), t); }
			_tasks.Clear();

			for (int i = 0; i < task.Length; i++) { AppendTask(task[i]); }
		}

		public bool SetGoWorkTasks(ArchLogicBase arch) {
			if (arch == null) { return false; }
			if (arch.ArchType == ArchType.Cottage) { return false; }
			ResetTasks(
				LogicFctry.Inst.NewMoveToTask(arch.Coord, MoveToTargetType.WorkArch),
				LogicFctry.Inst.NewWorkTask()
			);
			return true;
		}
		public bool SetGoSleepTasks() {
			var homeID = _impler.BondArchHelper.HomeID;
			if (homeID == 0) { return false; }
			var cottage = WorldMgr.Inst.FindArch(homeID);
			// if (!cottage.HasBondedVill(_impler.ID)) { return false; }
			ResetTasks(
				LogicFctry.Inst.NewMoveToTask(cottage.Coord, MoveToTargetType.HomeSleep),
				LogicFctry.Inst.NewSleepTask()
			);
			return true;
		}
		public bool SetGoRecoverVitTasks() {
			var homeID = _impler.BondArchHelper.HomeID;
			if (homeID == 0) { return false; }
			var cottage = WorldMgr.Inst.FindArch(homeID);

			ResetTasks(
				LogicFctry.Inst.NewMoveToTask(cottage.Coord, MoveToTargetType.HomeEat),
				LogicFctry.Inst.NewRecoverVitTask(),
				LogicFctry.Inst.NewWorkTask()
			);
			return true;
		}

		#endregion

		#region ISaveable
		public TaskRunnerSave GetSave() {
			var save = new TaskRunnerSave() {
				Tasks = new(),
			};
			if (_curTask != null) save.Tasks.Add(_curTask.GetSave());
			foreach (var task in _tasks) {
				save.Tasks.Add(task.GetSave());
			}
			return save;
		}
		public void InitFromSave(TaskRunnerSave save) {
			_tasks.Clear();
			try {
				foreach (var taskSave in save.Tasks) {
					AppendTask(LogicFctry.Inst.LoadTask(taskSave));
				}
			} catch {
				// 如果加载失败，多半是因为没有存任何任务，所以直接清空任务列表即可
			}
		}
		#endregion
	}
}