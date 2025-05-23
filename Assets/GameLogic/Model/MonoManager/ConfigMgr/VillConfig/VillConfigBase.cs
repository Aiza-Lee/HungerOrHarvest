using System;
using UnityEngine;

namespace GameLogic.Model.Mgr
{
	public abstract class VillConfigBase : ScriptableObject {
		[Header("类型名称(区分大小写)")] public string TypeName;
		private VillType? _villType;
		public VillType VillType => _villType ??= Enum.Parse<VillType>(TypeName);

		[Header("随机游走横向半径(相对于已解锁的地块,计量单位是ORD)")] public int SpareOrdRadius;
	}
}