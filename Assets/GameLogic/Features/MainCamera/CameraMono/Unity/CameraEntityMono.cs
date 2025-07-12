using System.Collections.Generic;
using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Common.View;
using GameLogic.Features.ClearWorld;
using GameLogic.World;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.MainCamera {
	public class CameraEntityMono : EntityMono, IWorldClearRespondable {
		public void RespondWorldClear() {
			// 在世界清除时，直接将摄像机位置移动到世界中心点
			var entity = GameWorldMono.MainWorld.GetEntity(EntityId);
			var stat = entity.GetComponent<SmoothPositionStatComponent>();

			var targetOL = ConstMgr.WORLD_CENTER_OL;
			targetOL.LYR -= 1;
			var target = targetOL.ToVec3DefaultY();
			target.y = ConstMgr.DEFAULT_CAMERA_HEIGHT;

			stat.SetChangeInfo(new(0, ChangeCurveType.Directive, false)).StartAChange(entity, target);
		}

		protected override IEnumerable<IComponent> GetAllComponents(Entity entity) {
			yield return entity.GetComponent<MainCameraComponent>();
			yield return entity.GetComponent<TransformComponent>();
		}

		void Start() {
			WorldClearRegistry.Inst.Register(this);

			var entity = GameWorldMono.MainWorld.CreateEntity();
			entity
				.AddComponent<TransformComponent>(new() {
					LocalPosition = new(0, ConstMgr.DEFAULT_CAMERA_HEIGHT, 0)
				})
				.AddComponent<MainCameraComponent>()
				.AddComponent<CameraComponent>()
				.AddComponent<SmoothPositionStatComponent>(new(new ChangeInfo()))
				.AddComponent<SmoothCameraSizeStatComponent>(new(new ChangeInfo()))
				.AddComponent<IgnoreWorldClearComponent>()
			;
			SetEntity(entity);
		}
	}
}