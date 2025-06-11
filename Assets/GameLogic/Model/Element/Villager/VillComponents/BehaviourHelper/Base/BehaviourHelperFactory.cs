using System.Linq;
using GameLogic.Model.Mgr;
using NSFrame.BehaviourTree;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// 工厂类，用于创建行为帮助器实例
	/// <para>不需要派生新节点的leaf的逻辑也写在这里</para>
	/// </summary>
	public static class BehaviourHelperFactory {
#if UNITY_EDITOR
		public static bool DebugLogEnabled = false;
#endif
		private static MyBlackBoardSave DefaultBlackBoardSave => new() {
			MoveRoute = null,
			CurMoveIndex = 0,
			LastMoveTime = 0,
			RecoverChance = ConfigMgr.Config.VitConfig.RecoverChancePerDay,
			RecoverMode = false,
			IsDying = false,
			Die = false,
			LastTickHomeID = 0,
			LastTickBondedWorkArchID = 0,
			LastTickInDay = LogicTimeMgr.Inst.IsDay
		};

		public static BehaviourHelper CreateBehaviourHelper(LogicImpler impler) {
			var builder = new BehaviourTreeBuilder<MyBlackboard>();
			var blackboard = new MyBlackboard(impler);
			blackboard.InitFromSave(DefaultBlackBoardSave);
			var tree = builder
				.Blackboard(blackboard)
				.Selector()

					.Sequence()
						.Condition(bb => bb.Die == true)
						.Selector()
							.Action(GetRouteForDie)
							.Action(Move)
							.Action(Die)
						.End()
					.End()

					.Selector()
						.Sequence()
							.Condition(bb => bb.NightToDay == true)
							.Action(ResetWhenDayStart)
							.Action(LeaveHome)
							.Selector()
								.Sequence()
									.Condition(bb => bb.CurVitProportion >= bb.LowVitThreshold)
									.Action(ExitDying)
								.End()
								.Sequence()
									.Condition(bb => bb.IsDying == true)
									.Action(EnterDie)
								.End()
							.End()
						.End()
						.Sequence()
							.Condition(bb => bb.DayToNight == true)
						// 进入夜晚时的逻辑
						.End()
					.End()

					.Selector()
						.Sequence()
							.Condition(bb => bb.InDay == false)
							.Selector()
								.Action(LeaveWorkArch)
								.Selector()
									.Action(GetRouteForHome)
									.Action(Move)
									.Action(EnterHome)
									.Action(Recover)
									.Action(Sleep)
								.End()
							.End()
						.End()
						.Sequence()
							.Condition(bb => bb.InDay == true)
							.Action(DayCons)
							.Selector()

								.Sequence()
									.Condition(bb => bb.IsDying == true)
									.Selector()
										.Action(GetRouteForRandom)
										.Action(Move)
									.End()
								.End()

								.Sequence()
									.Condition(bb => bb.RecoverMode == true)
									.Selector()
										.Action(LeaveWorkArch)
										.Selector()
											.Action(GetRouteForHome)
											.Action(Move)
											.Action(EnterHome)
											.Action(RecoverTillWork)
											.Action(ExitRecoverMode)
										.End()
									.End()
								.End()

								.Sequence()
									.Condition(bb => bb.CurVitProportion < bb.LowVitThreshold)
									.Action(UseRecoverChance)
									.Condition(CheckFoodEnoughForRecover)
									.Action(EnterRecoverMode)
								.End()

								.Selector()
									.Sequence()
										.Condition(bb => bb.BondArchHelper.BondedWorkArchID != 0)
										.Selector()
											.Action(GetRouteForWorkArch)
											.Action(Move)
											.Action(EnterWorkArch)
											.Action(WorkProd)
										.End()
									.End()
									.Sequence()
										.Condition(bb => bb.BondArchHelper.BondedWorkArchID == 0)
										.Selector()
											.Action(GetRouteForRandom)
											.Action(Move)
										.End()
									.End()
								.End()

							.End()
						.End()
					.End()
				.End()
				.Build();


			return new BehaviourHelper(impler, blackboard, tree);
		}

		private static NodeStatus WorkProd(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("WorkProd called");
#endif
			var workArch = WorldMgr.Inst.FindArch(bb.BondArchHelper.BondedWorkArchID);
			if (bb.IsHungry) {
				if (RepoMgr.Inst.TryCons(workArch.Lconfig.ExtraConsVelsPerOne, bb.RepoBuffHelper.ConsBuffs_F, new(bb.HungryProdLoss))) {
					RepoMgr.Inst.Prod(workArch.Lconfig.ExtraProdVelsPerOne, bb.RepoBuffHelper.ProdBuffs_F, new(-bb.HungryProdLoss));
					bb.ExpHelper.AddExp(workArch.Lconfig.ExpAdds.Change_New(val => val * (1f - bb.HungryProdLoss)));
					bb.VitHelper.TryConsVit(bb.TickDyingVitCons);
					return NodeStatus.SUCCESS; // 工作生产成功
				}
			}
			if (RepoMgr.Inst.TryCons(workArch.Lconfig.ExtraConsVelsPerOne, bb.RepoBuffHelper.ConsBuffs_F)) {
				RepoMgr.Inst.Prod(workArch.Lconfig.ExtraProdVelsPerOne, bb.RepoBuffHelper.ProdBuffs_F);
				bb.ExpHelper.AddExp(workArch.Lconfig.ExpAdds);
				bb.VitHelper.TryConsVit(bb.TickDyingVitCons);
				return NodeStatus.SUCCESS; // 工作生产成功
			}
			return NodeStatus.FAILURE;
		}

		private static NodeStatus ResetWhenDayStart(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("ResetWhenDayStart called");
#endif
			bb.RecoverChance = ConfigMgr.Config.VitConfig.RecoverChancePerDay;
			bb.RecoverMode = false;
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus DayCons(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("DayCons called");
#endif
			bb.VitHelper.TryConsVit(bb.TickDayVitCons);
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus Sleep(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("Sleep called");
#endif
			// todo: 睡觉逻辑,目前的实现应该没有问题，但是可能是个bug
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus Recover(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("Recover called");
#endif
			if (bb.CurVitProportion >= 1f) {
				return NodeStatus.FAILURE;
			}
			if (RepoMgr.Inst.TryCons(RepoType.Food, bb.TickFoodCons, bb.RepoBuffHelper.ConsBuffs_F[(int) RepoType.Food].Value)) {
				bb.VitHelper.AddVit(bb.VitPerFood * bb.TickFoodCons);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE; // 无法恢复，食物不足
		}
		private static NodeStatus RecoverTillWork(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("RecoverTillWork called");
#endif
			if (bb.CurVitProportion >= bb.RecoverVitThreshold) {
				return NodeStatus.FAILURE; // 已经满了
			}
			if (RepoMgr.Inst.TryCons(RepoType.Food, bb.TickFoodCons, bb.RepoBuffHelper.ConsBuffs_F[(int) RepoType.Food].Value)) {
				bb.VitHelper.AddVit(bb.VitPerFood * bb.TickFoodCons);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE; // 无法恢复，食物不足
		}

		private static NodeStatus ExitRecoverMode(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("ExitRecoverMode called");
#endif
			bb.RecoverMode = false;
			return NodeStatus.SUCCESS;
		}
		private static NodeStatus EnterRecoverMode(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("EnterRecoverMode called");
#endif
			bb.RecoverMode = true;
			return NodeStatus.SUCCESS;
		}
		private static NodeStatus UseRecoverChance(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("UseRecoverChance called");
#endif
			if (bb.RecoverChance <= 0) {
				return NodeStatus.FAILURE; // 没有恢复机会了
			}
			bb.RecoverChance--;
			return NodeStatus.SUCCESS; // 成功使用一次恢复机会
		}
		private static bool CheckFoodEnoughForRecover(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("CheckFoodEnoughForRecover called");
#endif
			var vitDemand = bb.MaxVit - bb.VitHelper.CurVit;
			var foodDemand = vitDemand / bb.VitPerFood;
			return RepoMgr.Inst.CheckRequest(RepoType.Food, foodDemand, bb.RepoBuffHelper.ConsBuffs_F[(int) RepoType.Food].Value);
		}

		private static NodeStatus EnterHome(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("EnterHome called");
#endif
			if (bb.BondArchHelper.HomeID == 0) {
				return NodeStatus.FAILURE; // 没有家，无法进入
			}
			var home = WorldMgr.Inst.FindArch(bb.BondArchHelper.HomeID);
			if (!home.InVillIDs_RO.Contains(bb.Impler.ID)) {
				home.VillArrive(bb.Impler.ID);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE; // 已经在家中
		}
		private static NodeStatus LeaveHome(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("LeaveHome called");
#endif
			if (bb.BondArchHelper.HomeID == 0) {
				return NodeStatus.FAILURE; // 没有家，无法离开
			}
			var home = WorldMgr.Inst.FindArch(bb.BondArchHelper.HomeID);
			if (home.InVillIDs_RO.Contains(bb.Impler.ID)) {
				home.VillLeave(bb.Impler.ID);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE; // 不在家中
		}

		private static NodeStatus EnterWorkArch(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("EnterWorkArch called");
#endif
			if (bb.BondArchHelper.BondedWorkArchID == 0) {
				return NodeStatus.FAILURE; // 没有工作建筑，无法进入
			}
			var workArch = WorldMgr.Inst.FindArch(bb.BondArchHelper.BondedWorkArchID);
			if (!workArch.InVillIDs_RO.Contains(bb.Impler.ID)) {
				workArch.VillArrive(bb.Impler.ID);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE; // 已经在工作建筑中
		}

		private static NodeStatus LeaveWorkArch(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("LeaveWorkArch called");
#endif
			if (bb.BondArchHelper.BondedWorkArchID == 0) {
				return NodeStatus.FAILURE;
			}
			var workArch = WorldMgr.Inst.FindArch(bb.BondArchHelper.BondedWorkArchID);
			if (workArch.InVillIDs_RO.Contains(bb.Impler.ID)) {
				workArch.VillLeave(bb.Impler.ID);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE;
		}

		private static NodeStatus EnterDie(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("EnterDie called");
#endif
			bb.Die = true;
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus ExitDying(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("ExitDying called");
#endif
			bb.IsDying = false;
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus Move(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("Move called");
#endif
			if (bb.MoveRoute == null || bb.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 没有可用的移动路线
			}
			if (bb.CurMoveIndex >= bb.MoveRoute.Count) {
				return NodeStatus.FAILURE; // 已经到达目的地
			}
			if (bb.LogicTime - bb.LastMoveTime < (ulong) (bb.IsDying ? bb.MOVE_INTERVAL_DYING : bb.MOVE_INTERVAL_NORMAL)) {
				return NodeStatus.SUCCESS; // 还在移动中，等待下一次移动
			}
			bb.LastMoveTime = bb.LogicTime;
			bb.Impler.Move(bb.Coord.DirectionTo(bb.MoveRoute[bb.CurMoveIndex]));
			if (bb.Impler.Coord == bb.MoveRoute[bb.CurMoveIndex]) {
				bb.CurMoveIndex++;
			}
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus Die(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("Die called");
#endif
			bb.Impler.LogicDestroy();
			return NodeStatus.SUCCESS; // 死亡处理完成
		}

		private static NodeStatus GetRouteForDie(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("GetRouteForDie called");
#endif
			// todo: 目前先定位到家
			var home = WorldMgr.Inst.FindArch(bb.BondArchHelper.HomeID);
			if (home == null) {
				return NodeStatus.FAILURE; // 家不存在，无法执行
			}
			if (bb.MoveRoute.Last() == home.Coord) {
				return NodeStatus.FAILURE; // 已经规划好route了
			}
			bb.CurMoveIndex = 0;
			bb.MoveRoute = RouteMgr.Inst.GetRoute(bb.Coord, home.Coord);
			if (bb.MoveRoute == null || bb.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 无法获取路线
			}
			return NodeStatus.SUCCESS;
		}
		private static NodeStatus GetRouteForHome(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("GetRouteForHome called");
#endif
			var home = WorldMgr.Inst.FindArch(bb.BondArchHelper.HomeID);
			if (home == null) {
				return NodeStatus.FAILURE; // 家不存在，无法执行
			}
			if (bb.MoveRoute.Last() == home.Coord) {
				return NodeStatus.FAILURE; // 已经规划好route了
			}
			bb.CurMoveIndex = 0;
			bb.MoveRoute = RouteMgr.Inst.GetRoute(bb.Coord, home.Coord);
			if (bb.MoveRoute == null || bb.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 无法获取路线
			}
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus GetRouteForRandom(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("GetRouteForRandom called");
#endif
			if (bb.MoveRoute != null && bb.MoveRoute.Count > 0 && bb.Coord != bb.MoveRoute.Last()) {
				return NodeStatus.FAILURE; // 已经有路线了，且未到达终点
			}
			var randomCoord = RouteMgr.Inst.GetRandomVillSpareCoord();
			bb.CurMoveIndex = 0;
			bb.MoveRoute = RouteMgr.Inst.GetRoute(bb.Coord, randomCoord);
			if (bb.MoveRoute == null || bb.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 无法获取路线
			}
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus GetRouteForWorkArch(MyBlackboard bb) {
#if UNITY_EDITOR
			if (DebugLogEnabled) UnityEngine.Debug.Log("GetRouteForWorkArch called");
#endif
			if (bb.BondArchHelper.BondedWorkArchID == 0) {
				return NodeStatus.FAILURE; // 没有工作建筑，无法获取路线
			}
			var workArch = WorldMgr.Inst.FindArch(bb.BondArchHelper.BondedWorkArchID);
			if (bb.MoveRoute.Last() == workArch.Coord) {
				return NodeStatus.FAILURE; // 已经规划好route了
			}
			bb.CurMoveIndex = 0;
			bb.MoveRoute = RouteMgr.Inst.GetRoute(bb.Coord, workArch.Coord);
			if (bb.MoveRoute == null || bb.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 无法获取路线
			}
			return NodeStatus.SUCCESS; // 成功获取路线
		}

	}
}