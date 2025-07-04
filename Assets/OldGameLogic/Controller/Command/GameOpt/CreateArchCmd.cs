using System.Collections.Generic;
using OldGameLogic.Model.Factory;
using OldGameLogic.Model.Mgr;
using UnityEngine;

namespace OldGameLogic.Controller
{
	/// <summary>
	/// Args: ArchType, OL
	/// </summary>
	public class CreateArchCmd : CommandBase {
		private readonly ArchType _archType;
		private readonly OL _ol;

		private string _failReason;

		public CreateArchCmd(List<string> args) : base(args) {
			if (args.Count != ArgCount) { return; }
			if (!ParamConverter.TryConvertToEnum(args[0], out _archType)) {
				Debug.Log($"<<{CmdTitle}>> 参数ArchType错误: 无法解析参数{args[0]}");
			}
			if (!ParamConverter.TryConvertToOL(args[1], out _ol)) {
				Debug.Log($"<<{CmdTitle}>> 参数OL错误: 无法解析参数{args[1]}");
			}
		}

		public override string CmdTitle => "建造建筑";
		public override string Description => $"类型:{_archType}  OL:{_ol} Coord:{_ol.ToCoord()}";
		public override string FailReason => _failReason;
		public override int ArgCount => 2;


		public override bool Check() {
			var config = ConfigMgr.Config.FindArchConfig(_archType);
			if (!RepoMgr.Inst.CheckRequest(config.ConstructCost)) {
				_failReason = "资源不足";
				return false;
			} else if (!_ol.CheckAvailableForArch()) {
				_failReason = $"位置{_ol}不合法";
				return false;
			}
			return true;
		}
		public override void Execute() {
			LogicFctry.Inst.NewArch(_archType, _ol);
		}
	}
}