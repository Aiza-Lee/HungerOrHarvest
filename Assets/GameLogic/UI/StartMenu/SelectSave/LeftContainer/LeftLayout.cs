using System.Collections.Generic;
using GameLogic.UI.Common.UiComponents.GroupLayout;

namespace GameLogic.UI.StartMenu {
	public class LeftLayout : ScrollGroupLayoutBase {
		/// <summary>
		/// 重新加载所有world选项
		/// </summary>
		/// <param name="worldNames"> pair:<worldHash, worldName> </param>
		public void ResetContent(List<string> worldNames) {
			base.Clear();
			worldNames.ForEach(name => base.AddEle(ButtonFactory.Inst.CreateWorldButton(name)));
		}
		public void ChooseFirstWorld() {
			if (base._eles.Count > 0) {
				(base._eles[0] as WorldButtonEle).OnClicked();
			}
		}
	}
}