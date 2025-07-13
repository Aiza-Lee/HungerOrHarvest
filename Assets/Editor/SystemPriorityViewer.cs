using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Reflection;
using NsEcsFrame.Core;
using GameLogic.World;

public class SystemPriorityViewer : EditorWindow {
	private Vector2 scrollPos;
	private IWorld world;
	private ISystemManager systemManager;
	private ISystem[] systems;
	private string searchFilter = "";
	private bool sortByPriority = true;
	private bool showEnabledOnly = false;
	private bool showUnregisteredSystems = false;
	private readonly Dictionary<System.Type, bool> foldouts = new();
	private Type[] unregisteredSystemTypes;

	[MenuItem("Tools/System优先级查看器")]
	public static void ShowWindow() {
		GetWindow<SystemPriorityViewer>("System优先级查看器");
	}

	private void OnEnable() {
		// 尝试通过场景查找 GameWorldMono
		world = GameWorldMono.MainWorld;
		systemManager = world?.SystemManager;
		systems = systemManager?.GetAllSystems()?.ToArray();
		
		// 查找未注册的系统
		FindUnregisteredSystems();
	}
	
	private void FindUnregisteredSystems() {
		var allSystemTypes = new List<Type>();
		
		// 扫描当前程序集中所有实现ISystem的类型
		foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
			try {
				var systemTypes = assembly.GetTypes()
					.Where(t => typeof(ISystem).IsAssignableFrom(t) && 
					           !t.IsInterface && 
					           !t.IsAbstract)
					.ToArray();
				allSystemTypes.AddRange(systemTypes);
			}
			catch (ReflectionTypeLoadException) {
				// 忽略加载失败的程序集
			}
		}
		
