using System.Collections.Generic;
using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Common.View;
using GameLogic.Features.ClearWorld;
using GameLogic.World;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Features.MainCamera {
	public class CameraEntityMono : EntityMono, IWorldClearRespondable {
		public void RespondWorldClear() {
			// 在世界清除时，直接将摄像机位置移动到世界中心点
			var entity = GameWorldMono.MainWorld.GetEntity(EntityId);
			var stat = entity.GetComponent<SmoothChangeStatComponent>();

			var targetOL = ConstMgr.WORLD_CENTER_OL;
			targetOL.LYR -= 1;
			var target = targetOL.ToVec3DefaultY();

			var info = SmoothChangeInfo.NewDirectInfo(ChangeTargetType.Transform_Position, new(target));
			stat.AddNewChange(info);
		}

		protected override IEnumerable<IComponent> GetAllComponents(Entity entity) {
			yield return entity.GetComponent<MainCameraComponent>();
			yield return entity.GetComponent<TransformComponent>();
		}

		void Start() {
			WorldClearRegistry.Inst.Register(this);

			var entity = GameWorldMono.MainWorld.CreateEntity();
			entity
				.AddComponent<TransformComponent>()
				.AddComponent<MainCameraComponent>()
				.AddComponent<CameraComponent>()
				.AddComponent<SmoothChangeStatComponent>()
				.AddComponent<IgnoreWorldClearComponent>()
			;
			SetEntity(entity);
		}
	}
}