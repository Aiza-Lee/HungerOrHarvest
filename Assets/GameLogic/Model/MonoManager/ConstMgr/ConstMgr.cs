using System;
using NSFrame;
using UnityEngine;

namespace GameLogic
{
	public sealed class ConstMgr : MonoSingleton<ConstMgr> {

		#region STATIC
		public static readonly int ARCH_TYPE_SIZE;
		public static readonly int JOB_TYPE_SIZE;
		public static readonly int LAYER_TYPE_SIZE;
		public static readonly int REPO_TYPE_SIZE;
		public static readonly int VILL_TYPE_SIZE;

		public static readonly int LAYERS = 21;
		public static readonly int MAX_LYR = (LAYERS - 1) / 2;
		public static readonly int MIN_LYR = -MAX_LYR;
		public static readonly int LAYER_CAPACITY = 50;
		/// <summary>
		/// 保证为偶数
		/// </summary>
		public static readonly int X_PER_ODR = 24;
		public static readonly int Y_PER_LYR = 48;
		
		public static readonly int VILL_SPARE_ORD_RADIUS = 3;

		static ConstMgr() {
			ARCH_TYPE_SIZE = GetEnumSize<ArchType>();
			JOB_TYPE_SIZE = GetEnumSize<JobType>();
			LAYER_TYPE_SIZE = GetEnumSize<LayerType>();
			REPO_TYPE_SIZE = GetEnumSize<RepoType>();
			VILL_TYPE_SIZE = GetEnumSize<VillType>();
		}
		private static int GetEnumSize<T>() where T : Enum {
			return Enum.GetValues(typeof(T)).Length;
		}
		#endregion

		[Header("挂载")] public ConstConfig Config;
		public static ConstConfig GetConfig => Inst.Config;


		protected override void Awake() {
			base.Awake();
			Config.SetConfig();
		}
	}
}