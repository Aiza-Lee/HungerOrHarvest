using System;
using UnityEngine;

namespace OldGameLogic.Model.Mgr
{
	public abstract class LayerConfigBase : ScriptableObject {
		[Header("类型名称(区分大小写)")] public string TypeName;
		private LayerType? _layerType = null;
		public LayerType LayerType => _layerType ??= Enum.Parse<LayerType>(TypeName);
		
		[Header("名称")] public string Name;
		[Header("介绍")][TextArea(5, 30)] public string Introductions;
	}
}