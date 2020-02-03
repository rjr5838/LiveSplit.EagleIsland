using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace LiveSplit.EagleIsland.Settings {
	public partial class EagleIslandSplitSettings : UserControl {
		public string ControlType = "";
		private bool isDragging = false;
		private int mX = 0;
		private int mY = 0;
		public static List<string> AnyPercentSplits = new List<string>()
		{
			"Start Game",
			"Greenwood Glade",
			"Gemshine Mines",
			"Smuggleway Bay",
			"Armaura 1",
			"Woodberry Warren",
			"Sanctuary Swamp",
			"Pinnacle Peak",
			"Armaura 2",
			"Hollow Hestia",
			"Falcon's Furnace",
			"Tomb of the Raven",
			"Ornis"
		};

		public static Dictionary<string, string> AvailableSplits = new Dictionary<string, string>()
		{
			{"Start Game",             ""},
			{"Greenwood Glade",        "Glade1"},
			{"Gemshine Mines",         "Mines1"},
			{"Smuggleway Bay",         "Coastal1"},
			{"Armaura 1",              ""},
			{"Woodberry Warren",       "Warren1"},
			{"Sanctuary Swamp",        "Swamp1"},
			{"Pinnacle Peak",          "Peak1"},
			{"Armaura 2",              ""},
			{"Hollow Hestia",          "Jungle1"},
			{"Falcon's Furnace",       "Fire1"},
			{"Tomb of the Raven",      "Tomb1"},
			{"Ornis",                  ""}
		};

		public EagleIslandSplitSettings() {
			InitializeComponent();
		}

		private void picHandle_MouseMove(object sender, MouseEventArgs e) {
			if (!isDragging) {
				if (e.Button == MouseButtons.Left) {
					int num1 = mX - e.X;
					int num2 = mY - e.Y;
					if (((num1 * num1) + (num2 * num2)) > 20) {
						DoDragDrop(this, DragDropEffects.All);
						isDragging = true;
						return;
					}
				}
			}
		}
		private void picHandle_MouseDown(object sender, MouseEventArgs e) {
			mX = e.X;
			mY = e.Y;
			isDragging = false;
		}
	}
}