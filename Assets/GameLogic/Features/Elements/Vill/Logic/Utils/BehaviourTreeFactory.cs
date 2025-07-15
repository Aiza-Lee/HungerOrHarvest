using System.Linq;
using GameLogic.Common.DataTypes;
using GameLogic.Common.Logic;
using GameLogic.Common.Logic.Utils;
using GameLogic.Common.Utils;
using GameLogic.Features.Destroyer;
using GameLogic.Features.Elements.Arch;
using GameLogic.Features.Elements.Vill;
using GameLogic.Features.Repo;
using GameLogic.Features.TickCounter;
using GameLogic.Features.WorldEdge;
using GameLogic.World;
using NSFrame.BehaviourTree;
using UnityEngine;

namespace GameLogic.Features.Vill {
	public static class BehaviourTreeFactory {
		private static readonly bool EnableDebugLogs = false;
		public static BehaviourTree<VillAiBlackboard> CreateVillBehaviourTree(VillAiBlackboard bb) {
			var builder = new BehaviourTreeBuilder<VillAiBlackboard>();
			{ // 创建Normal村民行为树
				var tree = builder
					.Blackboard(bb)
					.Selector()

						.Sequence()
							.Condition(bb => bb.Entity.GetComponent<VillVitalityComponent>().Die == true)
							.Selector()
								.Action(GetRouteForDie)
								.Action(Move)
								.Action(Die)
							.End()
						.End()

						.Selector()
							.Sequence()
								.Condition(bb => bb.World.GetResource<TickCounterResource>().IsNightLastTick == true)
								.Selector()
									.Sequence()
										.Condition(bb => bb.VitPercent >= bb.VitConfig.LowVitThreshold)
										.Action(ExitDying)
									.End()
									.Sequence()
										.Condition(bb => bb.VitalityComp.IsDying == true)
										.Action(EnterDie)
									.End()
								.End()
								.Action(LeaveArch)
							.End()
							.Sequence()
								.Condition(bb => bb.World.GetResource<TickCounterResource>().IsDayLastTick == true)
							// 进入夜晚时的逻辑
							.End()
						.End()

						.Selector()
							.Sequence()
								.Condition(bb => bb.World.GetResource<TickCounterResource>().IsDay == false)
								.Selector()
									.Action(LeaveArch)
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
								.Condition(bb => bb.World.GetResource<TickCounterResource>().IsDay == true)
								.Selector()

									.Sequence()
										.Condition(bb => bb.VitalityComp.IsDying == true)
										.Selector()
											.Action(GetRouteForRandom)
											.Action(Move)
										.End()
									.End()

									.Sequence()
										.Condition(bb => bb.VitalityComp.AtRecoverMode == true)
										.Selector()
											.Action(LeaveArch)
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
										.Condition(bb => bb.VitPercent < bb.VitConfig.LowVitThreshold)
										.Action(UseRecoverChance)
										.Condition(CheckFoodEnoughForRecover)
										.Action(EnterRecoverMode)
									.End()

									.Selector()
										.Sequence()
											.Condition(bb => bb.Entity.GetComponent<BondToArchComponent>().WorkArchGid != 0)
											.Selector()
												.Action(GetRouteForWorkArch)
												.Action(Move)
												.Action(EnterWorkArch)
												.Action(WorkProd)
											.End()
										.End()
										.Sequence()
											.Condition(bb => bb.Entity.GetComponent<BondToArchComponent>().WorkArchGid == 0)
											.Selector()
												.Action(LeaveArch)
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
				return tree;
			}
		}

