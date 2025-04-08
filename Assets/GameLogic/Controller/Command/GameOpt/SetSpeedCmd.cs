using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Controller
{
	/// <summary>
	/// Args: float
	/// </summary>
	public class SetSpeedCmd : CommandBase {
		private readonly float _speed;
		private readonly bool _inited;

		public SetSpeedCmd(List<string> args) : base(args) {
			_inited = true;
			if (args.Count != ArgCount) { _inited = false; return; }
			if (!ParamConverter.TryDefaultConvert(args[0], out _speed)) {
				_inited = false;
				Debug.Log($"<<{CmdTitle}>> 参数Speed错误: 无法解析参数{args[0]}");
			}
		}

		public override string CmdTitle => "设置时间流逝速度";
		public override string Description => $"设置为:{_speed}";
		public override string FailReason => string.Empty;
		public override int ArgCount => 1;


		public override bool Check() => _inited;

		public override void Execute() {
			TickTrigger.Inst.Speed = _speed;
		}

	}
}