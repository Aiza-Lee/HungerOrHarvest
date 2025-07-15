using System.Collections.Generic;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;
using System.Reflection;

namespace GameLogic.World {
	public class GameWorldMono : WorldBehaviour {
		public static Dictionary<ulong, Entity> GidToEntity = new();

		protected override void RegisterSystems() {
			foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
				if (typeof(ISystem).IsAssignableFrom(type) && !type.IsAbstract) {
					World.SystemManager.RegisterSystem(type);
				}
			}
		}
		protected override void RegisterResources() {
			foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
				if (typeof(IResource).IsAssignableFrom(type) && !type.IsAbstract) {
					World.InsertResource(type);
				}
			}
		}
	}
}