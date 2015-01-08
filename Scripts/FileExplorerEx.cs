using UnityEngine;
using System.Collections;
using FileExplorer.UI;

namespace FileExplorer.Ex {

	public class FileExplorerEx {

		private static GameObject _uiPrefab;
		private static GameObject _uiGo;
		private static GameObject _canvasGo;


		public static void Open (UIController uiController) {
			if (_uiPrefab == null) {
				_uiPrefab = Resources.Load("Prefabs/File Explorer List UI") as GameObject;
			}
			if (_canvasGo == null) {
				Canvas canvas = GameObject.FindObjectOfType<Canvas>();

				if (canvas == null) {
					_canvasGo = new GameObject("Canvas");
					_canvasGo.AddComponent<Canvas>();
				}
				else {
					_canvasGo = canvas.gameObject;
				}
			}

			_uiGo = GameObject.Instantiate(_uiPrefab) as GameObject;
			_uiGo.transform.SetParent(_canvasGo.transform);
			_uiGo.transform.localPosition = Vector3.zero;

			UIBase ui = _uiGo.GetComponent<UIBase>();
			ui.RegisterUIController(uiController);
		}

		public static void Close () {
			GameObject.Destroy(_uiGo);
		}
	}

}