using System;
using System.Collections.Generic;
using NSFrame;

namespace GameLogic.Model.Element.Vill {
	/// <summary>
	/// 状态基类
	/// <para>一帧中会先判断转移再执行逻辑</para>
	/// </summary>
	public abstract class StateBase : IPooledObject {
		protected LogicImpler _impler;
		public StateBase Init(LogicImpler impler) {
			_impler = impler;
			return this;
		}
		protected RepoBuffHelper RepoBuffHelper => _impler.RepoBuffHelper;
		protected BondArchHelper BondArchHelper => _impler.BondArchHelper;
		protected IVitHelper VitHelper => _impler.VitHelper;
		protected ExpHelper ExpHelper => _impler.ExpHelper;
		protected IStateMachine StateMachine => _impler.StateMachine;

		abstract public State StaType { get; }
		/// <summary>
		/// 状态转移判定条件
		/// </summary>
		abstract public List<Pair<Func<bool>, State>> Transitions { get; }
		/// <summary>
		/// 销毁对象，返回对象池
		/// </summary>
		virtual public void LogicDestroy() {
			PoolSystem.PushObj(GetType(), this);
		}

		/// <summary>
		/// 状态开始时调用
		/// </summary>
		abstract public void OnEnter();
		/// <summary>
		/// 状态结束时调用
		/// </summary>
		abstract public void OnEnd();
		/// <summary>
		/// 执行状态逻辑
		/// </summary>
		abstract public void Execute();

		public void InitAfterPop() {}
		public void CleanBeforePush() {
			_impler = null;
		}
	}
}