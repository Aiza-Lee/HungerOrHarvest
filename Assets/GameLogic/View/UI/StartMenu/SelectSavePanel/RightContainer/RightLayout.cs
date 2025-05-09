using System.Collections.Generic;
using NSFrame;

namespace GameLogic.View.UI.StartMenu.SelectSavePanel
{
	public class RightLayout : ScrollGroupLayoutBase {
		
		public void ResetContent(List<SaveInfo> saveInfos) {
			base.Clear();
			saveInfos.ForEach(si => {
				base.AddEle(Factory.Inst.CreateSaveInfoButton(si));
			});
		}
	}
}