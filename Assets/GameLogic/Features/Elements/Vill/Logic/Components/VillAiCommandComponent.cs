using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using NsEcsFrame.Core;

namespace GameLogic.Features.Vill {
	// 移除原有 VillAiCommand 枚举，改为每个命令一个派生类

	public abstract class AiCommandBase { }

	public class AiCommandMoveToTarget : AiCommandBase {
		public Coord TargetCoord;
		public AiCommandMoveToTarget(Coord targetCoord) { TargetCoord = targetCoord; }
	}

	public class AiCommandGetRoute : AiCommandBase {
		public enum RouteType {
			Home,
			WorkArch,
			Random,
			Die
		}
		public RouteType Type;
		public AiCommandGetRoute(RouteType type) { Type = type; }
	}

	public class AiCommandEnterHome : AiCommandBase { }
	public class AiCommandLeaveHome : AiCommandBase { }

	public class AiCommandEnterWorkArch : AiCommandBase { }
	public class AiCommandLeaveWorkArch : AiCommandBase { }

	public class AiCommandEnterRecoverMode : AiCommandBase { }
	public class AiCommandExitRecoverMode : AiCommandBase { }

	public class AiCommandEnterDie : AiCommandBase { }
	public class AiCommandExitDying : AiCommandBase { }

	public class AiCommandWorkProd : AiCommandBase { }
	public class AiCommandRecover : AiCommandBase { }
	public class AiCommandRecoverTillWork : AiCommandBase { }
	public class AiCommandSleep : AiCommandBase { }
	public class AiCommandDayCons : AiCommandBase { }
	public class AiCommandUseRecoverChance : AiCommandBase { }
	public class AiCommandCheckFoodEnoughForRecover : AiCommandBase { }
	public class AiCommandDie : AiCommandBase { }

	/// <summary>
	/// VillAiCommandComponent 用于存储村民的AI命令列表。
	/// </summary>
	public class VillAiCommandComponent : IComponent {
		public List<AiCommandBase> Commands = new();
	}
}