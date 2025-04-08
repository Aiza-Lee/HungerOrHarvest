using System.Collections.Generic;
using GameLogic.View;

namespace GameLogic.Controller
{
	public class CameraFreeViewCmd : CommandBase {
		public CameraFreeViewCmd(List<string> args) : base(args) {}

		public override int ArgCount => 0;

		public override string CmdTitle => "镜头自由视角";
		public override string Description => "镜头自由视角";
		public override string FailReason => "Never Fail";

		public override bool Check() {
			return true;
		}

		public override void Execute() {
			WorldCameraFocus.Inst.FreeView();
		}
	}
}