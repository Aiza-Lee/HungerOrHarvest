using System;
using UnityEngine;

namespace GameLogic.Controller
{
	public static class ParamConverter {
		public static bool TryDefaultConvert<T>(string str, out T result) {
			try {
				result = (T)Convert.ChangeType(str, typeof(T)); 
				return true;
			} catch {
				result = default;
				return false;
			}
		}
		public static bool TryConvertToEnum<T>(string str, out T result) where T : Enum {
			try {
				result = (T)Enum.Parse(typeof(T), str); 
				return true;
			} catch {
				result = default;
				return false;
			}
		}
		public static bool TryConvertToCoord(string str, out Coord result) {
			var strs = str.Split(',');

			if (
				strs.Length == 2 
				&& strs[0].StartsWith('(') && strs[1].EndsWith(')') 
				&& int.TryParse(strs[0][1..], out var x) && int.TryParse(strs[1][..^1], out var y)
			) {
				result = new Coord(x, y); 
				return true;
			}

			result = default;
			return false;
		}

		public static bool TryConvertToOL(string str, out OL result) {
			var strs = str.Split(',');

			if (
				strs.Length == 2 
				&& strs[0].StartsWith('[') && strs[1].EndsWith(']') 
				&& int.TryParse(strs[0][1..], out var x) && int.TryParse(strs[1][..^1], out var y)
			) {
				result = new OL(x, y); 
				return true;
			}

			result = default;
			return false;
		}
	}
}