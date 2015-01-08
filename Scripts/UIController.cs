using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using FileExplorer.UI;

namespace FileExplorer.Ex {

	public class UIController {

		public UIBase ui {get;set;}


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


		public virtual void OnStart () {}

		protected virtual void OnCancelButtonPressed (Button cancelButton) {
			FileExplorerEx.Close();
		}

		protected virtual void OnOtherButtonPressed (Button otherButton) {}

		public virtual void OnFileHighlighted (string path) {}
	}

}