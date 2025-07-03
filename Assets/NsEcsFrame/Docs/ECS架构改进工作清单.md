# ECS架构改进工作清单

## 一、背景与目标

当前框架采用了典型的面向对象结构，包括MonoSingleton单例模式、各种静态系统、观察者模式等，这些设计虽然能满足基本需求，但随着项目复杂性增加，可能面临以下问题：

1. 数据与行为高度耦合，难以灵活扩展
2. 继承层次复杂，不利于组合式功能开发
3. 对象生命周期管理与性能优化受限
4. 多线程扩展性有限

将现有框架改进为ECS架构，主要目标为：
- 分离数据(Component)与行为(System)，提高代码复用性
- 通过组合而非继承构建游戏对象
- 改进性能，支持大规模实体处理
- 提高可测试性与可扩展性

## 二、当前框架分析

### 现有核心系统

1. **NSFrameRoot**: 框架入口点，管理配置与子系统
2. **EventSystem**: 事件系统，基于枚举和委托
3. **PoolSystem**: 对象池系统，支持GameObject和普通对象
4. **SaveSystem**: 存档系统
5. **UISystem**: UI管理系统
6. **AudioSystem**: 音频系统
7. **SceneSystem**: 场景管理系统
8. **MonoService**: Unity生命周期服务

### 当前业务实现

1. **LogicFctry**: 逻辑层工厂，创建村民、建筑等对象
2. **ArchLogicBase**: 建筑基类，包含建筑逻辑与数据
3. **IBondVill**: 村民绑定接口
4. **ISaveable**: 可存档接口

## 三、ECS架构转型工作

### 1. 核心ECS框架设计

- **设计Entity组件**: 纯数据容器，唯一ID标识
  - 替代当前的GameObject和各种LogicBase类
  - 设计Entity创建、销毁、查询接口

- **设计Component系统**: 纯数据结构，无行为
  - 将当前类的属性抽取为独立组件
  - 例如：TransformComponent, VillagerComponent, BuildingComponent等
  - 组件注册与类型管理机制

- **设计System系统**: 处理特定组件集合的实体
  - 将当前类中的方法转移到相应系统
  - 实现System的注册、排序、更新机制
  - 查询与过滤机制，高效处理实体集合

- **设计World管理器**: 管理实体、组件、系统
  - 全局访问点，取代当前的多个Singleton
  - 生命周期管理
  - 系统调度

### 2. 基础组件设计

```
- TransformComponent: 位置、旋转、缩放
- TagComponent: 实体标签、名称
- IdComponent: 唯一标识
- ResourceComponent: 资源引用
- RelationshipComponent: 父子层级关系
```

### 3. 游戏逻辑组件设计

```
- VillagerComponent: 村民属性数据
- BuildingComponent: 建筑属性数据
- InventoryComponent: 库存数据
- BondComponent: 绑定关系数据
- ProductionComponent: 生产数据
- ConsumptionComponent: 消耗数据
- StateComponent: 状态机数据
```

### 4. 系统设计

```
- RenderSystem: 处理渲染逻辑
- MovementSystem: 处理移动逻辑
- ProductionSystem: 处理生产逻辑
- BondSystem: 处理绑定逻辑
- WorkSystem: 处理工作逻辑
- ResourceSystem: 资源管理
- StateSystem: 状态管理
- SaveSystem: 存档加载系统
```

### 5. 事件系统改造

- 设计基于ECS的事件系统
- 实体与组件变更事件
- 系统间通信机制
- 替换现有的EventSystem

### 6. 资源管理改造

- 组件化的资源引用
- 资源加载与卸载系统
- 基于组件的预制体实例化

### 7. UI系统改造

- UI实体与组件设计
- UI系统设计
- 基于ECS的UI事件处理

### 8. 存档系统适配

- 序列化组件数据
- 存档加载系统设计
- 实体重建机制

## 四、具体迁移步骤

### 第一阶段：核心ECS框架实现

1. 实现基础Entity类
2. 实现ComponentManager
3. 实现SystemManager
4. 实现World类
5. 搭建基本的查询与过滤机制

### 第二阶段：基础系统适配

1. 改造EventSystem为基于ECS的事件系统
2. 改造ResourceSystem为基于组件的资源管理
3. 改造PoolSystem为实体池

### 第三阶段：游戏逻辑迁移

1. 定义游戏所需的所有组件
2. 将Villager逻辑迁移为组件+系统
3. 将Building逻辑迁移为组件+系统
4. 将生产消耗逻辑迁移为组件+系统

### 第四阶段：UI与工具链适配

1. 改造UI系统适配ECS
2. 开发调试与可视化工具
3. 开发ECS编辑器扩展

## 五、性能与扩展性优化

1. **组件存储优化**:
   - 组件连续内存布局
   - 缓存友好的访问模式
   - 批量组件操作

2. **并行处理**:
   - 系统并行执行架构设计
   - 无状态系统设计，避免竞争
   - 工作线程分配策略

3. **内存管理**:
   - 组件内存池
   - 实体回收机制
   - 减少垃圾回收压力

## 六、测试与验证

1. 设计单元测试框架
2. 为核心系统编写测试
3. 性能基准测试
4. 比对改造前后的性能差异

## 七、未来扩展

1. 多线程/Job System集成
2. 编辑器工具链
3. 与Unity DOTS的可能桥接
4. 网络同步支持

---

> 这份工作清单旨在指导框架从传统OOP架构向ECS架构的转型，侧重于代码结构重组与性能优化，后续可根据实际开发过程动态调整。
