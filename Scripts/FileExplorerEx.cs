using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using FileExplorer.UI;

namespace FileExplorer.Ex {

	// FileExplorerEx provides interfaces for other applications to use.
	public class FileExplorerEx {

		private static GameObject _windowPrefab;
		private static GameObject _windowGo;
		private static GameObject _canvasGo;
		private static GameObject _eventSystemGo;


		// Used to open a File Explorer window.
		// 	controller: a customized controller to responds to window UI interaction.
		//	style: wanted window style.
		public static void Open (WindowController controller, WindowStyle style = WindowStyle.Default) {
			if (_windowPrefab == null) {
				string prefabPath;

				switch (style) {
				case WindowStyle.List:
					prefabPath = "Prefabs/File Explorer List Window";
					break;

				default:
					prefabPath = "Prefabs/File Explorer List Window";
					break;
				}

				_windowPrefab = Resources.Load(prefabPath) as GameObject;
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

			_windowGo = GameObject.Instantiate(_windowPrefab) as GameObject;
			_windowGo.transform.SetParent(_canvasGo.transform);
			_windowGo.transform.localPosition = Vector3.zero;		// TODO: leave an interface for positioning the window?

			WindowBase window = _windowGo.GetComponent<WindowBase>();
			window.RegisterWindowController(controller);
		}

		// Close the window.
		public static void Close () {
			GameObject.Destroy(_windowGo);
		}
	}


	public enum WindowStyle {
		Default = 0,
		List = 1,
	}
}