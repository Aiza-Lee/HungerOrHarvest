using System.Collections.Generic;
using System.Linq;
using NsEcsFrame.Core;
using NSFrame;
using UnityEditor;

namespace GameLogic.Test {

	public class SystemCounter {
		private class SystemClassRegistry {
			public List<string> SystemClassNames { get; set; } = new();
			public string Code;
			public string ToCode() {
				return string.Join("\n", SystemClassNames.Select(name => $".RegisterSystem<{name + "System"}>()"));
			}
		}

		// [InitializeOnLoadMethod]
		public static void CountSystems() {
			var systemType = typeof(ISystem);
			var assembly = typeof(SystemCounter).Assembly;
			var systemClassNames = assembly.GetTypes()
				.Where(t => systemType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract)
				.Select(t => t.Name.Replace("System", ""))
				.ToList();
			var registry = new SystemClassRegistry {
				SystemClassNames = systemClassNames
			};
			registry.Code = registry.ToCode();
			SaveSystem.SaveObject(SaveSystem.CreateSaveFile("Test"), registry);
		}
	}

	
}