using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;
using NsEcsFrame.Core;
using GameLogic.World;
using Editor.Utils;

/// <summary>
/// ECS实体查看器 - Unity Editor窗口
/// 用于查看和调试ECS世界中的实体和组件
/// </summary>
public class EntityViewer : EditorWindow {
	private Vector2 _scrollPos;
	private IWorld _world;
	private Entity[] _entities;
	private string _searchFilter = "";
	private bool _sortByEntityId = true;
	private bool _showComponentDetails = true;
	private readonly Dictionary<ulong, bool> _entityFoldouts = new();
	private readonly Dictionary<Type, bool> _componentFoldouts = new();

	[MenuItem("Tools/ECS实体查看器")]
	public static void ShowWindow() {
		GetWindow<EntityViewer>("ECS实体查看器");
	}

	private void OnEnable() {
		RefreshEntities();
	}

	private void RefreshEntities() {
		_world = GameWorldMono.MainWorld;
		if (_world != null) {
			try {
				// 直接使用IWorld的GetAllEntities方法
				_entities = _world.GetAllEntities().ToArray();
			} catch (Exception e) {
				Debug.LogError($"获取实体列表失败: {e.Message}");
				_entities = new Entity[0];
			}
		} else {
			_entities = new Entity[0];
		}
	}

	private void OnGUI() {
		EditorGUILayout.BeginVertical();

		// 顶部控制面板
		DrawControlPanel();

		if (_world == null) {
			EditorGUILayout.HelpBox("未找到World实例，请确保场景中有GameWorldMono并已初始化。", MessageType.Warning);
			EditorGUILayout.EndVertical();
			return;
		}

		if (_entities == null || _entities.Length == 0) {
			EditorGUILayout.HelpBox("当前世界中没有实体。", MessageType.Info);
			EditorGUILayout.EndVertical();
			return;
		}

		// 统计信息
		DrawStatistics();

		// 实体列表
		DrawEntitiesList();

		EditorGUILayout.EndVertical();
	}

	private void DrawControlPanel() {
		EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

		// 搜索框
		GUILayout.Label("搜索:", GUILayout.Width(40));
		_searchFilter = EditorGUILayout.TextField(_searchFilter, EditorStyles.toolbarTextField, GUILayout.Width(200));

		GUILayout.Space(10);

		// 排序选项
		_sortByEntityId = GUILayout.Toggle(_sortByEntityId, "按实体ID排序", EditorStyles.toolbarButton);

		GUILayout.Space(10);

		// 显示详情选项
		_showComponentDetails = GUILayout.Toggle(_showComponentDetails, "显示组件详情", EditorStyles.toolbarButton);

		GUILayout.FlexibleSpace();

		// 刷新按钮
		if (GUILayout.Button("刷新", EditorStyles.toolbarButton)) {
			RefreshEntities();
		}

		EditorGUILayout.EndHorizontal();
	}

	private void DrawStatistics() {
		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
		var totalCount = _entities.Length;
		var activeCount = _entities.Count(e => e.IsValid()); // 使用IsValid方法

		GUILayout.Label($"总实体数: {totalCount}", EditorStyles.miniLabel);
		GUILayout.Space(20);
		GUILayout.Label($"有效: {activeCount}", EditorStyles.miniLabel);
		GUILayout.Space(20);
		GUILayout.Label($"无效: {totalCount - activeCount}", EditorStyles.miniLabel);

		GUILayout.FlexibleSpace();
		EditorGUILayout.EndHorizontal();
	}

	private void DrawEntitiesList() {
		// 过滤实体
		var filteredEntities = _entities.AsEnumerable();

		if (!string.IsNullOrEmpty(_searchFilter)) {
			filteredEntities = filteredEntities.Where(e => {
				// 按实体ID或组件类型搜索
				var entityIdMatch = e.ID.ToString().Contains(_searchFilter);
				var componentMatch = false;

				try {
					var components = GetEntityComponents(e);
					componentMatch = components.Any(c => c.GetType().Name.ToLower().Contains(_searchFilter.ToLower()));
				} catch {
					// 忽略获取组件失败的情况
				}

				return entityIdMatch || componentMatch;
			});
		}

		// 排序
		if (_sortByEntityId) {
			filteredEntities = filteredEntities.OrderBy(e => e.ID.ID);
		} else {
			filteredEntities = filteredEntities.OrderBy(e => GetEntityComponents(e).Count());
		}

		var entitiesArray = filteredEntities.ToArray();

		// 表头
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("状态", EditorStyles.boldLabel, GUILayout.Width(40));
		EditorGUILayout.LabelField("实体ID", EditorStyles.boldLabel, GUILayout.Width(100));
		EditorGUILayout.LabelField("组件数量", EditorStyles.boldLabel, GUILayout.Width(80));
		EditorGUILayout.LabelField("主要组件", EditorStyles.boldLabel, GUILayout.MinWidth(200));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.Space(5);

		// 实体列表
		_scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

		for (int i = 0; i < entitiesArray.Length; i++) {
			DrawEntityRow(entitiesArray[i], i);
		}

		EditorGUILayout.EndScrollView();
	}

