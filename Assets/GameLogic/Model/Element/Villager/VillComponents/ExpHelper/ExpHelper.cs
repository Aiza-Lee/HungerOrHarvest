using GameLogic.Model.Mgr;
using NSFrame;
using UnityEngine;

namespace GameLogic.Model.Element.Vill
{
	/// <summary>
	/// 村民经验值管理器
	/// </summary>
	public class ExpHelper : ISaveable<ExpHelperSave> {

		private readonly JTList<int> _jobLevel_F = new();
		private readonly JTList<float> _jobExps_F = new();
		private readonly RTList<float> _consBuffs_F = new();
		private readonly RTList<float> _prodBuffs_F = new();
		private readonly VillLogicBase _vill;

		public RTList<float> ConsBuffs_F => _consBuffs_F;
		public RTList<float> ProdBuffs_F => _prodBuffs_F;

		public ExpHelper(VillLogicBase vill) {
			_vill = vill;
		}

		private void LevelUpImpl(JobType job) {
			var idx = (int) job;
			var levelNow = ++_jobLevel_F[idx].Value;

			var jConfig = ConfigMgr.Config.FindJobConfig(job);
			var levelConfig = jConfig.JobLevelConfigs[levelNow];

			foreach (var pr in levelConfig.RepoConsBuff.List) {
				_consBuffs_F[pr.Index].Value += pr.Value;
			}
			foreach (var pr in levelConfig.RepoProdBuff.List) {
				_prodBuffs_F[pr.Index].Value += pr.Value;
			}

			EventSystem.Invoke<ulong, JobType>(
				(int) ModelEvt.VillLevelUp_VuJ_2,
				_vill.ID, job,
				NSFrame.EventType.Model
			);
		}

		#region PublicMethods

		public JTList<int> GetSortedJobLevelsImpl() {
			JTList<int> res = _jobLevel_F.Clone();
			res.List.Sort((a, b) => b.Value.CompareTo(a.Value));
			// 这里是用这个标记是否可以使用下标访问内容
			res.Full = false;
			return res;
		}

		/// <summary>
		/// 添加经验值，如果当前经验值满了而没有下一级的 Config，那经验值不会再增加
		/// </summary>
		public void AddExpImpl(JTList<float> exps) {
			foreach (var JF in exps.List) {
				var idx = JF.Index;
				_jobExps_F[idx].Value += JF.Value;

				var jConfig = ConfigMgr.Config.FindJobConfig(idx);
				var level = _jobLevel_F[idx].Value;

				var levelUpDemand = jConfig.JobLevelConfigs[level].LevelUpDemand;
				if (_jobExps_F[idx].Value >= levelUpDemand) {
					if (jConfig.JobLevelConfigs.Count - 1 > level) {
						_jobExps_F[idx].Value -= levelUpDemand;
						LevelUpImpl(JF.JobType);
					} else {
						_jobExps_F[idx].Value = levelUpDemand;
					}
				}
			}
		}

		public float GetJobExpProportionImpl(JobType jobType) {
			var idx = (int) jobType;
			return
				Mathf.Clamp01(_jobExps_F[idx].Value /
				ConfigMgr.Config.FindJobConfig(jobType).JobLevelConfigs[_jobLevel_F[idx].Value].LevelUpDemand);
		}

		public int GetJobLevelImpl(JobType job) {
			return _jobLevel_F[(int) job].Value;
		}

		#endregion


		#region ISaveable
		public ExpHelperSave GetSave() {
			return new() {
				JobLevel = _jobLevel_F.GetSave(),
				JobExps = _jobExps_F.GetSave(),
				ConsBuffs = _consBuffs_F.GetSave(),
				ProdBuffs = _prodBuffs_F.GetSave(),
			};
		}

		public void InitFromSave(ExpHelperSave save) {
			_jobLevel_F.InitFromSave_Full(save.JobLevel);
			_jobExps_F.InitFromSave_Full(save.JobExps);
			_consBuffs_F.InitFromSave_Full(save.ConsBuffs);
			_prodBuffs_F.InitFromSave_Full(save.ProdBuffs);
		}
		#endregion
	}
}