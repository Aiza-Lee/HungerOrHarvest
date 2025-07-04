
namespace GameLogic.Features.MainCamera {
	public enum CameraSize {
		Focus,
		Normal,
		Wide,
		WideWide,
	}
	public class MainCameraResource : NsEcsFrame.Core.IResource {
		public CameraSize Size;
		public float MoveSpeed;
	}
}