using System.Collections.Generic;
using GameLogic.Common.DataTypes;
using GameLogic.Features.Elements.Decorations;
using GameLogic.Features.WorldDataManager;
using NsEcsFrame.Core;
using NsEcsFrame.Unity;

namespace GameLogic.Features.Generator {
	public class DecorationGeneratorResource : IResource, ISaveableResource {
		public List<DecorationGenerateData> DecorationDatas = new();

		public void AddDecoration(DecorationType type, Coord position, SimpleVector3 scale, bool flipX) {
			DecorationDatas.Add(new DecorationGenerateData {
				Type = type,
				Position = position,
				Scale = scale,
				FlipX = flipX
			});
		}

		public void Load(IEnumerable<object> loadedData) {
			DecorationDatas.Clear();

			foreach (var data in loadedData) {
				if (data is DecorationGeneratorResource res) {
					DecorationDatas.Clear();
					DecorationDatas.AddRange(res.DecorationDatas);
					break;
				}
			}
		}
	}

	public class DecorationGenerateData {
		public DecorationType Type;
		public Coord Position;
		public SimpleVector3 Scale;
		public bool FlipX;
	}
}