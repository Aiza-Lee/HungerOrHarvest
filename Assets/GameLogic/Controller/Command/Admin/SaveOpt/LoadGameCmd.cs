using System.Collections.Generic;
using GameLogic.Model.Mgr;
using GameLogic.View;
using NSFrame;

namespace GameLogic.Controller
{
	/// <summary>
	/// 加载当前saveInfo的存档
	/// </summary>
	public class LoadGameCmd : CommandBase {
		public LoadGameCmd(List<string> args) : base(args) { }

		public override int ArgCount => 0;

		public override string CmdTitle => "加载存档";
		public override string Description => "加载当前saveInfo的存档";
		public override string FailReason => "saveInfo未正确注入";

		public override bool Check() {
			return GameViewMgr.Inst.SaveInfoSeted() && GameModelMgr.Inst.SaveInfoSeted();
		}

		public override void Execute() {
			// note: 加载每日结束时候的存档会导致再一次触发那一天结束的自动存档，所以在这里标记，而跳过因为加载而产生的不必要的自动存档。
			// note: 然而如果当前存档是初始存档，则不应该跳过自动存档
			if (SaveSystem.LoadObject<WorldBaseInfoMgrSave>(GameModelMgr.Inst.CurSaveInfo).StartingSave) {
				LogicTimeMgr.Inst.IsLoadingNotStartingSave = false;
			} else {
				LogicTimeMgr.Inst.IsLoadingNotStartingSave = true;
			}
			
			// note: ViewMgr先加载, 因为ModelMgr加载时会触发ViewMgr的事件
			GameViewMgr.Inst.LoadGame();
			GameModelMgr.Inst.LoadGame();
		}
	}
}