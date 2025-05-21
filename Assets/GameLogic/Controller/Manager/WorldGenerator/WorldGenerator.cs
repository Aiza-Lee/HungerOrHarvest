using System.Collections.Generic;
using GameLogic.Model.Factory;
using GameLogic.View;
using NSFrame;
using UnityEngine;

namespace GameLogic.Controller
{
	public sealed class WorldGenerator : MonoSingleton<WorldGenerator> {

		[SerializeField] private PreSetConfig _defaultConfig;
		[SerializeField] private List<PreSetConfig> _randomConfigs;

		private void GenerateImpl(PreSetConfig config, string worldName) {
			var saveInfo = SaveSystem.CreateSaveFile(worldName);
			GameModelMgr.Inst.SetSaveInfo(saveInfo);
			GameViewMgr.Inst.SetSaveInfo(saveInfo);

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
			RepoMgr.Inst.AddRepoFromSave(config.StartingRepo);

			/* CREATE_LAYER */
			for (int i = 0; i < config.PosLayers.Count; ++i) {
				var type = config.PosLayers[i];
				LogicFctry.Inst.NewLayer(type, i);
			}
			for (int i = 0; i < config.NegLayers.Count; ++i) {
				var type = config.NegLayers[i];
				LogicFctry.Inst.NewLayer(type, - i - 1);
			}

			/* WORLD_BASE_INFO */
			WorldBaseInfoMgr.Inst.SetWorldHashTag();
			WorldBaseInfoMgr.Inst.WorldName = worldName;

			
			GameModelMgr.Inst.SaveGame();
			GameViewMgr.Inst.SaveGame();
			// 提示UI这个存档是第一个存档，方便显示不同的文本
			var baseSave = SaveSystem.LoadObject<WorldBaseInfoMgrSave>(saveInfo);
			baseSave.StartingSave = true;
			SaveSystem.SaveObject(saveInfo, baseSave);
		}

		public void GenerateDefaultWorld() {
			GenerateImpl(_defaultConfig, "Default World");
			WorldBaseInfoMgr.Inst.WorldName = "Default World";
		}
		public void GenerateRandomWorld(string worldName) {
			var rId = Random.Range(0, _randomConfigs.Count);
			GenerateImpl(_randomConfigs[rId], worldName);
			WorldBaseInfoMgr.Inst.WorldName = worldName;
		}
	}
}