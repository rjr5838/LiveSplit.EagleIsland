using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace LiveSplit.EagleIsland {
	public class KalimbaTest {
		private static EagleIslandComponent comp = null;
		private static Dictionary<short, Dictionary<int, int>> data = new Dictionary<short, Dictionary<int, int>>();
		public static void Main(string[] args) {
			var shortComparator = EqualityComparer<short>.Default;
			var intComparator = EqualityComparer<int>.Default;

			try
			{
				comp = new EagleIslandComponent(null);
				System.Windows.Forms.Application.Run(new EagleIslandManager(true));
			} catch { }
		}
	}
}