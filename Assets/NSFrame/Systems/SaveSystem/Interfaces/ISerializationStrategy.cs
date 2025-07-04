using System;

namespace NSFrame {
	/// <summary>
	/// 序列化策略接口，支持多种序列化实现
	/// </summary>
	public interface ISerializationStrategy {
		string Serialize(object obj, bool prettyPrint = false);
		T Deserialize<T>(string json);
		object Deserialize(string json, Type type);
	}
}
