using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.Controller
{
	/// <summary>
	/// Args: VillType, OL
	/// </summary>
	public class CreateVillCmd : CommandBase {

		private readonly VillType _villType;
		private readonly OL _ol;
		private readonly bool _inited;

		public CreateVillCmd(List<string> args) : base(args) {
			_inited = true;
			if (args.Count != ArgCount) { _inited = false; return; }
			if (!ParamConverter.TryConvertToEnum(args[0], out _villType)) {
				_inited = false;
				Debug.Log($"<<{CmdTitle}>> 参数VillType错误: 无法解析参数{args[0]}");
			}
			if (!ParamConverter.TryConvertToOL(args[1], out _ol)) {
				_inited = false;
				Debug.Log($"<<{CmdTitle}>> 参数OL错误: 无法解析参数{args[1]}");
			}
		}

		public override string CmdTitle => "生成村民";
		public override string Description => $"类型:{_villType}  OL:{_ol} Coord:{_ol.ToCoord()}";
		public override string FailReason => string.Empty;
		public override int ArgCount => 2;


		public override bool Check() {
			return _inited;
		}
		public override void Execute() {
			LogicFctry.Inst.NewVill(_villType, _ol);
		}
	}

}