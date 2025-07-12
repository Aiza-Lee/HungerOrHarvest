using UnityEngine;

namespace NsEcsFrame.Unity {
	/*
	 * Unity原生的Vector2、Vector3、Color等类型对于现有的序列化器难以直接序列化（虽然一般选择默认配置，只序列化其中简单的属性不会出什么问题）	
	 * 但还是选择使用自定义的简单类型来避免潜在的问题
	*/

	[System.Serializable]
	public struct SimpleColor {
		public float r, g, b, a;
		public SimpleColor(float r, float g, float b, float a = 1f) {
			this.r = r; this.g = g; this.b = b; this.a = a;
		}
		public SimpleColor(Color c) : this(c.r, c.g, c.b, c.a) { }
		public void ModifyAlpha(float alpha) { a = alpha; }
		public static implicit operator Color(SimpleColor c) => new(c.r, c.g, c.b, c.a);
		public static implicit operator SimpleColor(Color c) => new(c);
		public override readonly string ToString() => $"SimpleColor(r:{r}, g:{g}, b:{b}, a:{a})";
	}

	[System.Serializable]
	public struct SimpleVector2 {
		public float x, y;
		public SimpleVector2(float x, float y) { this.x = x; this.y = y; }
		public SimpleVector2(Vector2 v) : this(v.x, v.y) { }
		public static implicit operator Vector2(SimpleVector2 v) => new(v.x, v.y);
		public static implicit operator SimpleVector2(Vector2 v) => new(v);

		public static SimpleVector2 operator +(SimpleVector2 a, SimpleVector2 b) => new(a.x + b.x, a.y + b.y);
		public static SimpleVector2 operator +(SimpleVector2 v, Vector2 b) => new(v.x + b.x, v.y + b.y);
		public static SimpleVector2 operator +(Vector2 a, SimpleVector2 b) => new(a.x + b.x, a.y + b.y);

		public static SimpleVector2 operator -(SimpleVector2 a, SimpleVector2 b) => new(a.x - b.x, a.y - b.y);
		public static SimpleVector2 operator -(SimpleVector2 v, Vector2 b) => new(v.x - b.x, v.y - b.y);
		public static SimpleVector2 operator -(Vector2 a, SimpleVector2 b) => new(a.x - b.x, a.y - b.y);

		public static SimpleVector2 operator -(SimpleVector2 v) => new(-v.x, -v.y);

		public static SimpleVector2 operator *(SimpleVector2 v, float scalar) => new(v.x * scalar, v.y * scalar);
		public static SimpleVector2 operator *(float scalar, SimpleVector2 v) => new(v.x * scalar, v.y * scalar);

		public static SimpleVector2 operator *(SimpleVector2 a, SimpleVector2 b) => new(a.x * b.x, a.y * b.y);
		public static SimpleVector2 operator *(SimpleVector2 a, Vector2 b) => new(a.x * b.x, a.y * b.y);
		public static SimpleVector2 operator *(Vector2 a, SimpleVector2 b) => new(a.x * b.x, a.y * b.y);

		public static SimpleVector2 operator /(SimpleVector2 v, float scalar) => new(v.x / scalar, v.y / scalar);
		public override readonly string ToString() => $"SimpleVector2(x:{x}, y:{y})";
	}

	[System.Serializable]
	public struct SimpleVector3 {
		public float x, y, z;
		public SimpleVector3(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
		public SimpleVector3(Vector3 v) : this(v.x, v.y, v.z) { }
		public static implicit operator Vector3(SimpleVector3 v) => new(v.x, v.y, v.z);
		public static implicit operator SimpleVector3(Vector3 v) => new(v);

		public static SimpleVector3 operator +(SimpleVector3 a, SimpleVector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);
		public static SimpleVector3 operator +(SimpleVector3 v, Vector3 b) => new(v.x + b.x, v.y + b.y, v.z + b.z);
		public static SimpleVector3 operator +(Vector3 a, SimpleVector3 b) => new(a.x + b.x, a.y + b.y, a.z + b.z);

		public static SimpleVector3 operator -(SimpleVector3 a, Vector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
		public static SimpleVector3 operator -(Vector3 a, SimpleVector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);
		public static SimpleVector3 operator -(SimpleVector3 a, SimpleVector3 b) => new(a.x - b.x, a.y - b.y, a.z - b.z);

		public static SimpleVector3 operator -(SimpleVector3 v) => new(-v.x, -v.y, -v.z);

		public static SimpleVector3 operator *(SimpleVector3 v, float scalar) => new(v.x * scalar, v.y * scalar, v.z * scalar);
		public static SimpleVector3 operator *(float scalar, SimpleVector3 v) => new(v.x * scalar, v.y * scalar, v.z * scalar);

		public static SimpleVector3 operator *(SimpleVector3 a, SimpleVector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
		public static SimpleVector3 operator *(SimpleVector3 a, Vector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);
		public static SimpleVector3 operator *(Vector3 a, SimpleVector3 b) => new(a.x * b.x, a.y * b.y, a.z * b.z);

		public static SimpleVector3 operator /(SimpleVector3 v, float scalar) => new(v.x / scalar, v.y / scalar, v.z / scalar);
		public override readonly string ToString() => $"SimpleVector3(x:{x}, y:{y}, z:{z})";
	}

	[System.Serializable]
	public struct SimpleQuaternion {
		public float x, y, z, w;
		public SimpleQuaternion(float x, float y, float z, float w) {
			this.x = x; this.y = y; this.z = z; this.w = w;
		}
		public SimpleQuaternion(Quaternion q) : this(q.x, q.y, q.z, q.w) { }
		public static implicit operator Quaternion(SimpleQuaternion q) => new(q.x, q.y, q.z, q.w);
		public static implicit operator SimpleQuaternion(Quaternion q) => new(q);

		public static SimpleQuaternion operator +(SimpleQuaternion a, SimpleQuaternion b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		public static SimpleQuaternion operator +(SimpleQuaternion q, Quaternion b) => new(q.x + b.x, q.y + b.y, q.z + b.z, q.w + b.w);
		public static SimpleQuaternion operator +(Quaternion a, SimpleQuaternion b) => new(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);

		public static SimpleQuaternion operator -(SimpleQuaternion a, SimpleQuaternion b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		public static SimpleQuaternion operator -(SimpleQuaternion q, Quaternion b) => new(q.x - b.x, q.y - b.y, q.z - b.z, q.w - b.w);
		public static SimpleQuaternion operator -(Quaternion a, SimpleQuaternion b) => new(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);


		public static SimpleQuaternion operator -(SimpleQuaternion q) => new(-q.x, -q.y, -q.z, -q.w);

		public static SimpleQuaternion operator *(SimpleQuaternion q, float scalar) => new(q.x * scalar, q.y * scalar, q.z * scalar, q.w * scalar);
		public static SimpleQuaternion operator *(float scalar, SimpleQuaternion q) => new(q.x * scalar, q.y * scalar, q.z * scalar, q.w * scalar);
		public static SimpleQuaternion operator /(SimpleQuaternion q, float scalar) => new(q.x / scalar, q.y / scalar, q.z / scalar, q.w / scalar);
		public override readonly string ToString() => $"SimpleQuaternion(x:{x}, y:{y}, z:{z}, w:{w})";
	}
}
