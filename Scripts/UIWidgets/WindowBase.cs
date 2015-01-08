using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FileExplorer.Ex;


namespace FileExplorer.UI {

	// WindowBase is the base class for window.
	public class WindowBase : MonoBehaviour {

		// ----- UI elements that are required -----
		public Button cancelButton;
		public Button otherButton;

		protected WindowController _controller;

		protected string _activeFilePath;

	
		public string activeFilePath {get {return _activeFilePath;}}


		protected virtual void Start () {
			_controller.OnStart();
		}

		public void RegisterWindowController (WindowController controller) {
			_controller = controller;
			_controller.window = this;

			_controller.RegisterCancelButton(cancelButton);
			_controller.RegisterOtherButton(otherButton);
		}
	}

}