		private static NodeStatus WorkProd(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("WorkProd called");
			#endif
			var arch = VillQueryAPI.GetWorkArchGid(bb.Entity).ToEntity();
			var archLevel = ArchQueryAPI.GetLevel(arch);
			var lConfig = ArchQueryAPI.GetLevelConfig(arch);

			var vitCost = lConfig.VitConsPerTick;
			var cons = lConfig.ExtraConsPerOnePerTick.ToNewEtList();
			var prod = lConfig.ExtraProdPerOnePerTick.ToNewEtList();
			var expGain = lConfig.AddExpPerTick.ToNewEtList();

			if (bb.IsHungry) {
				cons.Change(val => val * (1f - bb.VitConfig.HungryProdLoss));
				prod.Change(val => val * (1f - bb.VitConfig.HungryProdLoss));
				expGain.Change(val => val * (1f - bb.VitConfig.HungryProdLoss));
			}

			VillRequestAPI.RequestProd(bb.Entity, cons, prod, expGain, vitCost);
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus Sleep(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("Sleep called");
			#endif
			// todo: 睡觉逻辑,目前的实现应该没有问题，但是可能是个bug
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus Recover(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("Recover called");
			#endif
			if (bb.VitPercent >= 1f) {
				return NodeStatus.FAILURE;
			}
			var cons = bb.VitConfig.FoodConsPerTickWhenRecover;
			if (RepoQueryAPI.GetRepoAmount(RepoType.Food) >= cons) {
				VillRequestAPI.RequestConsFoodRecoverVit(bb.Entity, cons, bb.VitConfig.VitPerFood * cons);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE;
		}
		private static NodeStatus RecoverTillWork(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("RecoverTillWork called");
			#endif
			if (bb.VitPercent >= bb.VitConfig.RecoverVitThreshold) {
				return NodeStatus.FAILURE; // 已经满了
			}
			var cons = bb.VitConfig.FoodConsPerTickWhenRecover;
			if (RepoQueryAPI.GetRepoAmount(RepoType.Food) >= cons) {
				VillRequestAPI.RequestConsFoodRecoverVit(bb.Entity, cons, bb.VitConfig.VitPerFood * cons);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE; // 无法恢复，食物不足
		}

		private static NodeStatus ExitRecoverMode(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("ExitRecoverMode called");
			#endif
			bb.VitalityComp.AtRecoverMode = false;
			return NodeStatus.SUCCESS;
		}
		private static NodeStatus EnterRecoverMode(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("EnterRecoverMode called");
			#endif
			bb.VitalityComp.AtRecoverMode = true;
			return NodeStatus.SUCCESS;
		}
		private static NodeStatus UseRecoverChance(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("UseRecoverChance called");
			#endif
			if (bb.VitalityComp.RecoverChances <= 0) {
				return NodeStatus.FAILURE; // 没有恢复机会了
			}
			bb.VitalityComp.RecoverChances--;
			return NodeStatus.SUCCESS; // 成功使用一次恢复机会
		}
		private static bool CheckFoodEnoughForRecover(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("CheckFoodEnoughForRecover called");
			#endif
			var vitDemand = bb.VitConfig.MaxVit - bb.VitalityComp.Vit;
			var foodDemand = vitDemand / bb.VitConfig.VitPerFood;
			return RepoQueryAPI.GetRepoAmount(RepoType.Food) >= foodDemand;
		}

		private static NodeStatus EnterHome(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("EnterHome called");
			#endif
			var homeGid = VillQueryAPI.GetHomeArchGid(bb.Entity);
			if (homeGid == 0) return NodeStatus.FAILURE; // 没有家，无法进入

			if (!VillQueryAPI.GetInArchGid(bb.Entity).Equals(homeGid)) {
				VillRequestAPI.RequestEnterHomeArch(bb.Entity);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE; // 已经在家中
		}

		private static NodeStatus EnterWorkArch(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("EnterWorkArch called");
			#endif
			var workArchGid = VillQueryAPI.GetWorkArchGid(bb.Entity);
			if (workArchGid == 0) return NodeStatus.FAILURE;

			if (!VillQueryAPI.GetInArchGid(bb.Entity).Equals(workArchGid)) {
				VillRequestAPI.RequestEnterWorkArch(bb.Entity);
				return NodeStatus.SUCCESS;
			}
			return NodeStatus.FAILURE; // 已经在工作建筑中
		}

		private static NodeStatus LeaveArch(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("LeaveArch called");
			#endif

			var archGid = VillQueryAPI.GetInArchGid(bb.Entity);
			if (archGid == 0) return NodeStatus.FAILURE;

			VillRequestAPI.RequestLeaveArch(bb.Entity);
			return NodeStatus.SUCCESS; // 成功离开建筑
		}

		private static NodeStatus EnterDie(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("EnterDie called");
			#endif
			bb.VitalityComp.Die = true;
			bb.VitalityComp.IsDirty = true;
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus ExitDying(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("ExitDying called");
			#endif
			bb.VitalityComp.IsDying = false;
			bb.VitalityComp.IsDirty = true;
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus Move(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("Move called");
			#endif
			var tick = bb.World.GetResource<TickCounterResource>();
			var routeComp = bb.RoutePlanComp;
			var moveComp = bb.MoveComp;

			if (routeComp.MoveRoute == null || routeComp.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 没有可用的移动路线
			}
			if (routeComp.CurMoveIndex >= routeComp.MoveRoute.Count) {
				return NodeStatus.FAILURE; // 已经到达目的地
			}
			if (tick.TickCount - moveComp.LastMoveTick < bb.Config.TicksPerCoord) {
				return NodeStatus.SUCCESS; // 还在移动中，等待下一次移动
			}
			moveComp.LastMoveTick = tick.TickCount;
			var cComp = bb.CoordComp;
			cComp.Coord += cComp.Coord.DirectionTo(routeComp.MoveRoute[routeComp.CurMoveIndex]);
			cComp.IsDirty = true;
			bb.SmoothPosStatComp.SetChangeInfo(bb.Config.NormalWalkChangeInfo);
			if (cComp.Coord == routeComp.MoveRoute[routeComp.CurMoveIndex]) {
				routeComp.CurMoveIndex++;
			}
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus Die(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("Die called");
			#endif
			var villDestroyRes = bb.World.GetResource<VillDestroyResource>();
			villDestroyRes.VillToDestroy.Add(bb.GidComp.Gid);
			return NodeStatus.SUCCESS; // 死亡处理完成
		}

		private static NodeStatus GetRouteForDie(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("GetRouteForDie called");
			#endif
			var villBondArch = bb.Entity.GetComponent<BondToArchComponent>();
			if (villBondArch.HomeArchGid == 0) {
				DestroyerAPI.DestroyVill(bb.Entity.GetGid());
				return NodeStatus.FAILURE;
			}
			var home = GameWorldMono.GidToEntity[villBondArch.HomeArchGid];
			var routeComp = bb.RoutePlanComp;
			if (routeComp.MoveRoute.Last() == home.GetComponent<OLComponent>().OL.ToCoord()) {
				return NodeStatus.FAILURE; // 已经规划好route了
			}

			routeComp.CurMoveIndex = 0;
			routeComp.MoveRoute = RouteGenerator.GetRoute(bb.CoordComp.Coord, home.GetComponent<OLComponent>().OL.ToCoord());
			if (routeComp.MoveRoute == null || routeComp.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 无法获取路线
			}
			return NodeStatus.SUCCESS;
		}
		private static NodeStatus GetRouteForHome(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("GetRouteForHome called");
			#endif
			var villBondArch = bb.Entity.GetComponent<BondToArchComponent>();
			if (villBondArch.HomeArchGid == 0) {
				return NodeStatus.FAILURE; // 没有家，无法执行
			}
			var home = GameWorldMono.GidToEntity[villBondArch.HomeArchGid];
			var routeComp = bb.RoutePlanComp;
			if (routeComp.MoveRoute.Last() == home.GetComponent<OLComponent>().OL.ToCoord()) {
				return NodeStatus.FAILURE; // 已经规划好route了
			}

			routeComp.CurMoveIndex = 0;
			routeComp.MoveRoute = RouteGenerator.GetRoute(bb.CoordComp.Coord, home.GetComponent<OLComponent>().OL.ToCoord());
			if (routeComp.MoveRoute == null || routeComp.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 无法获取路线
			}
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus GetRouteForRandom(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("GetRouteForRandom called");
			#endif
			var routeComp = bb.RoutePlanComp;
			if (routeComp.MoveRoute.Count > 0 && routeComp.MoveRoute.Last() != bb.CoordComp.Coord) {
				return NodeStatus.FAILURE;
			}
			routeComp.CurMoveIndex = 0;
			routeComp.MoveRoute = RouteGenerator.GetRoute(bb.CoordComp.Coord, WorldEdgeAPI.GetRandomCoordInWorldEdge());
			if (routeComp.MoveRoute == null || routeComp.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 无法获取路线
			}
			return NodeStatus.SUCCESS;
		}

		private static NodeStatus GetRouteForWorkArch(VillAiBlackboard bb) {
			#if UNITY_EDITOR
				if (EnableDebugLogs) Debug.Log("GetRouteForWorkArch called");
			#endif
			var villBondArch = bb.Entity.GetComponent<BondToArchComponent>();
			if (villBondArch.WorkArchGid == 0) {
				return NodeStatus.FAILURE; // 没有家，无法执行
			}
			var workArch = GameWorldMono.GidToEntity[villBondArch.WorkArchGid];
			var routeComp = bb.RoutePlanComp;
			if (routeComp.MoveRoute.Last() == workArch.GetComponent<OLComponent>().OL.ToCoord()) {
				return NodeStatus.FAILURE; // 已经规划好route了
			}

			routeComp.CurMoveIndex = 0;
			routeComp.MoveRoute = RouteGenerator.GetRoute(bb.CoordComp.Coord, workArch.GetComponent<OLComponent>().OL.ToCoord());
			if (routeComp.MoveRoute == null || routeComp.MoveRoute.Count == 0) {
				return NodeStatus.FAILURE; // 无法获取路线
			}
			return NodeStatus.SUCCESS;
		}
	}
}