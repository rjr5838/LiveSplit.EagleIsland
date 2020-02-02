using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
namespace LiveSplit.EagleIsland {
	public partial class EagleIslandManager : Form {
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();
		public EagleIslandMemory Memory { get; set; }
		public EagleIslandComponent Component { get; set; }
		private Thread getValuesThread = null;
		private KeyboardHook keyboard = new KeyboardHook();
		public bool AlwaysShown { get; set; }

		public EagleIslandManager(bool shown) {
			InitializeComponent();
			Text = "Eagle Island Manager " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
			AlwaysShown = shown;
			Visible = shown;
			Memory = new EagleIslandMemory();
			getValuesThread = new Thread(UpdateLoop);
			getValuesThread.IsBackground = true;
			getValuesThread.Start();
		}

		private void EagleIslandManager_FormClosing(object sender, FormClosingEventArgs e) {
			e.Cancel = Memory != null && !AlwaysShown;
			if (!e.Cancel) {
				if (getValuesThread != null) {
					getValuesThread = null;
				}
			}
		}
		private void UpdateLoop() {
			while (getValuesThread != null) {
				try {
					keyboard.Poll();
					UpdateValues();
					Thread.Sleep(15);
				} catch { }
			}
		}
		public void UpdateValues() {
			if (this.InvokeRequired)
			{
				this.Invoke((Action)UpdateValues);
			}
			else if (Memory != null && Memory.HookProcess())
			{
				if (!Visible) { this.Show(); }

				lblCoordValues.Text = "(" + Memory.GetCoordX() + "," + Memory.GetCoordY() + ")";
				lblRoomType.Text = Memory.GetRoomType().ToString();
				lblIntro.Text = Memory.GetIntroPanelsActive().ToString();
				lblPos.Text = "(" + Memory.GetPosX() + "," + Memory.GetPosY() + ")";
				lblHubEvent.Text = Memory.GetHubEvent().ToString();
				lblOrnisFrozen.Text = Memory.GetOrnisFrozen().ToString();
				lblLevel.Text = Memory.GetLevel().ToString();
			}
		}
	}
}