	private void DrawEntityRow(Entity entity, int index) {
		var isValid = entity.IsValid();
		var backgroundColor = isValid ? Color.white : Color.gray;
		var entityId = entity.ID;

		// 设置背景色
		var originalColor = GUI.backgroundColor;
		GUI.backgroundColor = backgroundColor;

		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);

		// 状态图标
		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.Toggle(isValid, GUILayout.Width(40));
		EditorGUI.EndDisabledGroup();

		// 实体ID（可点击展开详情）
		if (!_entityFoldouts.ContainsKey(entityId.ID)) {
			_entityFoldouts[entityId.ID] = false;
		}

		var components = GetEntityComponents(entity);
		var componentCount = components.Count();
		var mainComponents = string.Join(", ", components.Take(3).Select(c => c.GetType().Name));
		if (componentCount > 3) {
			mainComponents += "...";
		}

		_entityFoldouts[entityId.ID] = EditorGUILayout.Foldout(_entityFoldouts[entityId.ID], $"Entity #{entityId.ID}", EditorStyles.foldout);
		GUILayout.Space(100 - EditorGUIUtility.labelWidth);

		// 组件数量
		EditorGUILayout.LabelField(componentCount.ToString(), GUILayout.Width(80));

		// 主要组件
		EditorGUILayout.LabelField(mainComponents, GUILayout.MinWidth(200));

		EditorGUILayout.EndHorizontal();

		// 展开详情
		if (_entityFoldouts[entityId.ID]) {
			EditorGUI.indentLevel++;
			DrawEntityDetails(entity);
			EditorGUI.indentLevel--;
		}

		GUI.backgroundColor = originalColor;
	}

	private void DrawEntityDetails(Entity entity) {
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);

		// 基本信息
		EditorGUILayout.LabelField("实体详情:", EditorStyles.boldLabel);
		EditorGUILayout.LabelField($"实体ID: {entity.ID.ID}");
		EditorGUILayout.LabelField($"是否有效: {entity.IsValid()}");

		// 组件列表
		var components = GetEntityComponents(entity);
		if (components.Any()) {
			EditorGUILayout.Space(5);
			EditorGUILayout.LabelField($"组件列表 ({components.Count()}):", EditorStyles.boldLabel);

			foreach (var component in components) {
				DrawComponentInfo(component);
			}
		} else {
			EditorGUILayout.LabelField("此实体没有组件");
		}

		EditorGUILayout.EndVertical();
	}

	private void DrawComponentInfo(object component) {
		if (component == null) return;

		var componentType = component.GetType();

		EditorGUILayout.BeginHorizontal();

		// 组件类型名称
		if (!_componentFoldouts.ContainsKey(componentType)) {
			_componentFoldouts[componentType] = false;
		}

		_componentFoldouts[componentType] = EditorGUILayout.Foldout(
			_componentFoldouts[componentType],
			componentType.Name,
			EditorStyles.foldout
		);

		EditorGUILayout.EndHorizontal();

		// 组件详情
		if (_componentFoldouts[componentType] && _showComponentDetails) {
			EditorGUI.indentLevel++;
			ReflectionComponentDrawer.DrawComponentDetails(component);
			EditorGUI.indentLevel--;
		}
	}

	private IEnumerable<object> GetEntityComponents(Entity entity) {
		try {
			// 使用Entity的GetAllComponents方法
			if (entity.IsValid()) {
				return entity.GetAllComponents().Cast<object>();
			}
		} catch (Exception e) {
			Debug.LogError($"获取实体组件失败: {e.Message}");
		}

		return new object[0];
	}
}