		// 找出未注册的系统类型
		var registeredTypes = systems?.Select(s => s.GetType()).ToHashSet() ?? new HashSet<Type>();
		unregisteredSystemTypes = allSystemTypes.Where(t => !registeredTypes.Contains(t)).ToArray();
	}

	private void OnGUI() {
		EditorGUILayout.BeginVertical();

		// 顶部控制面板
		DrawControlPanel();

		if (systems == null) {
			EditorGUILayout.HelpBox("未找到系统列表，请确保场景中有GameWorldMono并已初始化。", MessageType.Warning);
			if (GUILayout.Button("刷新")) {
				OnEnable();
			}
			EditorGUILayout.EndVertical();
			return;
		}

		// 统计信息
		DrawStatistics();

		// 系统列表
		if (showUnregisteredSystems) {
			DrawUnregisteredSystemsList();
		} else {
			DrawSystemsList();
		}

		EditorGUILayout.EndVertical();
	}

	private void DrawControlPanel() {
		EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

		// 搜索框
		GUILayout.Label("搜索:", GUILayout.Width(40));
		searchFilter = EditorGUILayout.TextField(searchFilter, EditorStyles.toolbarTextField, GUILayout.Width(200));

		GUILayout.Space(10);

		// 排序选项
		sortByPriority = GUILayout.Toggle(sortByPriority, "按优先级排序", EditorStyles.toolbarButton);

		GUILayout.Space(10);

		// 过滤选项
		showEnabledOnly = GUILayout.Toggle(showEnabledOnly, "仅显示启用", EditorStyles.toolbarButton);
		
		GUILayout.Space(10);
		
		// 显示未注册系统选项
		showUnregisteredSystems = GUILayout.Toggle(showUnregisteredSystems, "显示未注册系统", EditorStyles.toolbarButton);

		GUILayout.FlexibleSpace();

		// 刷新按钮
		if (GUILayout.Button("刷新", EditorStyles.toolbarButton)) {
			OnEnable();
		}

		EditorGUILayout.EndHorizontal();
	}

	private void DrawStatistics() {
		if (systems == null) return;

		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
		var enabledCount = systems.Count(s => s.Enabled);
		var totalCount = systems.Length;
		var unregisteredCount = unregisteredSystemTypes?.Length ?? 0;

		GUILayout.Label($"已注册: {totalCount}", EditorStyles.miniLabel);
		GUILayout.Space(20);
		GUILayout.Label($"启用: {enabledCount}", EditorStyles.miniLabel);
		GUILayout.Space(20);
		GUILayout.Label($"禁用: {totalCount - enabledCount}", EditorStyles.miniLabel);
		GUILayout.Space(20);
		if (unregisteredCount > 0) {
			var originalColor = GUI.color;
			GUI.color = Color.yellow;
			GUILayout.Label($"未注册: {unregisteredCount}", EditorStyles.miniLabel);
			GUI.color = originalColor;
		} else {
			GUILayout.Label($"未注册: {unregisteredCount}", EditorStyles.miniLabel);
		}

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
	}

	private void DrawSystemsList() {
		// 过滤系统
		var filteredSystems = systems.AsEnumerable();

		if (!string.IsNullOrEmpty(searchFilter)) {
			filteredSystems = filteredSystems.Where(s =>
				s.GetType().Name.ToLower().Contains(searchFilter.ToLower()));
		}

		if (showEnabledOnly) {
			filteredSystems = filteredSystems.Where(s => s.Enabled);
		}

		// 排序
		if (sortByPriority) {
			filteredSystems = filteredSystems.OrderBy(s => s.Priority);
		} else {
			filteredSystems = filteredSystems.OrderBy(s => s.GetType().Name);
		}

		var systemsArray = filteredSystems.ToArray();

		// 表头
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("启用", EditorStyles.boldLabel, GUILayout.Width(40));
		EditorGUILayout.LabelField("系统类型", EditorStyles.boldLabel, GUILayout.Width(300));
		EditorGUILayout.LabelField("优先级", EditorStyles.boldLabel, GUILayout.Width(80));
		EditorGUILayout.LabelField("命名空间", EditorStyles.boldLabel, GUILayout.Width(200));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space(5);

		// 系统列表
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

		foreach (var sys in systemsArray) {
			DrawSystemRow(sys);
		}

		EditorGUILayout.EndScrollView();
	}

	private void DrawSystemRow(ISystem system) {
		var systemType = system.GetType();
		var backgroundColor = system.Enabled ? Color.white : Color.gray;

		// 设置背景色
		var originalColor = GUI.backgroundColor;
		GUI.backgroundColor = backgroundColor;

		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

		// 启用状态（只读显示）
		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.Toggle(system.Enabled, GUILayout.Width(40));
		EditorGUI.EndDisabledGroup();

		// 系统名称（可点击展开详情）
		if (!foldouts.ContainsKey(systemType)) {
			foldouts[systemType] = false;
		}

		foldouts[systemType] = EditorGUILayout.Foldout(foldouts[systemType], systemType.Name, EditorStyles.foldout);
		GUILayout.Space(300 - EditorGUIUtility.labelWidth);

		// 优先级
		EditorGUILayout.LabelField(system.Priority.ToString(), GUILayout.Width(80));

		// 命名空间
		EditorGUILayout.LabelField(systemType.Namespace ?? "无", GUILayout.Width(200));

		EditorGUILayout.EndHorizontal();

		// 展开详情
		if (foldouts[systemType]) {
			EditorGUI.indentLevel++;
			DrawSystemDetails(system);
			EditorGUI.indentLevel--;
		}

		GUI.backgroundColor = originalColor;
	}

	private void DrawSystemDetails(ISystem system) {
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);

		var systemType = system.GetType();

		// 基本信息
		EditorGUILayout.LabelField("详细信息:", EditorStyles.boldLabel);
		EditorGUILayout.LabelField($"完整类型名: {systemType.FullName}");
		EditorGUILayout.LabelField($"程序集: {systemType.Assembly.GetName().Name}");

		// 如果系统实现了特定接口，显示相关信息
		var interfaces = systemType.GetInterfaces().Where(i => i != typeof(ISystem)).ToArray();
		if (interfaces.Length > 0) {
			EditorGUILayout.LabelField("实现的接口:");
			foreach (var interfaceType in interfaces) {
				EditorGUILayout.LabelField($"  • {interfaceType.Name}");
			}
		}

		EditorGUILayout.EndVertical();
	}

	private void DrawUnregisteredSystemsList() {
		if (unregisteredSystemTypes == null || unregisteredSystemTypes.Length == 0) {
			EditorGUILayout.HelpBox("没有发现未注册的系统类型。", MessageType.Info);
			return;
		}
		
		// 过滤未注册系统
		var filteredTypes = unregisteredSystemTypes.AsEnumerable();
		
		if (!string.IsNullOrEmpty(searchFilter)) {
			filteredTypes = filteredTypes.Where(t => 
				t.Name.ToLower().Contains(searchFilter.ToLower()));
		}
		
		// 排序
		if (sortByPriority) {
			// 对于未注册的系统，我们需要创建实例来获取Priority（如果可能）
			filteredTypes = filteredTypes.OrderBy(t => GetSystemPriority(t));
		} else {
			filteredTypes = filteredTypes.OrderBy(t => t.Name);
		}
		
		var typesArray = filteredTypes.ToArray();
		
		// 表头
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("状态", EditorStyles.boldLabel, GUILayout.Width(40));
		EditorGUILayout.LabelField("系统类型 (未注册)", EditorStyles.boldLabel, GUILayout.Width(300));
		EditorGUILayout.LabelField("预期优先级", EditorStyles.boldLabel, GUILayout.Width(80));
		EditorGUILayout.LabelField("命名空间", EditorStyles.boldLabel, GUILayout.Width(200));
		EditorGUILayout.EndHorizontal();
		
		EditorGUILayout.Space(5);
		
		// 系统列表
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		
		foreach (var systemType in typesArray) {
			DrawUnregisteredSystemRow(systemType);
		}
		
		EditorGUILayout.EndScrollView();
	}
	
	private void DrawUnregisteredSystemRow(Type systemType) {
		var originalColor = GUI.backgroundColor;
		GUI.backgroundColor = Color.red * 0.3f + Color.white * 0.7f; // 淡红色背景
		
		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
		
		// 状态图标（未注册）
		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.Toggle(false, GUILayout.Width(40));
		EditorGUI.EndDisabledGroup();
		
		// 系统名称（可点击展开详情）
		if (!foldouts.ContainsKey(systemType)) {
			foldouts[systemType] = false;
		}
		
		foldouts[systemType] = EditorGUILayout.Foldout(foldouts[systemType], systemType.Name, EditorStyles.foldout);
		GUILayout.Space(300 - EditorGUIUtility.labelWidth);
		
		// 预期优先级
		var priority = GetSystemPriority(systemType);
		EditorGUILayout.LabelField(priority.ToString(), GUILayout.Width(80));
		
		// 命名空间
		EditorGUILayout.LabelField(systemType.Namespace ?? "无", GUILayout.Width(200));
		
		EditorGUILayout.EndHorizontal();
		
		// 展开详情
		if (foldouts[systemType]) {
			EditorGUI.indentLevel++;
			DrawUnregisteredSystemDetails(systemType);
			EditorGUI.indentLevel--;
		}
		
		GUI.backgroundColor = originalColor;
	}
	
	private void DrawUnregisteredSystemDetails(Type systemType) {
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		
		// 基本信息
		EditorGUILayout.LabelField("详细信息:", EditorStyles.boldLabel);
		EditorGUILayout.LabelField($"完整类型名: {systemType.FullName}");
		EditorGUILayout.LabelField($"程序集: {systemType.Assembly.GetName().Name}");
		
		// 状态提示
		var originalColor = GUI.color;
		GUI.color = Color.yellow;
		EditorGUILayout.LabelField("⚠️ 此系统未在GameWorldMono中注册！", EditorStyles.boldLabel);
		GUI.color = originalColor;
		
		// 如果系统实现了特定接口，显示相关信息
		var interfaces = systemType.GetInterfaces().Where(i => i != typeof(ISystem)).ToArray();
		if (interfaces.Length > 0) {
			EditorGUILayout.LabelField("实现的接口:");
			foreach (var interfaceType in interfaces) {
				EditorGUILayout.LabelField($"  • {interfaceType.Name}");
			}
		}
		
		// 构造函数信息
		var constructors = systemType.GetConstructors();
		if (constructors.Length > 0) {
			EditorGUILayout.LabelField("构造函数:");
			foreach (var constructor in constructors) {
				var parameters = constructor.GetParameters();
				var paramStr = parameters.Length > 0 ? 
					string.Join(", ", parameters.Select(p => $"{p.ParameterType.Name} {p.Name}")) : 
					"无参数";
				EditorGUILayout.LabelField($"  • {systemType.Name}({paramStr})");
			}
		}
		
		EditorGUILayout.EndVertical();
	}
	
	private int GetSystemPriority(Type systemType) {
		try {
			// 尝试创建实例获取Priority（仅对无参构造函数的类型）
			if (systemType.GetConstructor(Type.EmptyTypes) != null) {
				var instance = Activator.CreateInstance(systemType) as ISystem;
				return instance?.Priority ?? 0;
			}
		}
		catch {
			// 如果创建失败，返回默认值
		}
		return 0; // 默认优先级
	}
}
