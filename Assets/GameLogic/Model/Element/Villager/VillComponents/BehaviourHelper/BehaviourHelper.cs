using GameLogic.Model.Mgr;
using GameLogic.Utilities;
using NSFrame.BehaviourTree;

namespace GameLogic.Model.Element.Vill {
	public class BehaviourHelper : IBehaviourHelper, ISaveable<BehaviourHelperSave> {
		private readonly LogicImpler _impler;
		private readonly MyBlackboard _blackBoard;
		private readonly BehaviourTree<MyBlackboard> _behaviourTree;

		public string CurStateDescription => string.Empty;

		public BehaviourHelper(LogicImpler impler, MyBlackboard blackboard, BehaviourTree<MyBlackboard> behaviourTree) {
			_impler = impler;
			_impler.TickUpdate += Think;
			_blackBoard = blackboard;
			_behaviourTree = behaviourTree;
		}

		private void RecordLastTickInfo() {
			_blackBoard.LastTickHomeID = _impler.BondArchHelper.HomeID;
			_blackBoard.LastTickBondedWorkArchID = _impler.BondArchHelper.BondedWorkArchID;
			_blackBoard.LastTickInDay = LogicTimeMgr.Inst.IsDay;
		}

		public void Think() {
			_behaviourTree.Think();
			RecordLastTickInfo();
		}

		public void Reset() {
			_behaviourTree.Reset();
		}

		public BehaviourHelperSave GetSave() {
			return new BehaviourHelperSave {
				BlackBoardSave = _blackBoard.GetSave()
			};
		}

		public void InitFromSave(BehaviourHelperSave save) {
			_blackBoard.InitFromSave(save.BlackBoardSave);
		}
	}
}