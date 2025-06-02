using System.Collections.Generic;
using GameLogic.Model.Element.Vill;
using UnityEngine;

namespace GameLogic.Controller
{
	public class DestroyVillCmd : CommandBase {

		private readonly VillLogicBase _vill;

		public DestroyVillCmd(List<string> args) : base(args) {
			if (args.Count != ArgCount) { return; }
			if (!ParamConverter.TryDefaultConvert<ulong>(args[0], out var vID)) {
				Debug.Log($"<<{CmdTitle}>> 参数VillID错误: 无法解析参数{args[0]}");
			} else {
				_vill = WorldMgr.Inst.FindVill(vID);
			}
		}

		public override int ArgCount => 1;
		public override string CmdTitle => "销毁村民";
		public override string Description => $"村民ID:{_vill.ID} 被销毁";
		public override string FailReason => "村民不存在";

		public override bool Check() {
			return _vill != null;
		}

		public override void Execute() {
			_vill.LogicDestroy();
		}
	}
}