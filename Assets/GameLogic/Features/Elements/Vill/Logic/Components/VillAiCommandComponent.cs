using System.Collections.Generic;
using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	public enum VillAiCommand {
		MoveToTarget,
		GetRouteForHome, GetRouteForWorkArch, GetRouteForRandom, GetRouteForDie,
		EnterHome, LeaveHome,
		EnterWorkArch, LeaveWorkArch,
		EnterRecoverMode, ExitRecoverMode,
		EnterDie,
		ExitDying,
		WorkProd,
		Recover, RecoverTillWork,
		Sleep,
		DayCons,
		UseRecoverChance,
		CheckFoodEnoughForRecover,
		Die
	}
	/// <summary>
	/// VillAiCommandComponent 用于存储村民的AI命令列表。
	/// </summary>
	public class VillAiCommandComponent : IComponent {
		public List<VillAiCommand> Commands = new();
	}
}