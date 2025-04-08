using System.Collections.Generic;
using NSFrame;

namespace GameLogic
{
	public class VillTaskRunner : ISaveable<VillTaskRunnerSave> {

		public VillTaskRunner(VillLogicBase vill) {
			AttachedVill = vill;
			EventSystem.AddListener((int)LogicEvt.Tick_0, Execute);
		}
		private readonly Queue<TaskBase> _tasks = new();
		private TaskBase _curTask;

		public VillLogicBase AttachedVill { get; private set; }
		public TaskType CurTaskType => _curTask?.TaskType ?? TaskType.None;

		private void Execute() {

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
					} while (target == AttachedVill.Coord);
					_curTask = LogicFctry.Inst.NewMoveToTask(target);
					_curTask.SetVill(AttachedVill);
				}
				_curTask.Enter();
			}

			_curTask.Execute();
		}


		#region PublicMethods
		public void Destroy() {
			EventSystem.RemoveListener((int)LogicEvt.Tick_0, Execute);
			AttachedVill = null;
			_curTask = null;
		}

		/// <summary>
		/// 在当前任务列表后追加一个任务
		/// </summary>
		public void AppendTask(TaskBase task) {
			task.SetVill(AttachedVill);
			_tasks.Enqueue(task);
		}

		/// <summary>
		/// 直接触发当前任务的End，并将所有任务列表替换为传入的任务
		/// </summary>
		public void ResetTasks(params TaskBase[] task) {
			
			// 这样写似乎可以保证在End中调用ResetTasks不会出错
			if (_curTask != null) {
				var tmpTask = _curTask;
				_curTask = null;
				tmpTask?.End();
				PoolSystem.PushObj(tmpTask.GetType(), tmpTask);
			}

			foreach (var t in _tasks) { PoolSystem.PushObj(t.GetType(), t); }
			_tasks.Clear();

			for (int i = 0; i < task.Length; i++) { AppendTask(task[i]); }
		}
		#endregion

		#region ISaveable
		public VillTaskRunnerSave GetSave() {
			var save = new VillTaskRunnerSave() {
				Tasks = new(),
			};
			save.Tasks.Add(_curTask?.GetSave());
			foreach (var task in _tasks) {
				save.Tasks.Add(task.GetSave());
			}
			return save;
		}
		public void InitFromSave(VillTaskRunnerSave save) {
			foreach (var taskSave in save.Tasks) {
				AppendTask(LogicFctry.Inst.LoadTask(taskSave));
			}
		}
		#endregion
	}
}