using System;
using System.Collections.Generic;
using System.Linq;
using OldGameLogic.Utilities;

namespace OldGameLogic.Model.Element.Vill {
	/// <summary>
	/// 实现村民资源产出的buff逻辑
	/// </summary>
	public class RepoBuffHelper : ISaveable<RepoBuffHelperSave>, IRepoBuffHelper {

		private readonly LogicImpler _impler;

		public RepoBuffHelper(LogicImpler logicImpler) {
			_impler = logicImpler;
		}
		public void LogicDestroy() { }

		#region IVillRepoBuff
		private readonly List<RepoBuffAdder> _repoBuffAdders = new();
		public RTList<float> ProdBuffs_F { get; } = new(fill: true);
		public RTList<float> ConsBuffs_F { get; } = new(fill: true);

		public void AddConsBuff_Eternal(RTList<float> buffs) {
			ConsBuffs_F.Add(buffs);
		}
		public void AddProdBuff_Eternal(RTList<float> buffs) {
			ProdBuffs_F.Add(buffs);
		}

		public void AddConsBuff_Temp(RTList<float> buffs, int ticks) {
			ConsBuffs_F.Add(buffs);
			var buffsClone = buffs.Clone();
			_repoBuffAdders.Add(new(buffsClone, ticks, RepoBuffType.Cons, (adder) => {
				ConsBuffs_F.Sub(buffsClone);
				_repoBuffAdders.Remove(adder);
			}));
		}
		public void AddProdBuff_Temp(RTList<float> buffs, int ticks) {
			ProdBuffs_F.Add(buffs);
			var buffsClone = buffs.Clone();
			_repoBuffAdders.Add(new(buffsClone, ticks, RepoBuffType.Prod, (adder) => {
				ProdBuffs_F.Sub(buffsClone);
				_repoBuffAdders.Remove(adder);
			}));
		}
		#endregion

		#region ISaveable
		public RepoBuffHelperSave GetSave() {
			return new() {
				ProdBuffs_F = ProdBuffs_F.GetSave(),
				ConsBuffs_F = ConsBuffs_F.GetSave(),
				Adders = _repoBuffAdders.Select((adder) => adder.GetSave()).ToList(),
			};
		}
		public void InitFromSave(RepoBuffHelperSave save) {
			ProdBuffs_F.InitFromSave_Full(save.ProdBuffs_F);
			ConsBuffs_F.InitFromSave_Full(save.ConsBuffs_F);
			_repoBuffAdders.ForEach((adder) => adder.Stop());
			_repoBuffAdders.Clear();

			save.Adders.ForEach((adderSave) => {

				var buffs = new RTList<float>();
				buffs.InitFromSave(adderSave.Buffs);

				var buffsClone = buffs.Clone();
				Action<RepoBuffAdder> action =
					adderSave.RepoBuffType == RepoBuffType.Cons ?
					((a) => {
						ConsBuffs_F.Sub(buffsClone);
						_repoBuffAdders.Remove(a);
					})
					: ((a) => {
						ProdBuffs_F.Sub(buffsClone);
						_repoBuffAdders.Remove(a);
					});

				var adder = new RepoBuffAdder(buffs, adderSave.Ticks, adderSave.RepoBuffType, action);

				_repoBuffAdders.Add(adder);
			});
		}
		#endregion
	}
}