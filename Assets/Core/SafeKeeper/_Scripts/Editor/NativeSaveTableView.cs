using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditor.TreeViewExamples;
using UnityEngine;
using UnityEngine.Assertions;


namespace SafeKepperEditor.Table
{
	internal class NativeSaveTableView : TreeViewWithTreeModel<SaveData>
	{
		const float kRowHeights = 20f;
		const float kToggleWidth = 18f;
		public bool showControls = true;

		// All columns
		enum MyColumns
		{
			Key,
			Data,
			Type,
			KeySize,
			Actions,
		}

		public enum SortOption
		{
			Key,
			Data,
			Type,
			KeySize,
			Actions
		}

		// Sort options per column
		SortOption[] m_SortOptions = 
		{
			SortOption.Key, 
			SortOption.Data, 
			SortOption.Type, 
			SortOption.KeySize,
			SortOption.Actions,
			
		};

		public static void TreeToList (TreeViewItem root, IList<TreeViewItem> result)
		{
			if (root == null)
				throw new NullReferenceException("root");
			if (result == null)
				throw new NullReferenceException("result");

			result.Clear();
	
			if (root.children == null)
				return;

			Stack<TreeViewItem> stack = new Stack<TreeViewItem>();
			for (int i = root.children.Count - 1; i >= 0; i--)
				stack.Push(root.children[i]);

			while (stack.Count > 0)
			{
				TreeViewItem current = stack.Pop();
				result.Add(current);

				if (current.hasChildren && current.children[0] != null)
				{
					for (int i = current.children.Count - 1; i >= 0; i--)
					{
						stack.Push(current.children[i]);
					}
				}
			}
		}

		public NativeSaveTableView (TreeViewState state, MultiColumnHeader multicolumnHeader, TreeModel<SaveData> model) : base (state, multicolumnHeader, model)
		{
			Assert.AreEqual(m_SortOptions.Length , Enum.GetValues(typeof(MyColumns)).Length, "Ensure number of sort options are in sync with number of MyColumns enum values");

			// Custom setup
			rowHeight = kRowHeights;
			columnIndexForTreeFoldouts = 2;
			showAlternatingRowBackgrounds = true;
			showBorder = true;
			customFoldoutYOffset = (kRowHeights - EditorGUIUtility.singleLineHeight) * 0.5f; // center foldout in the row since we also center content. See RowGUI
			extraSpaceBeforeIconAndLabel = kToggleWidth;
			multicolumnHeader.sortingChanged += OnSortingChanged;
			
			Reload();
		}


		// Note we We only build the visible rows, only the backend has the full tree information. 
		// The treeview only creates info for the row list.
		protected override IList<TreeViewItem> BuildRows(TreeViewItem root)
		{
			var rows = base.BuildRows (root);
			SortIfNeeded (root, rows);
			return rows;
		}

		public override void Search(SaveData searchFromThis, string search, List<TreeViewItem> result)
		{
			if (string.IsNullOrEmpty(search))
				throw new ArgumentException("Invalid search: cannot be null or empty", "search");

			const int kItemDepth = 0; // tree is flattened when searching

			Stack<SaveData> stack = new Stack<SaveData>();
			foreach (var element in searchFromThis.children)
				stack.Push((SaveData)element);
			while (stack.Count > 0)
			{
				SaveData current = stack.Pop();
				// Matches search?
				if (current.Key.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0)
				{
					result.Add(new TreeViewItem<SaveData>(current.id, kItemDepth, current.name, current));
				}

				if (current.children != null && current.children.Count > 0)
				{
					foreach (var element in current.children)
					{
						stack.Push((SaveData)element);
					}
				}
			}
			SortSearchResult(result);
		}

		void OnSortingChanged (MultiColumnHeader multiColumnHeader)
		{
			SortIfNeeded (rootItem, GetRows());
		}

		void SortIfNeeded (TreeViewItem root, IList<TreeViewItem> rows)
		{
			if (rows.Count <= 1)
				return;
			
			if (multiColumnHeader.sortedColumnIndex == -1)
			{
				return; // No column to sort for (just use the order the data are in)
			}
			
			// Sort the roots of the existing tree items
			SortByMultipleColumns ();
			TreeToList(root, rows);
			Repaint();
		}

		void SortByMultipleColumns ()
		{
			var sortedColumns = multiColumnHeader.state.sortedColumns;

			if (sortedColumns.Length == 0)
				return;

			var myTypes = rootItem.children.Cast<TreeViewItem<SaveData> >();
			var orderedQuery = InitialOrder (myTypes, sortedColumns);
			for (int i=1; i<sortedColumns.Length - 1; i++)
			{
				SortOption sortOption = m_SortOptions[sortedColumns[i]];
				bool ascending = multiColumnHeader.IsSortedAscending(sortedColumns[i]);

				switch (sortOption)
				{
					case SortOption.Data:
						orderedQuery = orderedQuery.ThenBy(l => l.data.Data, ascending);
						break;
					case SortOption.Key:
						orderedQuery = orderedQuery.ThenBy(l => l.data.Key, ascending);
						break;
					case SortOption.Type:
						orderedQuery = orderedQuery.ThenBy(l => l.data.Type, ascending);
						break;
					case SortOption.KeySize:
						orderedQuery = orderedQuery.ThenBy(l => l.data.KeySize, ascending);
						break;
					case SortOption.Actions:
						return;
				}
			}

			rootItem.children = orderedQuery.Cast<TreeViewItem> ().ToList ();
		}

