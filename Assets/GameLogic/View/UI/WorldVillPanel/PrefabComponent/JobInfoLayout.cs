namespace GameLogic.View.UI.WorldVillPanel
{
	public class JobInfoLayout : GroupLayoutBase {

		#region PublicMethods
		public void Init(ulong villID) {
			for (int i = 0; i < ConstMgr.JOB_TYPE_SIZE; ++i) {
				var jobType = (JobType)i;
				var info = VillExpandJobInfoFactory.Inst.Create(villID, jobType);
				AddEle(info);
			}
		}
		public override void Clear() {
			foreach (var ele in _eles) {
				(ele as VillExpandJobInfo).Clear();
			}
			base.Clear();
		}
		#endregion
	}
}