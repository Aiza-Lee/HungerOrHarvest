using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
	public class CameraFocusVillCmd : CommandBase {
		private readonly ulong _id;
		private VillViewBase _view;

		public CameraFocusVillCmd(List<string> args) : base(args) {
			if (!ParamConverter.TryDefaultConvert(args[0], out _id)) {
				Debug.Log($"<<{CmdTitle}>> 参数VillID错误: 无法解析参数{args[0]}");
			}
		}

		public override int ArgCount => 1;

		public override string CmdTitle => "镜头聚焦村民";

		public override string Description => $"聚焦村民ID:{_id}";

		public override string FailReason => $"找不到村民的View. ID:{_id}";

		public override bool Check() {
			return WorldViewMgr.Inst.TryGetVillView(_id, out _view);
		}

		public override void Execute() {
			WorldCameraMgr.Inst.FocusOn(_view.transform);
		}
	}
}