namespace GameLogic.Model.Element.Vill
{
	/// <summary>
	/// 村民体力值管理器
	/// </summary>
	public class VitHelper : ISaveable<VitHelperSave> {

		private float _vit;

		#region PublicMethods


		#endregion

		#region ISaveable
		public VitHelperSave GetSave() {
			throw new System.NotImplementedException();
		}

		public void InitFromSave(VitHelperSave save) {
			throw new System.NotImplementedException();
		}
		#endregion
	}
}