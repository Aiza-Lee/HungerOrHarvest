using NSFrame;

namespace GameLogic.Model.Element.Vill
{
	public abstract class TaskBase : ISaveable<TaskSaveBase>, IPooledObject {
		public bool IsEnded { get; protected set; }
		public VillLogicBase AttachedVill { get; private set; }

		public abstract TaskType TaskType { get; }
		public abstract void TaskEnter();
		public abstract void TaskExecute();
		public abstract void TaskEnd();

		public void SetVill(VillLogicBase vill) {
			AttachedVill = vill;
		}


		#region IPooledObject
		protected abstract void InitAfterPop_Derived();
		public void InitAfterPop() {
			InitAfterPop_Derived();
			IsEnded = false;
		}
		protected abstract void CleanBeforePush_Derived();
		public void CleanBeforePush() {
			CleanBeforePush_Derived();
			AttachedVill = null;
		}
		#endregion

		#region ISaveable
		protected abstract TaskSaveBase GetSave_Derived();
		public TaskSaveBase GetSave() {
			var save = GetSave_Derived();
				save.TaskType = TaskType;
				save.IsEnded = IsEnded;
			return save;
		}
		protected abstract void InitFromSave_Derived(TaskSaveBase save);
		public void InitFromSave(TaskSaveBase save) {
			InitFromSave_Derived(save);
			IsEnded = save.IsEnded;
		}

		#endregion
	}
}