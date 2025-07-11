# Destroyer Feature

是游戏运行时销毁Entity的功能模块。

## Common

Common部分提供了一个通用的实体销毁工具类`EntityDestroyUtil`，它包含一个静态方法`DestroyEntityWithGid`，用于销毁实体，并从移除GID。

所有销毁都应该使用这个工具类来确保GID的正确管理。

[具体实现](./Common/EntityDestroyUtil.cs)

## xxxDestroyer

一般会提供一个Resource类来存储需要销毁的实体GID列表。

要销毁实体就把GID添加到这个列表中。
