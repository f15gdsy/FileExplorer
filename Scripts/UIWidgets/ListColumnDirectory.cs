using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace FileExplorer.UI {

	public class ListColumnDirectory : ListColumn {
		public RectTransform indicatorTrans;




		public override int indentLevel {
			get {
				return base.indentLevel;
			}
			set {
				base.indentLevel = value;

				float indent = indentLevel * indentPerLevel;
				Vector3 indicatorLocalPos = indicatorTrans.localPosition;
				indicatorLocalPos.x += indent;
				indicatorTrans.localPosition = indicatorLocalPos;
			}
		}

		protected override void OnActive () {
			ui.CreateColumns(this);
			PlayIndicatorAnimationOpen();

			ui.highlightColumn = null;
		}

		protected override void OnInactive () {
			ui.DeleteColumns(this);
			PlayIndicatorAnimationClose();
		}

		private void PlayIndicatorAnimationOpen () {
			indicatorTrans.localEulerAngles = new Vector3(0, 0, -90);
		}

		private void PlayIndicatorAnimationClose () {
			indicatorTrans.localEulerAngles = new Vector3(0, 0, 0);
		}
	}

}