namespace zRat
{
    partial class CurrentForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CurrentForm));
            this.ListViewVictims = new System.Windows.Forms.ListView();
            this.IpAddress = new System.Windows.Forms.ColumnHeader();
            this.ComputerName = new System.Windows.Forms.ColumnHeader();
            this.ConnectionTime = new System.Windows.Forms.ColumnHeader();
            this.RightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.remoteDesktopClick = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BeepClicked = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectPressed = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerateLargeFile = new System.Windows.Forms.ToolStripMenuItem();
            this.processManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ConnectionNumber = new System.Windows.Forms.ToolStripStatusLabel();
            this.LockCursorClick = new System.Windows.Forms.ToolStripMenuItem();
            this.RightClickMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListViewVictims
            // 
            this.ListViewVictims.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IpAddress,
            this.ComputerName,
            this.ConnectionTime});
            this.ListViewVictims.ContextMenuStrip = this.RightClickMenu;
            this.ListViewVictims.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListViewVictims.GridLines = true;
            this.ListViewVictims.Location = new System.Drawing.Point(0, 0);
            this.ListViewVictims.MultiSelect = false;
            this.ListViewVictims.Name = "ListViewVictims";
            this.ListViewVictims.Size = new System.Drawing.Size(800, 450);
            this.ListViewVictims.TabIndex = 0;
            this.ListViewVictims.UseCompatibleStateImageBehavior = false;
            this.ListViewVictims.View = System.Windows.Forms.View.Details;
            // 
            // IpAddress
            // 
            this.IpAddress.Text = "IP Address";
            this.IpAddress.Width = 120;
            // 
            // ComputerName
            // 
            this.ComputerName.Text = "Computer";
            this.ComputerName.Width = 250;
            // 
            // ConnectionTime
            // 
            this.ConnectionTime.Text = "Connection Time";
            this.ConnectionTime.Width = 145;
            // 
            // RightClickMenu
            // 
            this.RightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.remoteDesktopClick,
            this.toolStripSeparator1,
            this.BeepClicked,
            this.DisconnectPressed,
            this.GenerateLargeFile,
            this.processManagerToolStripMenuItem,
            this.LockCursorClick});
            this.RightClickMenu.Name = "RightClickMenu";
            this.RightClickMenu.Size = new System.Drawing.Size(181, 164);
            // 
            // remoteDesktopClick
            // 
            this.remoteDesktopClick.Image = ((System.Drawing.Image)(resources.GetObject("remoteDesktopClick.Image")));
            this.remoteDesktopClick.Name = "remoteDesktopClick";
            this.remoteDesktopClick.Size = new System.Drawing.Size(180, 22);
            this.remoteDesktopClick.Text = "Remote Desktop";
            this.remoteDesktopClick.Click += new System.EventHandler(this.RDPClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // BeepClicked
            // 
            this.BeepClicked.Image = ((System.Drawing.Image)(resources.GetObject("BeepClicked.Image")));
            this.BeepClicked.Name = "BeepClicked";
            this.BeepClicked.Size = new System.Drawing.Size(180, 22);
            this.BeepClicked.Text = "Beep";
            this.BeepClicked.Click += new System.EventHandler(this.OnBeepClick);
            // 
            // DisconnectPressed
            // 
            this.DisconnectPressed.Image = ((System.Drawing.Image)(resources.GetObject("DisconnectPressed.Image")));
            this.DisconnectPressed.Name = "DisconnectPressed";
            this.DisconnectPressed.Size = new System.Drawing.Size(180, 22);
            this.DisconnectPressed.Text = "Disconnect";
            this.DisconnectPressed.Click += new System.EventHandler(this.DisconnectPressed_Click);
            // 
            // GenerateLargeFile
            // 
            this.GenerateLargeFile.Image = ((System.Drawing.Image)(resources.GetObject("GenerateLargeFile.Image")));
            this.GenerateLargeFile.Name = "GenerateLargeFile";
            this.GenerateLargeFile.Size = new System.Drawing.Size(180, 22);
            this.GenerateLargeFile.Text = "Generate Large File";
            this.GenerateLargeFile.Click += new System.EventHandler(this.ClickedGenerateFile);
            // 
            // processManagerToolStripMenuItem
            // 
            this.processManagerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("processManagerToolStripMenuItem.Image")));
            this.processManagerToolStripMenuItem.Name = "processManagerToolStripMenuItem";
            this.processManagerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.processManagerToolStripMenuItem.Text = "Process Manager";
            this.processManagerToolStripMenuItem.Click += new System.EventHandler(this.processManagerToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectionNumber});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ConnectionNumber
            // 
            this.ConnectionNumber.Image = ((System.Drawing.Image)(resources.GetObject("ConnectionNumber.Image")));
            this.ConnectionNumber.Name = "ConnectionNumber";
            this.ConnectionNumber.Size = new System.Drawing.Size(161, 17);
            this.ConnectionNumber.Text = "Number of connections: 0";
            this.ConnectionNumber.Click += new System.EventHandler(this.ConnectionNumber_Click);
            // 
            // LockCursorClick
            // 
            this.LockCursorClick.Image = ((System.Drawing.Image)(resources.GetObject("LockCursorClick.Image")));
            this.LockCursorClick.Name = "LockCursorClick";
            this.LockCursorClick.Size = new System.Drawing.Size(180, 22);
            this.LockCursorClick.Text = "Lock Cursor";
            this.LockCursorClick.Click += new System.EventHandler(this.LockCursorClicked);
            // 
            // CurrentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ListViewVictims);
            this.Name = "CurrentForm";
            this.Text = "Zealand Rat";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.RightClickMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView ListViewVictims;
        private ColumnHeader IpAddress;
        private ColumnHeader ComputerName;
        private ColumnHeader ConnectionTime;
        private ContextMenuStrip RightClickMenu;
        private ToolStripMenuItem DisconnectPressed;
        private ToolStripMenuItem BeepClicked;
        private ToolStripMenuItem GenerateLargeFile;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel ConnectionNumber;
        private ToolStripMenuItem remoteDesktopClick;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem processManagerToolStripMenuItem;
        private ToolStripMenuItem LockCursorClick;
    }
}