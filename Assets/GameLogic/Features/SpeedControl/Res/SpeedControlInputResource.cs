using NsEcsFrame.Core;

namespace GameLogic.Features.SpeedControl {
	public class SpeedControlInputResource : IResource {
		public bool EnabledInput;

		public bool Speed01KeyDown;
		public bool Speed02KeyDown;
		public bool Speed03KeyDown;
		public bool Speed04KeyDown;

		public bool PauseKeyDown;
	}
}