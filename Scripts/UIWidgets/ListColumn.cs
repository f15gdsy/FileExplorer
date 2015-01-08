using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace FileExplorer.UI {

	public class ListColumn : MonoBehaviour {

		public Text columnName;
		public Image icon;
		public Image backgroundImage;
		public Button button;

		public static float indentPerLevel;
		private int _indentLevel;

		protected bool _active = false;

		private RectTransform _rectTrans;


		public Color color {
			get {return backgroundImage.color;}
			set {backgroundImage.color = value;}
		}

		public string text {
			get {return columnName.text;}
			set {columnName.text = value;}
		}

		public float localPositionY {
			get {
				return _rectTrans.localPosition.y;
			}
		}

		public virtual int indentLevel {
			get {return _indentLevel;} 
			set {
				_indentLevel = value;

				float indent = _indentLevel * indentPerLevel;
				RectTransform iconRectTrans = icon.rectTransform;
				RectTransform textRectTrans = columnName.rectTransform;

				Vector3 iconLocalPos = iconRectTrans.localPosition;
				iconLocalPos.x += indent;
				iconRectTrans.localPosition = iconLocalPos;

				Vector3 textLocalPos = textRectTrans.localPosition;
				textLocalPos.x += indent;
				textRectTrans.localPosition = textLocalPos;
			}
		}

		public string path {get; set;}

		public ListColumn parent {get; set;}

		public ListUI ui {get; set;}

		public RectTransform rectTrans {get {return _rectTrans;}}


		void Awake () {
			_rectTrans = GetComponent<RectTransform>();
		}

		void Start () {
			button.onClick.AddListener(ToggleActive);
		}

		public void ToggleActive () {
			ui.highlightColumn = this;

			if (_active) {
				OnInactive();
			}
			else {
				OnActive();
			}

			_active = !_active;
		}

		protected virtual void OnActive () {

		}

		protected virtual void OnInactive () {

		}

		public override string ToString () {
			return string.Format ("[ListColumn: path={0}, height={1}, indentLevel={2}]", path, localPositionY, indentLevel);
		}
	}

}