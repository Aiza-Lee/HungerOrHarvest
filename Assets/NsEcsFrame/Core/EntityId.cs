using System;

namespace NsEcsFrame.Core {
	/// <summary>
	/// 实体ID，包含唯一标识符和版本号，用于识别和验证实体
	/// </summary>
	public readonly struct EntityId : IEquatable<EntityId> {
		public static readonly EntityId NullEntityId = new(0, 0);

		public readonly uint ID;
		/// <summary>
		/// Version用于区分同一个ID在不同生命周期的实体。
		/// <para> 例如：</para>
		/// <para> - 创建实体A，分配ID=1，Version=1。</para>
		/// <para> - 销毁实体A后，ID=1被回收。</para>
		/// <para> - 再次创建新实体B，分配ID=1，但Version=2。</para>
		/// <para> 这样可以防止外部引用旧的EntityId(1,1)误操作新实体(1,2)。</para>
		/// </summary>
		public readonly uint Version;
		
		public EntityId(uint id, uint version) {
			this.ID = id;
			this.Version = version;
		}

		public bool IsValid() { return ID != 0; }
		public bool IsNull() { return ID == 0 && Version == 0; }

		public override bool Equals(object obj) {
			return obj is EntityId other && Equals(other);
		}

		public bool Equals(EntityId other) {
			return ID == other.ID && Version == other.Version;
		}

		public override int GetHashCode() {
			return HashCode.Combine(ID, Version);
		}

		public static bool operator ==(EntityId left, EntityId right) {
			return left.Equals(right);
		}

		public static bool operator !=(EntityId left, EntityId right) {
			return !left.Equals(right);
		}

		public override string ToString() {
			return $"EntityId({ID}, v{Version})";
		}
	}
}
