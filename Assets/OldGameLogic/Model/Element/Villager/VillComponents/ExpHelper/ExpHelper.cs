using System;
using System.Collections.Generic;
using OldGameLogic.Model.Mgr;
using OldGameLogic.Utilities;
using UnityEngine;

namespace OldGameLogic.Model.Element.Vill {
	/// <summary>
	/// 村民经验值管理器
	/// </summary>
	public class ExpHelper : ISaveable<ExpHelperSave>, IExpHelper {

		private readonly JTList<int> _jobLevel_F = new();
		private readonly JTList<float> _jobExps_F = new();
		private readonly LogicImpler _impler;

		public ExpHelper(LogicImpler impler) {
			_impler = impler;
		}

		private void LevelUpImpl(JobType job) {
			var idx = (int) job;
			var levelNow = ++_jobLevel_F[idx].Value;

			var jConfig = ConfigMgr.Config.FindJobConfig(job);
			var levelConfig = jConfig.JobLevelConfigs[levelNow];

			_impler.RepoBuffHelper.AddConsBuff_Eternal(levelConfig.RepoConsBuff);
			_impler.RepoBuffHelper.AddProdBuff_Eternal(levelConfig.RepoProdBuff);

			OnJobLevelUp?.Invoke(job);
		}

		public void LogicDestroy() { }

		#region IVillExp
		public event Action<JobType> OnJobLevelUp;
		public List<JobType> GetSortedJobLevels() {
			var res = new List<JobType>();
			for (int i = 0; i < ConstMgr.JOB_TYPE_SIZE; ++i) {
				res.Add((JobType) i);
			}
			res.Sort((lhv, rhv) => {
				var l = (int) lhv;
				var r = (int) rhv;
				var rk1 = _jobLevel_F[r].Value.CompareTo(_jobLevel_F[l].Value);
				if (rk1 != 0) return rk1;
				return _jobExps_F[r].Value.CompareTo(_jobExps_F[l].Value);
			});
			return res;
		}
		public void AddExp(JTList<float> exps) {
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
		public float GetJobExpProportion(JobType jobType) {
			var idx = (int) jobType;
			return
				Mathf.Clamp01(_jobExps_F[idx].Value /
				ConfigMgr.Config.FindJobConfig(jobType).JobLevelConfigs[_jobLevel_F[idx].Value].LevelUpDemand);
		}
		public int GetJobLevel(JobType job) {
			return _jobLevel_F[(int) job].Value;
		}

		#endregion


		#region ISaveable
		public ExpHelperSave GetSave() {
			return new() {
				JobLevel = _jobLevel_F.GetSave(),
				JobExps = _jobExps_F.GetSave(),
			};
		}

		public void InitFromSave(ExpHelperSave save) {
			_jobLevel_F.InitFromSave_Full(save.JobLevel);
			_jobExps_F.InitFromSave_Full(save.JobExps);
		}
		#endregion
	}
}