using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace FileExplorer.UI {

	public class ListUI : UIBase {

		public ListColumn fileColumnPrefab;
		public ListColumn directoryColumnPrefab;
		public Color columnColor1;
		public Color columnColor2;
		public Color columnHighlightColor;
		public RectTransform contentTrans;
		public float indent;

		private float _stepY;

		private List<ListColumn> _columns;
		private Dictionary<ListColumn, ColumnRange> _columnsToChildren;

		private ListColumn _highlightColumn;




		public ListColumn highlightColumn {
			get {return _highlightColumn;}
			set {
				if (_highlightColumn != null) {
					int previousActiveColumnIndex = _columns.IndexOf(_highlightColumn);
					_highlightColumn.color = ChooseColumnColor(previousActiveColumnIndex);
				}

				if (value != null) {
					_highlightColumn = value;
					_highlightColumn.color = columnHighlightColor;

					_controller.OnFileHighlighted(_highlightColumn.path);

					_activeFilePath = _highlightColumn.path;
				}
			}
		}
		

		protected override void Start () {
			_columns = new List<ListColumn>();
			_columnsToChildren = new Dictionary<ListColumn, ColumnRange>();

			ListColumn.indentPerLevel = indent;

			_stepY = fileColumnPrefab.GetComponent<RectTransform>().rect.height;

			CreateColumns(null);

			base.Start();
		}

		public void CreateColumns (ListColumn parent) {
			float currentY = 0;
			int columnIndex = 0;
			int indentLevel = 0;
			string directoryPath = parent == null ? Utilities.GetUserRoot() : parent.path;

			List<FileSystemInfo> filesAndDirectories = Utilities.GetFilesInDirectory(directoryPath);

			if (parent != null) {
				currentY = parent.localPositionY - _stepY;
				columnIndex = _columns.IndexOf(parent) + 1;
				indentLevel = parent.indentLevel + 1;

				ColumnRange range = new ColumnRange();
				if (filesAndDirectories.Count > 0) {
					range.min = columnIndex;
					range.count = filesAndDirectories.Count;
				}
				else {
					range.min = -1;
				}

				if (range.CheckValid()) {
					_columnsToChildren.Add(parent, range);
				}
			}
			else {
				_columns.Clear();
			}
		
			foreach (FileSystemInfo fileOrDirectory in filesAndDirectories) {
				ListColumn column = null;
				
				if ((fileOrDirectory.Attributes & FileAttributes.Directory) == FileAttributes.Directory) {
					column = GameObject.Instantiate(directoryColumnPrefab) as ListColumn;
				}
				else {
					column = GameObject.Instantiate(fileColumnPrefab) as ListColumn;
				}

				column.name = fileOrDirectory.Name + " Column";
				column.text = fileOrDirectory.Name;
				column.path = fileOrDirectory.FullName;
				column.parent = parent;
				column.indentLevel = indentLevel;
				column.ui = this;
				
				column.transform.SetParent(contentTrans);
				column.transform.localPosition = new Vector3 (0, currentY, 0);
				
				currentY -= _stepY;

				_columns.Insert(columnIndex++, column);
			}

			for (int i=columnIndex; i<_columns.Count; i++) {
				_columns[i].transform.localPosition = new Vector3(0, currentY, 0);
				currentY -= _stepY;
			}

			SetColumnsColor();

			contentTrans.sizeDelta = new Vector2(contentTrans.sizeDelta.x, Mathf.Abs(currentY));
		}

		public void DeleteColumns (ListColumn parent) {
			if (!_columnsToChildren.ContainsKey(parent)) return;

			ColumnRange range;
			if (!_columnsToChildren.TryGetValue(parent, out range)) return;

			if (!range.CheckValid()) return;

			for (int i=range.min; i<range.min + range.count; i++) {
				DeleteColumns(_columns[i]);
			}

			float currentY = _columns[range.min].localPositionY;

			List<ListColumn> columnsToRemove = new List<ListColumn>();
			for (int i=range.min; i<range.min + range.count; i++) {
				columnsToRemove.Add(_columns[i]);
			}
			_columns.RemoveRange(range.min, range.count);

			for (int i=columnsToRemove.Count-1; i>=0; i--) {
				Destroy(columnsToRemove[i].gameObject);
			}

			for (int i=range.min; i<_columns.Count; i++) {
				ListColumn column = _columns[i];
				column.transform.localPosition = new Vector3(0, currentY, 0);
				currentY -= _stepY;
			}

			contentTrans.sizeDelta = new Vector2(contentTrans.sizeDelta.x, Mathf.Abs(currentY));

			_columnsToChildren.Remove(parent);

			SetColumnsColor();
		}

		private void SetColumnsColor () {
			for (int i=0; i<_columns.Count; i++) {
				ListColumn column = _columns[i];
				column.color = ChooseColumnColor(i);
			}
		}

		private Color ChooseColumnColor (int index) {
			return index % 2 == 0 ? columnColor1 : columnColor2;
		}


		private struct ColumnRange {
			public int min;
			public int count;

			public bool CheckValid () {
				return min >= 0;
			}
		}
	}

}