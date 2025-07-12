using NsEcsFrame.Core;

namespace GameLogic.Common.View {
	[System.Serializable]
	public abstract class SmoothChangeStatCompBase<T> : IComponent where T : struct {
		/// <summary>
		/// 是否已经被设置好，准备进行平滑变化
		/// </summary>
		public bool Started;

		public ChangeInfo ChangeInfo;

		public float TotalTime => ChangeInfo.TotalTime;
		public ChangeCurveType CurveType => ChangeInfo.CurveType;
		public bool UseLogicTime => ChangeInfo.UseLogicTime;

		public float ElapsedTime;
		public T StartValue;
		public T TargetValue;

		public SmoothChangeStatCompBase<T> StartAChange(Entity entity, T TargetValue) {
			SetStartValueToCurValue(entity);
			this.TargetValue = TargetValue;
			Started = true;
			ElapsedTime = 0f;
			return this;
		}
		public void StopCurChange() {
			Started = false;
			ElapsedTime = 0f;
		}
		public SmoothChangeStatCompBase<T> SetChangeInfo(ChangeInfo changeInfo) {
			ChangeInfo = changeInfo;
			return this;
		}
		public SmoothChangeStatCompBase(ChangeInfo changeInfo) {
			ChangeInfo = changeInfo;
			Started = false;
			ElapsedTime = 0f;
		}
		public SmoothChangeStatCompBase(float totalTime, ChangeCurveType curveType, bool useLogicTime) {
			ChangeInfo = new ChangeInfo(totalTime, curveType, useLogicTime);
			Started = false;
			ElapsedTime = 0f;
		}

		public abstract void ApplyChange(Entity entity);
		public abstract void SetStartValueToCurValue(Entity entity);
	}

	[System.Serializable]
	public struct ChangeInfo {
		public float TotalTime;
		public ChangeCurveType CurveType;
		public bool UseLogicTime;

		public ChangeInfo(float totalTime, ChangeCurveType curveType, bool useLogicTime) {
			TotalTime = totalTime;
			CurveType = curveType;
			UseLogicTime = useLogicTime;
		}
	}
}