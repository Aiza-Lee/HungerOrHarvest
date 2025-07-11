namespace GameLogic.Features.SaveLoadData {
	/// <summary>
	/// ISaveIgnoreComponent 接口用于标记不需要保存的组件。
	/// 需要保存的实体会含有SavedEntityComponent组件，配合该接口实现更细致的保存控制。
	/// 但是需要注意在加载存档时，这些组件不会被加载。
	/// </summary>
	public interface ISaveIgnoreComponent { }
}