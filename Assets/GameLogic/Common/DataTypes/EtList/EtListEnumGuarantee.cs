using System;
using System.Collections.Generic;

namespace GameLogic.Common.DataTypes {
	/// <summary>
	/// 负责为枚举类型做离散化的保证。
	/// 该类确保枚举类型在使用时能够被正确地转换为整数
	/// </summary>
	public static class EtListEnumGuarantee {

		private static List<int> GetNewMap<E>() where E : Enum {
			int mx = 0;
			var values = Enum.GetValues(typeof(E));
			foreach (var value in values) { mx = Math.Max(mx, (int) value); }
			var map = new List<int>(new int[mx + 1]);
			for (int i = 0; i < values.Length; i++) {
				map[Convert.ToInt32(values.GetValue(i))] = i;
			}
			return map;
		}
		private static readonly Dictionary<Type, List<int>> _TypeMap = new() {
			{ typeof(RepoType), GetNewMap<RepoType>() },
			{ typeof(JobType), GetNewMap<JobType>() },
			{ typeof(LayerType), GetNewMap<LayerType>() },
			{ typeof(VillType), GetNewMap<VillType>() },
			{ typeof(ArchType), GetNewMap<ArchType>() },
		};

		public static int ToListIndex<E>(this E e) where E : Enum {
			if (_TypeMap.TryGetValue(typeof(E), out var map)) {
				return map[Convert.ToInt32(e)];
			}
			throw new ArgumentException($"Enum type {typeof(E)} is not supported.");
		}

	}
}