		IOrderedEnumerable<TreeViewItem<SaveData>> InitialOrder(IEnumerable<TreeViewItem<SaveData>> myTypes, int[] history)
		{
			SortOption sortOption = m_SortOptions[history[0]];
			bool ascending = multiColumnHeader.IsSortedAscending(history[0]);
			switch (sortOption)
			{
				case SortOption.Data:
					return myTypes.Order(l => l.data.Data, ascending);
				case SortOption.Key:
					return myTypes.Order(l => l.data.Key, ascending);
				case SortOption.Type:
					return myTypes.Order(l => l.data.Type.ToString(), ascending);
				case SortOption.KeySize:
					return myTypes.Order(l => l.data.KeySize.ToString(), ascending);
			}

			// default
			return myTypes.Order(l => l.data.name, ascending);
		}
		

		protected override void RowGUI (RowGUIArgs args)
		{
			var item = (TreeViewItem<SaveData>) args.item;

			for (int i = 0; i < args.GetNumVisibleColumns (); ++i)
			{
				CellGUI(args.GetCellRect(i), item, (MyColumns)args.GetColumn(i), ref args);
			}
		}
		

		void CellGUI (Rect cellRect, TreeViewItem<SaveData> item, MyColumns column, ref RowGUIArgs args)
		{
			// Center cell rect vertically (makes it easier to place controls, icons etc in the cells)

			CenterRectUsingSingleLineHeight(ref cellRect);

			switch (column)
			{
				case MyColumns.Key:
				{
					
					GUI.Label(cellRect, item.data.Key);
					break;
				}
				case MyColumns.Data:
				{
					item.data.Data = GUI.TextField(cellRect, item.data.Data);
					break;
				}
				case MyColumns.Type:
				{
					GUI.Label(cellRect, item.data.Type.ToString());
					break;
				}
				case MyColumns.KeySize:
					GUI.Label(cellRect, item.data.KeySize.ToString());
					break;
				case MyColumns.Actions:
					//GUILayout.BeginHorizontal();
					var left = cellRect;
					left.width /= 2;
					var right = new Rect();
					right.Set(left.width + left.x + 5, left.y, left.width, left.height);

					var defaultColor = GUI.contentColor; 
					GUI.backgroundColor = Color.green;
					if (GUI.Button(left, "↺"))
					{
						item.data.Update();
					}
					GUI.backgroundColor = Color.red;
					if (GUI.Button(right, "×"))
					{
						item.data.Delete();
					}
					GUI.backgroundColor = defaultColor;
					//GUILayout.EndHorizontal();
					break;
			}
			
		}
		

		// Misc
		//--------

		protected override bool CanMultiSelect (TreeViewItem item)
		{
			return true;
		}

		public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth)
		{
			var columns = new[] 
			{
				new MultiColumnHeaderState.Column 
				{
					headerContent = new GUIContent("Key"),
					headerTextAlignment = TextAlignment.Left,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Center,
					width = 150, 
					minWidth = 60,
					autoResize = false,
					allowToggleVisibility = false
				},
				new MultiColumnHeaderState.Column 
				{
					headerContent = new GUIContent("Data"),
					headerTextAlignment = TextAlignment.Right,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Left,
					width = 110,
					minWidth = 60,
					autoResize = true
				},
				new MultiColumnHeaderState.Column 
				{
					headerContent = new GUIContent("Type"),
					headerTextAlignment = TextAlignment.Right,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Left,
					width = 95,
					minWidth = 60,
					autoResize = true,
					allowToggleVisibility = true
				},
				new MultiColumnHeaderState.Column 
				{
					headerContent = new GUIContent("KeySize"),
					headerTextAlignment = TextAlignment.Right,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Left,
					width = 70,
					minWidth = 60,
					autoResize = true
				},
				new MultiColumnHeaderState.Column 
				{
					headerContent = new GUIContent("Actions"),
					headerTextAlignment = TextAlignment.Right,
					sortedAscending = true,
					sortingArrowAlignment = TextAlignment.Left,
					width = 70,
					minWidth = 60,
					autoResize = true
				}
			};

			Assert.AreEqual(columns.Length, Enum.GetValues(typeof(MyColumns)).Length, "Number of columns should match number of enum values: You probably forgot to update one of them.");

			var state =  new MultiColumnHeaderState(columns);
			return state;
		}
	}

	static class MyExtensionMethods
	{
		public static IOrderedEnumerable<T> Order<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, bool ascending)
		{
			if (ascending)
			{
				return source.OrderBy(selector);
			}
			else
			{
				return source.OrderByDescending(selector);
			}
		}

		public static IOrderedEnumerable<T> ThenBy<T, TKey>(this IOrderedEnumerable<T> source, Func<T, TKey> selector, bool ascending)
		{
			if (ascending)
			{
				return source.ThenBy(selector);
			}
			else
			{
				return source.ThenByDescending(selector);
			}
		}
	}
}
