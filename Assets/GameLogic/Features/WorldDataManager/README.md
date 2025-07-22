# World Data Manager

该功能下有 清除当前世界、加载存档、保存存档、创造新世界 四个子功能。
分别叫 ClearWorld、LoadData、SaveData、NewWorldCreator。

由于其管理整个游戏中所有对象的存储、清空问题，难免会有较高的耦合关系。

耦合主要体现在外部需要对自身进行一些标记操作，本文档将对其进行详细说明。

## ClearWorld

### 使Entity在清除操作中被忽略

* `IgnoreWorldClearComponent`

拥有该组件的Entity在清除操作中将被忽略。其余Entity将会被销毁。

### 响应世界清除操作

* `IWorldClearRespondable`

实现该接口的System和Resource将会在世界清除操作中被调用。

实现该接口的其余单例类需要自行调用`WorldClearRegistry.Inst.Register`方法进行注册。注册后将会在世界清除操作中被调用。

## SaveData

### 忽略某个Component的保存

* `IIgnoreSaveComponent`

由于框架中实现的Component并不是纯的struct而是class，并且实际使用的时候也创建了复杂的Component，并不适合保存。

实现该接口的Component在Entity的存储操作中将被忽略。但是要注意在从存档中加载时需要手动处理这些Component的恢复。

### 标记Resource为可保存

* `ISaveableResource`

实现该接口的Resource将会被保存到存档中。

该接口会有一个加载保存的信息的方法需要实现，调用者传入的参数是反序列化后的Resource对象的列表，直接遍历该列表进行反序列化即可。

> 因为数量较少，其余的需要保存的单例类的保存和加载操作目前全是手动实现的。
