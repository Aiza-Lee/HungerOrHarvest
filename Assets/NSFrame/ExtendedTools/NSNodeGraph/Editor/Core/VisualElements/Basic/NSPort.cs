using System;
using UnityEditor.Experimental.GraphView;

namespace NSFrame
{
	public class NSPort : Port
	{
		public string ID { get; set; }
		public NSPort(Orientation portOrientation, Direction portDirection, Capacity portCapacity, Type type) : base(portOrientation, portDirection, portCapacity, type)
		{
		}
	}
}