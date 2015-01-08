using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FileExplorer.UI;

namespace FileExplorer.Ex {

	// WindowController is the base class for customized controller.
	// It is used to respond to window UI interactions.
	public class WindowController {

		public WindowBase window {get;set;}


		public void RegisterCancelButton (Button cancelButton) {
			cancelButton.onClick.AddListener(delegate {
				OnCancelButtonPressed(cancelButton);
			});
		}

		public void RegisterOtherButton (Button otherButton) {
			otherButton.onClick.AddListener(delegate {
				OnOtherButtonPressed(otherButton);
			});
		}

		// Override these functions to customize.
		public virtual void OnStart () {}
		public virtual void OnFileHighlighted (string path) {}
		protected virtual void OnCancelButtonPressed (Button cancelButton) {
			FileExplorerEx.Close();
		}
		protected virtual void OnOtherButtonPressed (Button otherButton) {}
	}

}