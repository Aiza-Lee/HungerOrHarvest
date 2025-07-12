using System.Collections.Generic;
using GameLogic.UI.Common.UiComponents.GroupLayout;
using NSFrame;

namespace GameLogic.UI.StartMenu {
	public class RightLayout : ScrollGroupLayoutBase {

		public void ResetContent(List<SaveInfo> saveInfos) {
			base.Clear();
			saveInfos.ForEach(si => {
				base.AddEle(ButtonFactory.Inst.CreateSaveInfoButton(si));
			});
		}
	}
}