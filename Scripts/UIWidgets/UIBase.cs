using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FileExplorer.Ex;


namespace FileExplorer.UI {

	public class UIBase : MonoBehaviour {

		// ----- UI elements that are required -----
		public Button cancelButton;
		public Button otherButton;

		protected UIController _controller;

		protected string _activeFilePath;

	
		public string activeFilePath {get {return _activeFilePath;}}


		protected virtual void Start () {
			_controller.OnStart();
		}

		public void RegisterUIController (UIController uiController) {
			_controller = uiController;
			_controller.ui = this;

			_controller.RegisterCancelButton(cancelButton);
			_controller.RegisterOtherButton(otherButton);
		}
	}

}