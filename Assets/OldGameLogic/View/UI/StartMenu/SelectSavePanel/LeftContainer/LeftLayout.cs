using System.Collections.Generic;

namespace OldGameLogic.View.UI.StartMenu.SelectSavePanel
{
	public class LeftLayout : ScrollGroupLayoutBase {
		/// <summary>
		/// 重新加载所有world选项
		/// </summary>
		/// <param name="worldInfos"> pair:<worldHash, worldName> </param>
		public void ResetContent(List<Pair<string, string>> worldInfos) {
			base.Clear();
			worldInfos.ForEach(
				pi => {
					base.AddEle(Factory.Inst.CreateWorldButton(pi.Key, pi.Value));
				}
			);
		}
		public void ChooseFirstWorld() {
			if (base._eles.Count > 0) {
				(base._eles[0] as WorldButtonEle).OnClicked();
			}
		}
	}
}