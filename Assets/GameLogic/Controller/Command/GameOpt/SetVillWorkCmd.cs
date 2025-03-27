using UnityEngine;

namespace GameLogic
{
	/// <summary>
	/// Args: ulong, ulong
	/// </summary>
	public class SetVillWorkCmd : CommandBase {
		private readonly VillLogicBase _vill;
		private readonly ArchLogicBase _arch;
		private string _failReason;

		public SetVillWorkCmd(string[] args) : base(args) {
			if (args.Length != ArgCount) { return; }
			if (!ParamConverter.TryDefaultConvert<ulong>(args[0], out var vID)) {
				Debug.Log($"<<{CmdTitle}>> 参数VillID错误: 无法解析参数{args[0]}");
			} else {
				_vill = WorldMgr.Inst.FindVill(vID);
			}
			if (!ParamConverter.TryDefaultConvert<ulong>(args[1], out var aID)) {
				Debug.Log($"<<{CmdTitle}>> 参数ArchID错误: 无法解析参数{args[1]}");
			} else {
				_arch =  WorldMgr.Inst.FindArch(aID);
			}
		}

		public override string CmdTitle => "设置村民Work";
		public override string Description => $"建筑ID:{_arch.ID}  建筑类型:{_arch.ArchType}  村民ID:{_vill.ID}  新状态:Work";
		public override string FailReason => _failReason;
		public override int ArgCount => 2;


		public override bool Check() {
			if (_vill == null) {
				_failReason = "村民不存在";
				return false;
			}
			if (_arch == null) {
				_failReason = "建筑不存在";
				return false;
			}
			if (_arch.CheckCapacity()) {
				return true;
			}
			_failReason = "建筑已满";
			return false;
		}

		public override void Execute() {
			_vill.SetWork(_arch.ID);
			_arch.AddVill(_vill);
		}

	}

}