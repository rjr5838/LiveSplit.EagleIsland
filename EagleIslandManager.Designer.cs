namespace LiveSplit.EagleIsland
{
	partial class EagleIslandManager
    {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EagleIslandManager));
            this.lblTASOutput = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.lblCoords = new System.Windows.Forms.Label();
            this.lblCoordValues = new System.Windows.Forms.Label();
            this.lblRoomType = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblIntro = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblPos = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblHubEvent = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblOrnisFrozen = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTASOutput
            // 
            this.lblTASOutput.AutoSize = true;
            this.lblTASOutput.Location = new System.Drawing.Point(97, 132);
            this.lblTASOutput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTASOutput.Name = "lblTASOutput";
            this.lblTASOutput.Size = new System.Drawing.Size(296, 32);
            this.lblTASOutput.TabIndex = 22;
            this.lblTASOutput.Text = "P1-L999(999,R,J,S)(998 / 999 | 9999)\nP1-L999(999,R,J,S)(998 / 999 | 9999)";
            this.lblTASOutput.Visible = false;
            // 
            // menu
            // 
            this.menu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menu.AutoSize = false;
            this.menu.Dock = System.Windows.Forms.DockStyle.None;
            this.menu.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menu.Size = new System.Drawing.Size(437, 24);
            this.menu.TabIndex = 24;
            // 
            // lblCoords
            // 
            this.lblCoords.AutoSize = true;
            this.lblCoords.Location = new System.Drawing.Point(36, 24);
            this.lblCoords.Name = "lblCoords";
            this.lblCoords.Size = new System.Drawing.Size(64, 16);
            this.lblCoords.TabIndex = 25;
            this.lblCoords.Text = "(x, y):";
            // 
            // lblCoordValues
            // 
            this.lblCoordValues.AutoSize = true;
            this.lblCoordValues.Location = new System.Drawing.Point(106, 24);
            this.lblCoordValues.Name = "lblCoordValues";
            this.lblCoordValues.Size = new System.Drawing.Size(56, 16);
            this.lblCoordValues.TabIndex = 26;
            this.lblCoordValues.Text = "(?, ?)";
            // 
            // lblRoomType
            // 
            this.lblRoomType.AutoSize = true;
            this.lblRoomType.Location = new System.Drawing.Point(106, 40);
            this.lblRoomType.Name = "lblRoomType";
            this.lblRoomType.Size = new System.Drawing.Size(16, 16);
            this.lblRoomType.TabIndex = 28;
            this.lblRoomType.Text = "?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "room type:";
            // 
            // lblIntro
            // 
            this.lblIntro.AutoSize = true;
            this.lblIntro.Location = new System.Drawing.Point(106, 56);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(16, 16);
            this.lblIntro.TabIndex = 30;
            this.lblIntro.Text = "?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 29;
            this.label3.Text = "intro:";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(106, 106);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(16, 16);
            this.lblMessage.TabIndex = 31;
            this.lblMessage.Text = "0";
            // 
            // lblPos
            // 
            this.lblPos.AutoSize = true;
            this.lblPos.Location = new System.Drawing.Point(255, 24);
            this.lblPos.Name = "lblPos";
            this.lblPos.Size = new System.Drawing.Size(56, 16);
            this.lblPos.TabIndex = 33;
            this.lblPos.Text = "(?, ?)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "(x, y):";
            // 
            // lblHubEvent
            // 
            this.lblHubEvent.AutoSize = true;
            this.lblHubEvent.Location = new System.Drawing.Point(106, 90);
            this.lblHubEvent.Name = "lblHubEvent";
            this.lblHubEvent.Size = new System.Drawing.Size(32, 16);
            this.lblHubEvent.TabIndex = 34;
            this.lblHubEvent.Text = "n/a";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 35;
            this.label1.Text = "Ornis Frozen: ";
            // 
            // lblOrnisFrozen
            // 
            this.lblOrnisFrozen.AutoSize = true;
            this.lblOrnisFrozen.Location = new System.Drawing.Point(311, 90);
            this.lblOrnisFrozen.Name = "lblOrnisFrozen";
            this.lblOrnisFrozen.Size = new System.Drawing.Size(32, 16);
            this.lblOrnisFrozen.TabIndex = 36;
            this.lblOrnisFrozen.Text = "n/a";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 37;
            this.label5.Text = "Hub Event:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 106);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 38;
            this.label6.Text = "Room Type:";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(311, 106);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(32, 16);
            this.lblLevel.TabIndex = 40;
            this.lblLevel.Text = "n/a";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(185, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 39;
            this.label8.Text = "Level:";
            // 
            // EagleIslandManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(437, 131);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblOrnisFrozen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblHubEvent);
            this.Controls.Add(this.lblPos);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblIntro);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblRoomType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCoordValues);
            this.Controls.Add(this.lblCoords);
            this.Controls.Add(this.lblTASOutput);
            this.Controls.Add(this.menu);
            this.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1500, 209);
            this.MinimumSize = new System.Drawing.Size(425, 170);
            this.Name = "EagleIslandManager";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Eagle Island Manager";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EagleIslandManager_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label lblTASOutput;
		private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.Label lblCoords;
        private System.Windows.Forms.Label lblCoordValues;
        private System.Windows.Forms.Label lblRoomType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblPos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblHubEvent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOrnisFrozen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label label8;
    }
}