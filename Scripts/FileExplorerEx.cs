using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using FileExplorer.UI;

namespace FileExplorer.Ex {

	public class FileExplorerEx {

		private static GameObject _uiPrefab;
		private static GameObject _uiGo;
		private static GameObject _canvasGo;
		private static GameObject _eventSystemGo;


		public static void Open (UIController uiController) {
			if (_uiPrefab == null) {
				_uiPrefab = Resources.Load("Prefabs/File Explorer List UI") as GameObject;
			}
			if (_canvasGo == null) {
				Canvas canvas = GameObject.FindObjectOfType<Canvas>();

				if (canvas == null) {
					_canvasGo = new GameObject("Canvas");
					canvas = _canvasGo.AddComponent<Canvas>();
					_canvasGo.AddComponent<CanvasScaler>();
					_canvasGo.AddComponent<GraphicRaycaster>();

					canvas.renderMode = RenderMode.ScreenSpaceOverlay;
				}
				else {
					_canvasGo = canvas.gameObject;
				}
			}

			if (_eventSystemGo == null) {
				EventSystem eventSystem = GameObject.FindObjectOfType<EventSystem>();

				if (eventSystem == null) {
					_eventSystemGo = new GameObject("EventSystem");
					eventSystem = _eventSystemGo.AddComponent<EventSystem>();
					_eventSystemGo.AddComponent<StandaloneInputModule>();
					_eventSystemGo.AddComponent<TouchInputModule>();
				}
				else {
					_eventSystemGo = eventSystem.gameObject;
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