using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.EagleIsland.Settings;
namespace LiveSplit.EagleIsland
{
	public partial class EagleIslandSettings : UserControl {
		public List<EagleIslandSplit> Splits { get; private set; }
		public bool ShowMapDisplay { get; set; }
		public bool RainbowDash { get; set; }
		public bool AlphaSort { get; set; }
		private EagleIslandComponent component;
		private bool isLoading;
		public EagleIslandSettings(EagleIslandComponent comp) {
			isLoading = true;
			InitializeComponent();

			component = comp;
			Splits = new List<EagleIslandSplit>();
			Splits.Add(new EagleIslandSplit("Start Game", "True"));
			isLoading = false;
		}

		private void EagleIslandSettings_Load(object sender, EventArgs e) {
			LoadSettings();
		}
		public void LoadSettings() {
			isLoading = true;
			this.flowMain.SuspendLayout();

			for (int i = flowMain.Controls.Count - 1; i > 0; i--) {
				Control c = flowMain.Controls[i];
				if (c is EagleIslandSplitSettings) {
					RemoveHandlers((EagleIslandSplitSettings)c);
					flowMain.Controls.RemoveAt(i);
				}
			}

			foreach (EagleIslandSplit split in Splits) {
				EagleIslandSplitSettings setting = new EagleIslandSplitSettings();
				setting.cboName.DisplayMember = "SplitName";
				setting.cboName.ValueMember = "LevelEnum";
				setting.cboName.DataSource = SplitComboData();
				setting.cboName.Text = split.Field;
				setting.cboName.SelectedValue = split.Value;
				setting.chkShouldSplit.Checked = split.ShouldSplit;
				AddHandlers(setting);
				flowMain.Controls.Add(setting);
			}

			isLoading = false;
			this.flowMain.ResumeLayout(true);
		}
		private void AddHandlers(EagleIslandSplitSettings setting) {
			setting.cboName.SelectedIndexChanged += new EventHandler(cboName_SelectedIndexChanged);
			setting.btnRemove.Click += new EventHandler(btnRemove_Click);
			setting.chkShouldSplit.CheckedChanged += new EventHandler(chkBox_CheckedChanged);
		}
		private void RemoveHandlers(EagleIslandSplitSettings setting) {
			setting.cboName.SelectedIndexChanged -= cboName_SelectedIndexChanged;
			setting.btnRemove.Click -= btnRemove_Click;
		}
		public void btnRemove_Click(object sender, EventArgs e) {
			for (int i = flowMain.Controls.Count - 1; i > 0; i--) {
				if (flowMain.Controls[i].Contains((Control)sender)) {
					RemoveHandlers((EagleIslandSplitSettings)((Button)sender).Parent);

					flowMain.Controls.RemoveAt(i);
					break;
				}
			}
			UpdateSplits();
		}
		public void cboName_SelectedIndexChanged(object sender, EventArgs e) {
			UpdateSplits();
		}
		private void chkBox_CheckedChanged(object sender, EventArgs e) {
			UpdateSplits();
		}
		public void UpdateSplits() {
			if (isLoading) return;

			Splits.Clear();
			foreach (Control c in flowMain.Controls) {
				if (c is EagleIslandSplitSettings) {
					EagleIslandSplitSettings setting = (EagleIslandSplitSettings)c;
					if (!string.IsNullOrEmpty(setting.cboName.Text)) {
						EagleIslandSplit split = new EagleIslandSplit(setting.cboName.Text, setting.cboName.SelectedValue.ToString(), setting.chkShouldSplit.Checked);
						Splits.Add(split);
					}
				}
			}
		}
		public XmlNode UpdateSettings(XmlDocument document) {
			XmlElement xmlSettings = document.CreateElement("Settings");

			XmlElement xmlSplits = document.CreateElement("Splits");
			xmlSettings.AppendChild(xmlSplits);

			foreach (EagleIslandSplit split in Splits) {
				XmlElement xmlSplit = document.CreateElement("Split");
				xmlSplit.InnerText = split.Field;

				XmlAttribute att = document.CreateAttribute("LevelEnum");
				att.Value = split.Value;
				xmlSplit.Attributes.Append(att);

				att = document.CreateAttribute("ShouldSplit");
				att.Value = split.ShouldSplit.ToString();
				xmlSplit.Attributes.Append(att);

				xmlSplits.AppendChild(xmlSplit);
			}
			return xmlSettings;
		}
		public void SetSettings(XmlNode settings) {
			Splits.Clear();
			XmlNodeList splitNodes = settings.SelectNodes(".//Splits/Split");
			foreach (XmlNode splitNode in splitNodes) {
				string name = splitNode.InnerText;
				string value = splitNode.Attributes["LevelEnum"].Value;
				bool shouldSplit = bool.Parse(splitNode.Attributes["ShouldSplit"].Value);
				Splits.Add(new EagleIslandSplit(name, value, shouldSplit));
			}
		}
		private void btnAddSplit_Click(object sender, EventArgs e) {
			EagleIslandSplitSettings setting = new EagleIslandSplitSettings();
			setting.cboName.DisplayMember = "SplitName";
			setting.cboName.ValueMember = "LevelEnum";
			setting.cboName.DataSource = SplitComboData();
			setting.cboName.Text = "Start Game";
			setting.chkShouldSplit.Checked = true;
			AddHandlers(setting);

			flowMain.Controls.Add(setting);
			UpdateSplits();
		}
		public DataTable SplitComboData() {
			DataTable dt = new DataTable();
			dt.Columns.Add("SplitName", typeof(string));
			dt.Columns.Add("LevelEnum", typeof(string));
			foreach (var pair in EagleIslandSplitSettings.AvailableSplits) {
				dt.Rows.Add(pair.Key, pair.Value);
			}
			return dt;
		}
		private void flowMain_DragDrop(object sender, DragEventArgs e) {
			UpdateSplits();
		}
		private void flowMain_DragEnter(object sender, DragEventArgs e) {
			e.Effect = DragDropEffects.Move;
		}
		private void flowMain_DragOver(object sender, DragEventArgs e) {
			EagleIslandSplitSettings data = (EagleIslandSplitSettings)e.Data.GetData(typeof(EagleIslandSplitSettings));
			FlowLayoutPanel destination = (FlowLayoutPanel)sender;
			Point p = destination.PointToClient(new Point(e.X, e.Y));
			var item = destination.GetChildAtPoint(p);
			int index = destination.Controls.GetChildIndex(item, false);
			if (index == 0) {
				e.Effect = DragDropEffects.None;
			} else {
				e.Effect = DragDropEffects.Move;
				int oldIndex = destination.Controls.GetChildIndex(data);
				if (oldIndex != index) {
					destination.Controls.SetChildIndex(data, index);
					destination.Invalidate();
				}
			}
		}
	}
}