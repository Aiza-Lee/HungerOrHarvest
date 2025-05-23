using System;

namespace GameLogic.Model.Mgr
{
	public sealed class ConstMgr {
		private ConstMgr() { }
		public static ConstMgr Inst { get; } = new();

		public static readonly int ARCH_TYPE_SIZE;
		public static readonly int JOB_TYPE_SIZE;
		public static readonly int LAYER_TYPE_SIZE;
		public static readonly int REPO_TYPE_SIZE;
		public static readonly int VILL_TYPE_SIZE;

		/// <summary>
		/// 每层的容量
		/// </summary>
		public static readonly int LAYER_CAPACITY = 50;
		/// <summary>
		/// 保证为偶数
		/// </summary>
		public static readonly int X_PER_ODR = 24;
		public static readonly int Y_PER_LYR = 48;

		/// <summary>
		/// 总层数上限
		/// </summary>
		public static readonly int LAYERS = 21;
		/// <summary>
		/// 最大层编号
		/// </summary>
		public static readonly int MAX_LYR = (LAYERS - 1) / 2;
		/// <summary>
		/// 最小层编号
		/// </summary>
		public static readonly int MIN_LYR = -MAX_LYR;

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
	}
}