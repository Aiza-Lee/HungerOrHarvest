using System.Collections.Generic;
using GameLogic.View;
using UnityEngine;

namespace GameLogic.Controller
{
	public class CameraFocusArchCmd : CommandBase {
		private readonly ulong _id;
		private ArchViewBase _view;

		public CameraFocusArchCmd(List<string> args) : base(args) {
			if (!ParamConverter.TryDefaultConvert(args[0], out _id)) {
				Debug.Log($"<<{CmdTitle}>> 参数ArchID错误: 无法解析参数{args[0]}");
			}
		}

		public override int ArgCount => 1;

		public override string CmdTitle => "镜头聚焦建筑";
		public override string Description => $"聚焦建筑ID:{_id}";
		public override string FailReason => "找不到建筑的View";

		public override bool Check() {
			return WorldViewMgr.Inst.TryGetArchView(_id, out _view);
		}

		public override void Execute() {
			WorldCameraFocus.Inst.FocusOn(_view.transform);
		}
	}
}