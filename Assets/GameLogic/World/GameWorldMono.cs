using GameLogic.Common.View;
using GameLogic.Test;
using NsEcsFrame.Components;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.World {
	public class GameWorldMono : WorldBehaviour {

		protected override void RegisterSystems() {
			World.SystemManager
				.RegisterSystem<SmoothChangeSystem>()
				.RegisterSystem<TransformSyncSystem>();
		}
		protected override void RegisterResources() {
			World.InsertResource(new ChangeCurveResource());
		}

		public MoveEntityMono TestMono;

		void Start() {
			World.InsertResource(new ChangeCurveResource());

			var entity = World.CreateEntity();
			entity.AddComponent<SpriteRendererComponent>();
			entity.AddComponent<TransformComponent>();

			var smoothChangeInfo = new SmoothChangeInfo {
				ChangeTargetType = ChangeTargetType.Transform_Position,
				ChangeCurveType = ChangeCurveType.Linear,
				StartValue = new SmoothValue(new Vector3(0, 0, 0)),
				TargetValue = new SmoothValue(new Vector3(5, 0, 0)),
				TotalTime = 2f,
				IsLogicTime = true,
			};
			entity.AddComponent(new SmoothChangeStatComponent() {
				SmoothChangeInfos = new System.Collections.Generic.List<SmoothChangeInfo> { smoothChangeInfo }
			});
			TestMono.SetEntity(entity);


		}
	}
}