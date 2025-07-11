# ClearWorld Feature

实现清空当前世界信息的功能

``` Csharp
// System，Res，工具单例（如GidMgr），UI...实现这个接口
// 清空的时候system和res从world中找
// UI大概会沿用UIMgr的设计，可以从这里找
// 其余的反射来找
public interface IWorldClearRespondable {
    public void RespondWorldClear();
}

// Entity添加这个Comp，在Clear的时候含有这个Comp的Entity不会被清除
//（考虑到绝大多数entity是要销毁的，所以这里标记不销毁）
// 清楚entity时，如果有gidComp，记得清除gid
public class IgnoreWorldClearCompnent : IComponent {}
```

## 使用方法

静态类`ClearWorldCommand`提供了清空世界的功能。

其中有一个`Clear()`方法，调用这个方法即可清空世界。
