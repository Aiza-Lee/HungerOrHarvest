using NsEcsFrame.Core;
using UnityEngine;

namespace GameLogic.Features.SpeedControl {
	[System.Serializable]
	public class SpeedControlConfigResource : IResource {

		[Header("按下数字键1、2、3、4时的速度设置")]

		public float Key1Speed = 1f;
		public float Key2Speed = 2f;
		public float Key3Speed = 3f;
		public float Key4Speed = 4f;
	}
}