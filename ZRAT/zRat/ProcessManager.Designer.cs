namespace zRat
{
    partial class ProcessManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessManager));
            this.ProcessList = new System.Windows.Forms.ListView();
            this.ProcessName = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.ProcessContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.KillContext = new System.Windows.Forms.ToolStripMenuItem();
            this.ProcessContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // ProcessList
            // 
            this.ProcessList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProcessName,
            this.columnHeader1});
            this.ProcessList.ContextMenuStrip = this.ProcessContext;
            this.ProcessList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProcessList.GridLines = true;
            this.ProcessList.Location = new System.Drawing.Point(0, 0);
            this.ProcessList.Name = "ProcessList";
            this.ProcessList.Size = new System.Drawing.Size(800, 450);
            this.ProcessList.TabIndex = 0;
            this.ProcessList.UseCompatibleStateImageBehavior = false;
            this.ProcessList.View = System.Windows.Forms.View.Details;
            this.ProcessList.SelectedIndexChanged += new System.EventHandler(this.ProcessList_SelectedIndexChanged);
            // 
            // ProcessName
            // 
            this.ProcessName.Text = "Process Name";
            this.ProcessName.Width = 200;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Location";
            this.columnHeader1.Width = 550;
            // 
            // ProcessContext
            // 
            this.ProcessContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KillContext});
            this.ProcessContext.Name = "ProcessContext";
            this.ProcessContext.Size = new System.Drawing.Size(181, 48);
            // 
            // KillContext
            // 
            this.KillContext.Image = ((System.Drawing.Image)(resources.GetObject("KillContext.Image")));
            this.KillContext.Name = "KillContext";
            this.KillContext.Size = new System.Drawing.Size(180, 22);
            this.KillContext.Text = "Kill";
            this.KillContext.Click += new System.EventHandler(this.KillSelected);
            // 
            // ProcessManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ProcessList);
            this.Name = "ProcessManager";
            this.Text = "ProcessManager";
            this.Load += new System.EventHandler(this.ProcessManager_Load);
            this.ProcessContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ListView ProcessList;
        private ColumnHeader ProcessName;
        private ColumnHeader columnHeader1;
        private ContextMenuStrip ProcessContext;
        private ToolStripMenuItem KillContext;
    }
}