using System;
using System.Collections.Generic;

namespace NsEcsFrame.Core {
	/// <summary>
	/// Entity查询构建器，用于构建复杂的Entity查询。
	/// 支持链式调用，灵活组合多种查询条件。
	/// <para>
	/// <b>用法示例：</b>
	/// <code><![CDATA[
	/// // 查询同时拥有TransformComponent和TagComponent的所有实体
	/// var query = world.CreateQueryBuilder()
	///     .WithAll<TransformComponent, TagComponent>()
	///     .Build();
	/// 
	/// // 查询拥有任意一种资源组件但没有TagComponent的实体
	/// var query = world.CreateQueryBuilder()
	///     .WithAny<ResourceRefComponent, InventoryComponent>()
	///     .Without<TagComponent>()
	///     .Build();
	/// 
	/// //查询没有RelationshipComponent的所有实体
	/// var query = world.CreateQueryBuilder()
	///     .Without<RelationshipComponent>()
	///     .Build();
	/// 
	/// // 查询拥有TransformComponent但没有TagComponent和RelationshipComponent的实体
	/// var query = world.CreateQueryBuilder()
	///     .WithAll<TransformComponent>()
	///     .Without<TagComponent, RelationshipComponent>()
	///     .Build();
	/// ]]></code>
	/// </para>
	/// </summary>
	public class EntityQueryBuilder {
		private readonly World _world;
		private readonly List<Type> _withAllTypes = new();
		private readonly List<Type> _withAnyTypes = new();
		private readonly List<Type> _withoutTypes = new();

		internal EntityQueryBuilder(World world) {
			_world = world;
		}

		/// <summary>
		/// 添加必须包含的Component类型
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <returns>查询构建器</returns>
		public EntityQueryBuilder WithAll<T>() where T : class, IComponent {
			_withAllTypes.Add(typeof(T));
			return this;
		}

		/// <summary>
		/// 添加必须包含的多个Component类型
		/// </summary>
		/// <typeparam name="T1">第一个Component类型</typeparam>
		/// <typeparam name="T2">第二个Component类型</typeparam>
		/// <returns>查询构建器</returns>
		public EntityQueryBuilder WithAll<T1, T2>()
			where T1 : class, IComponent
			where T2 : class, IComponent {
			_withAllTypes.Add(typeof(T1));
			_withAllTypes.Add(typeof(T2));
			return this;
		}

		/// <summary>
		/// 添加必须包含的多个Component类型
		/// </summary>
		/// <typeparam name="T1">第一个Component类型</typeparam>
		/// <typeparam name="T2">第二个Component类型</typeparam>
		/// <typeparam name="T3">第三个Component类型</typeparam>
		/// <returns>查询构建器</returns>
		public EntityQueryBuilder WithAll<T1, T2, T3>()
			where T1 : class, IComponent
			where T2 : class, IComponent
			where T3 : class, IComponent {
			_withAllTypes.Add(typeof(T1));
			_withAllTypes.Add(typeof(T2));
			_withAllTypes.Add(typeof(T3));
			return this;
		}

		/// <summary>
		/// 添加至少包含一种的Component类型
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <returns>查询构建器</returns>
		public EntityQueryBuilder WithAny<T>() where T : class, IComponent {
			_withAnyTypes.Add(typeof(T));
			return this;
		}

		/// <summary>
		/// 添加至少包含一种的多个Component类型
		/// </summary>
		/// <typeparam name="T1">第一个Component类型</typeparam>
		/// <typeparam name="T2">第二个Component类型</typeparam>
		/// <returns>查询构建器</returns>
		public EntityQueryBuilder WithAny<T1, T2>()
			where T1 : class, IComponent
			where T2 : class, IComponent {
			_withAnyTypes.Add(typeof(T1));
			_withAnyTypes.Add(typeof(T2));
			return this;
		}

		/// <summary>
		/// 添加不能包含的Component类型
		/// </summary>
		/// <typeparam name="T">Component类型</typeparam>
		/// <returns>查询构建器</returns>
		public EntityQueryBuilder Without<T>() where T : class, IComponent {
			_withoutTypes.Add(typeof(T));
			return this;
		}

		/// <summary>
		/// 添加不能包含的多个Component类型
		/// </summary>
		/// <typeparam name="T1">第一个Component类型</typeparam>
		/// <typeparam name="T2">第二个Component类型</typeparam>
		/// <returns>查询构建器</returns>
		public EntityQueryBuilder Without<T1, T2>()
			where T1 : class, IComponent
			where T2 : class, IComponent {
			_withoutTypes.Add(typeof(T1));
			_withoutTypes.Add(typeof(T2));
			return this;
		}

		/// <summary>
		/// 构建查询并执行
		/// </summary>
		/// <returns>查询结果</returns>
		public EntityQuery Build() {
			return _world.Query(_withAllTypes, _withAnyTypes, _withoutTypes);
		}
	}
}