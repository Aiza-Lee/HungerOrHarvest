using System;
using NsEcsFrame.Core;

namespace Test {
	[Serializable]
	public class TestRes : IResource {
		public int ttt;
		public void CopyFrom(IResource other) {
			if (other is TestRes otherRes) {
				ttt = otherRes.ttt;
			} else {
				throw new System.InvalidCastException("Cannot copy from non-TestRes resource.");
			}
		}
	}
}