using GameLogic.Model.Mgr;

namespace GameLogic.View.UI.WorldVillPanel
{
	/// <summary>
	/// 村民展开面板中，展示职业信息的布局
	/// </summary>
	public class JobInfoLayout : GroupLayoutBase {

		#region PublicMethods
		public void Init(ulong villID) {
			Clear();
			for (int i = 0; i < ConstMgr.JOB_TYPE_SIZE; ++i) {
				var jobType = (JobType) i;
				var info = VillExpandJobInfoFactory.Inst.Create(villID, jobType);
				AddEle(info);
			}
		}
		#endregion
	}
}