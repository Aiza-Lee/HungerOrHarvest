using GameLogic.Common.Logic;
using GameLogic.Common.UnityComponentsBridge;
using GameLogic.Features.Elements.Decorations;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Components;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using UnityEngine;

namespace GameLogic.Features.Generator {
	/// <summary>
	/// DecorationGeneratorSystem 负责生成装饰物
	/// </summary>
	public class DecorationGeneratorSystem : ISystem {
		public int Priority => 500;
		public bool Enabled { get; set; }

		private IWorld _world;

		public void Initialize(IWorld world) {
			_world = world;
			Enabled = true;
		}

		public void OnCreate() { }
		public void OnDestroy() { }
		public void OnLogicUpdate(float _) {
			var res = _world.GetResource<DecorationGeneratorResource>();
			if (res.DecorationDatas.Count > 0) {
				foreach (var data in res.DecorationDatas) {
					var entity = _world.CreateEntity()
									.AddComponent(new DecorationIdentityComp { Type = data.Type })
									.AddComponent(new TransformComponent() { LocalScale = data.Scale })
									.AddComponent(new CoordComponent { Coord = data.Position })
									.AddComponent(new SavedEntityComponent())
									.AddComponent(new SpriteRendererComponent());
					var prefab = DecorationAPI.GetRandomDecorationPrefab(data.Type);
					var go = GameObject.Instantiate(prefab);
					go.GetComponent<EntityMono>().SetEntity(entity);
				}
				res.DecorationDatas.Clear(); // 清空已处理的数据
			}
		}
		public void OnRenderUpdate(float _) { }
	} 
}