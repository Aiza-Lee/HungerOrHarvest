
namespace GameLogic.Features.MainCamera {
	public enum CameraSize {
		Focus,
		Normal,
		Wide,
		WideWide,
	}
	public struct MainCameraComponent : NsEcsFrame.Core.IComponent {
		public CameraSize Size;
		public float MoveSpeed;

		public MainCameraComponent(CameraSize size, float moveSpeed) {
			Size = size;
			MoveSpeed = moveSpeed;
		}
	}
}