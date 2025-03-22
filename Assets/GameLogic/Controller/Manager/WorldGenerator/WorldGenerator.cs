using System.Collections.Generic;
using NSFrame;

namespace GameLogic
{
	public sealed class WorldGenerator : MonoSingleton<WorldGenerator> {

		public List<PreSetConfig> Configs;
		public int ChooseIndex;

		public void Generate() {
			var config = Configs[ChooseIndex];

			/* LAYER_RANGE */
			foreach (var pr in config.Layer_Range) {
				var lyr = pr.Key;
				for (int i = pr.Value.Key; i <= pr.Value.Value; i++) {
					WorldMgr.Inst.UnlockOL(new(i, lyr));
				}
			}

			/* ARCH_OL */
			foreach (var pr in config.Arch_OL) {
				LogicFctry.Inst.NewArch(pr.Key, pr.Value);
			}

			/* VILL_OL */
			foreach (var pr in config.Vill_OL) {
				LogicFctry.Inst.NewVill(pr.Key, pr.Value);
			}

			/* UNLOCK_REPO */
			foreach (var rt in config.UnlockedRepo) {
				RepoMgr.Inst.UnlockRepo(rt);
			}

			/* STRARTING_REPO */
			RepoMgr.Inst.AddRepo(config.StartingRepo);

			/* CREATE_LAYER */
			for (int i = 0; i < config.PosLayers.Count; ++i) {
				var type = config.PosLayers[i];
				LogicFctry.Inst.NewLayer(type, i);
			}
			for (int i = 0; i < config.NegLayers.Count; ++i) {
				var type = config.NegLayers[i];
				LogicFctry.Inst.NewLayer(type, - i - 1);
			}
		}
	}
}