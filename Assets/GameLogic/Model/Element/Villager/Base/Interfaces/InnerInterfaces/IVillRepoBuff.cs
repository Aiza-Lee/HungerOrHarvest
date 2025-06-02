namespace GameLogic.Model.Element.Vill {
	public interface IVillRepoBuff {
		/// <summary>
		/// 村民资源的增加产出buff
		/// </summary>
		RTList<float> ProdBuffs_F { get; }
		/// <summary>
		/// 村民资源的减少消耗buff
		/// </summary>
		RTList<float> ConsBuffs_F { get; }

		/// <summary>
		/// 添加永久的产出buff
		/// </summary>
		void AddProdBuff_Eternal(RTList<float> buffs);
		/// <summary>
		/// 添加永久的消耗buff
		/// </summary>
		void AddConsBuff_Eternal(RTList<float> buffs);
		/// <summary>
		/// 添加临时的产出buff
		/// </summary>
		void AddProdBuff_Temp(RTList<float> buffs, int ticks);
		/// <summary>
		/// 添加临时的消耗buff
		/// </summary>
		void AddConsBuff_Temp(RTList<float> buffs, int ticks);
	}
}