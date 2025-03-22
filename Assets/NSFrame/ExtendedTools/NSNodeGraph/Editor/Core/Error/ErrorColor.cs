using UnityEngine;

namespace NSFrame
{
	public class ErrorColor {
		public Color Color { get; set; }
		
		public ErrorColor() {
			GenerateRandomColor();
		}

		public void GenerateRandomColor() {
			Color = new Color32(
				(byte) Random.Range(65, 256),
				(byte) Random.Range(50, 176),
				(byte) Random.Range(50, 176),
				255
			);
		}
	}
}