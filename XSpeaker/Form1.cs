using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

using NAudio.Wave;

namespace XSpeaker
{
    public partial class Form1 : Form
    {

        //INTERFACE STUFF=================================================
        const string FileName = "SavedBuddyList.bin";
        public static List<User> UserList; //юзер-лист
        public String IP;
        public static bool isAdmin = false;
        volatile public static List<ListViewItem> SavedColors = new List<ListViewItem>();
        //=================================================================
        
        //SOUND_STUFF==========================================================================
        WaveIn _waveIn;
        WaveOut _waveOut;
        BufferedWaveProvider _playBuffer;
        //int Volume = 0;
        //=====================================================================================

        //CONNECTION_STUFF=====================================================================
        UdpClient udpClient;                //Listens and sends data on port, used in synchronous mode.
        Socket clientSocket;
        volatile bool bStop;                         //Flag to end the Start and Receive threads.
        IPEndPoint otherPartyIP;            //IP of party we want to make a call.
        EndPoint otherPartyEP;
        // volatile bool bIsCallActive;                 //Tells whether we have an active call.
        byte[] byteData = new byte[1024];   //Buffer to store the data received.
        volatile int nUdpClientFlag;                 //Flag used to close the udpClient socket.
        //bool isSocketOpen = true;
        volatile bool isFormClosing = false;
        TcpListener Listener;
        //======================================================================================

        public Form1()
        {
            InitializeComponent();
        }
        
        public delegate void ChangeImage(Button btn, Image img);
        /// <summary>
        /// Предоставляет доступ к кнопке для изменения изображения.
        /// </summary>
        /// <param name="btn">Кнопка.</param>
        /// <param name="img">Изображение.</param>
        public static ChangeImage ChangeImg = (Button btn , Image img) =>
            {
                btn.BackgroundImage = img;
            };

        public delegate void CallingUser(ListView LV, IPAddress IP, Color clr);
        /// <summary>
        /// Изменяет изображение элемента.
        /// </summary>
        public static CallingUser CallUser = (ListView LV, IPAddress IP, Color clr ) =>
            {
                User u = UserList.Find(x => IPAddress.Equals( x.IP, IP ));
                if (LV.Items[u.Name] != null)
                    LV.Items[u.Name].BackColor = clr;
                else if (UserList.Contains(u))
                {
                    ListViewItem item = new ListViewItem(u.Name);
                    item.Name = u.Name;
                    item.SubItems.Add(u.IP.ToString());
                    item.BackColor = clr;
                    LV.Items.Add(item);
                }
            };

        public delegate void AddToList(ListView LV, ListViewItem item);
        /// <summary>
        /// Добавляет элемент в ListView.
        /// </summary>
        public AddToList Adding = (ListView LV, ListViewItem item) =>
             {
                 if (!LV.Items.ContainsKey(item.Name))
                    LV.Items.Add(item);
             };

        public delegate void ClearList(ListView LV);
        /// <summary>
        /// Очищает все элементы из ListView.
        /// </summary>
        public ClearList Clearing = (ListView LV) =>
            {
                foreach (ListViewItem item in LV.Items)
                {
                    if (item.BackColor != Color.Orange) LV.Items.Remove(item);
                }
            };
        
        delegate void SetVisible(ProgressBar pBar, bool flag);
        /// <summary>
        /// Устанавливает режим видимости для ProgressBar.
        /// </summary>
        /// <param name="pBar">Элемент управления.</param>
        /// <param name="flag">Флаг видимости.</param>
        SetVisible SettingVisible = (ProgressBar pBar, bool flag) =>
        {
            pBar.Visible = flag;
        };
        
        delegate void PerformStep(ProgressBar pBar);
        /// <summary>
        /// Увеличивает текущую позицию индикатора хода выполнения.
        /// </summary>
        /// <param name="pBar">Элемент управления.</param>
        PerformStep Steping = (ProgressBar pBar) =>
        {
            pBar.PerformStep();
        };
        
        delegate void SetMaxValue(ProgressBar pBar, int Value);
        /// <summary>
        /// Задает наибольшее значение диапазона ProgressBar.
        /// </summary>
        /// <param name="pBar">Элемент управления.</param>
        /// <param name="Value">Максимальное значение.</param>
        SetMaxValue MaxValue = (ProgressBar pBar, int Value) =>
        {
            pBar.Maximum = Value;
        };

        delegate void SetCurrentValue(ProgressBar pBar, int Value);
        /// <summary>
        /// Устанавливает текущее значение ProgressBar.
        /// </summary>
        SetCurrentValue CurrentValue = (ProgressBar pBar, int Value) =>
            {
                pBar.Value = Value;
            };
        delegate void SetEnableControl(ToolStripMenuItem item, bool flag);
        /// <summary>
        /// Устанавливает режим, определяющий, включен ли элемент управления ToolStripMenuItem.
        /// </summary>
        SetEnableControl SetEnable = (ToolStripMenuItem item, bool flag) =>
            {
                item.Enabled = flag;
            };
        delegate void ShowPopup(NotifyIcon icon, IPAddress IP, String Title, String Msg);
        /// <summary>
        /// Показывает всплывающую подсказку с информацией о входящем звонке.
        /// </summary>
        ShowPopup ShowTip = (NotifyIcon icon, IPAddress IP, String Title, String Msg) =>
            {
                string name = UserList.Find(x => IPAddress.Equals(x.IP, IP)).Name;
                icon.Visible = true;
                icon.ShowBalloonTip(7000, Title, Msg + name, ToolTipIcon.Info);
            };

        delegate void SaveColors(ListView.ListViewItemCollection itemCollection);

        SaveColors SavingColors = (ListView.ListViewItemCollection itemCollection) =>
            {
                SavedColors.Clear();
                foreach (ListViewItem item in itemCollection)
                {
                    if (item.BackColor != Color.White) SavedColors.Add(item);
                }
            };

        delegate void SetColors(ListView LV);

        SetColors SettingColors = (ListView LV) =>
            {
                foreach (ListViewItem item in LV.Items)
                {
                    if (SavedColors.Contains(item)) item.BackColor = Color.Orange;
                }
            };

        /// <summary>
        /// Принудительное закрытие приложения.
        /// </summary>
        void FatalClosing()
        {

            try 
            {
                EndPoint ep2 = new IPEndPoint(IPAddress.Parse(IP), 1450);
                SendMessage(XProtocol.Command.Disconnect, ep2);

                byte[] emptybuffer = new byte[0];
                udpClient.Send(emptybuffer, 0, otherPartyIP.Address.ToString(), 1550);

                Listener.Stop();
                Listener = null;
            } 
            catch (Exception ex) {};

            WriteToFile();

            isFormClosing = true;
            bStop = true;
           // isSocketOpen = false;

            StopIncoming();
            StopOutcoming();

            StateTimer.Stop();
            timer.Stop();
            ConnectingTimer.Stop();

            Thread.Sleep(2000);
            
            this.Close();
            Application.Exit();
            
        }
        /// <summary>
        /// Проверка пользователей "на Онлайн"
        /// </summary>
        /// <param name="UserList">Список пользователей</param>
        private void CheckOnline(List<User> UserList, Boolean isPause)
        {
            if (isPause)
                Thread.Sleep(2000);

            BeginInvoke(SetEnable, new object[] { toolStripMenuItem1, false }); ;//отключает возможность добавления/удаления

            int count = UserList.Count;

            //IAsyncResult iar = BuddyList.BeginInvoke(SavingColors, BuddyList.Items);
            //iar.AsyncWaitHandle.WaitOne();
            BuddyList.BeginInvoke(Clearing, BuddyList);
            IAsyncResult result;
            bool success;
            progressBar1.BeginInvoke(MaxValue, new object[] { progressBar1, count });//устанавливаем максимальное значение.
            progressBar1.BeginInvoke(SettingVisible, new object[] { progressBar1, true });//отображаем ProgressBar.
            IPEndPoint EndPoint;
            Socket mySocket;
            ListViewItem lv;
            List<ListViewItem> ColoredItems;

            foreach (User item in (List<User>)UserList)
            {
                try
                {
                        // получаем адрес и порт из элемента адресной книги
                        EndPoint = new IPEndPoint(item.IP,item.Port);
                        mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                        //result = mySocket.BeginConnect(EndPoint, ConnectCallback, mySocket);//попытка подключения
                        //success = result.AsyncWaitHandle.WaitOne(1000, false);//устанавливается тайм-аут
                        
                        mySocket.Connect(EndPoint);
                        lv = new ListViewItem(item.Name);
                        lv.Name = item.Name;
                        lv.SubItems.Add(item.IP.ToString());
                        BuddyList.BeginInvoke(Adding, new object[] { BuddyList, lv });
                        

                        //if (success)
                        //{
                        //    lv = new ListViewItem(item.Name);
                        //    lv.Name = item.Name;
                        //    lv.SubItems.Add(item.IP.ToString());
                        //    BuddyList.BeginInvoke(Adding, new object[] { BuddyList, lv });
                        //}

                        mySocket.Close();
                        //progressBar1.BeginInvoke(Steping, new object[] { progressBar1 });
                    }
                    catch (Exception ex)
                    {
                        //progressBar1.BeginInvoke(Steping, new object[] { progressBar1 });
                        //MessageBox.Show(ex.Message);
                    };
                }
            progressBar1.BeginInvoke(SettingVisible, new object[] { progressBar1, false });//скрываем ProgressBar.
            progressBar1.BeginInvoke(CurrentValue, new object[] { progressBar1, 0 });
            //BuddyList.BeginInvoke(SettingColors, BuddyList);
            if (isAdmin)
                BeginInvoke(SetEnable, new object[] { toolStripMenuItem1, true });
        }

        //см. isOnline
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
            }
            catch (Exception error)
            {
            }
        }

        void StartThreadChecking(Boolean ispause)
        {
            Thread CheckingThread = new Thread(delegate() { CheckOnline(UserList, ispause); });
            CheckingThread.IsBackground = true;
            CheckingThread.Start();
            
        } 

        /// <summary>
        /// Запись данных в файл
        /// </summary>
        private void WriteToFile()
        {
            try
            {
                Stream FileStream = File.Create(FileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(FileStream, UserList);
                FileStream.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Чтение данных из файла
        /// </summary>     
        private void ReadFromFile()
        {
            if (File.Exists(FileName))
            {
                Stream FileStream = File.OpenRead(FileName);
                BinaryFormatter deserializer = new BinaryFormatter();
                UserList = (List<User>)deserializer.Deserialize(FileStream);
                FileStream.Close();
            }
            else MessageBox.Show("Файла данных не существует!");

        }
        /// <summary>
        /// Добавление клиента.
        /// </summary>
        private void AddIP()
        {
            ListViewItem lv;

            foreach (User item in UserList.ToArray())
            {
                if (BuddyList.Items.ContainsKey(item.Name)) continue;//если такой элемент уже есть, переходим к след итерации
                lv = new ListViewItem(item.Name);//создаем элемент с подэлементами
                lv.Name = item.Name;
                lv.SubItems.Add(item.IP.ToString());
                BuddyList.Items.Add(lv);//добавляем элемент в адресную книгу
            }
        }
        /// <summary>
        /// Удаление клиента.
        /// </summary>
        private void DelIP()
        {
            if (BuddyList.SelectedItems.Count == 0) // проверка: выделен ли элемент
            {
                MessageBox.Show("Не выделен элемент(ы), который необходимо удалить!");
                return;
            }
            var result = MessageBox.Show("Вы уверены, что хотите удалить этот адрес?", "Удаление адреса",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                while (BuddyList.SelectedItems.Count > 0) // удаление выделенных элементов из адресной книги и юзер-листа
                {
                    UserList.Remove(UserList.Find(x => x.Name == BuddyList.SelectedItems[0].Text));
                    BuddyList.Items.Remove(BuddyList.SelectedItems[0]);
                }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (IP == "" || IP == null)
            {
                MessageBox.Show("Выделите контакт!");
                return;
            }

            switch (State)
            {
                case Statement.Nothing:
                    EndPoint ep = new IPEndPoint(IPAddress.Parse(IP), 1450);
                    SendMessage( XProtocol.Command.Invite, ep);

                    State = Statement.Connecting;
                    ConnectingTimer.Start();
                    break;

                case Statement.Connecting:
                case Statement.ConnectingAndListening:

                    var result = MessageBox.Show("Продолжить ожидание?", "Соединение в процессе...", 
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    StartThreadChecking(true);

                    if (result == DialogResult.No && State == Statement.Connecting)
                    {
                        InitSocket();
                        State = Statement.Nothing;
                        ConnectingTimer.Stop();
                    }
                    if (result == DialogResult.No && State == Statement.ConnectingAndListening) 
                    {
                        InitSocket();
                        State = Statement.Listening;
                        ConnectingTimer.Stop();
                    }
                    break;

                case Statement.Speaking:
                    StopOutcoming();
                    EndPoint ep1 = new IPEndPoint(IPAddress.Parse(IP), 1450);
                    SendMessage( XProtocol.Command.Bye, ep1);
                    //bIsCallActive = false;
                    nUdpClientFlag += 1;

                    button1.BeginInvoke(ChangeImg, new object[] { button1, Properties.Resources.microphoneblue_5728 });
                    InitAdressBookListener();
                    break;

                case Statement.SpeakingAndListening:
                    EndPoint ep2 = new IPEndPoint(IPAddress.Parse(IP), 1450);
                    SendMessage(XProtocol.Command.Bye, ep2);

                    StopOutcoming();
                    nUdpClientFlag += 1; 

                    button1.BeginInvoke(ChangeImg, new object[] { button1, Properties.Resources.microphoneblue_5728 });
                    break;

                case Statement.Listening:
                    EndPoint ep3 = new IPEndPoint(IPAddress.Parse(IP), 1450);
                    SendMessage(XProtocol.Command.Invite, ep3);

                    State = Statement.ConnectingAndListening;
                    ConnectingTimer.Start();
                    break;
            }

            timer.Tick += timer_Tick;
            timer.Start();
            button1.Enabled = false;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            button1.Enabled = true;
            timer.Stop();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddOrChangeUser form = new AddOrChangeUser();
            form.Text = "Добавление нового клиента...";
            form.ShowDialog(this); //вызов формы

            //if (form.DialogResult == DialogResult.OK)
            //    AddIP();
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelIP();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!isFormClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);

        static void MinimizeFootprint()
        {
            EmptyWorkingSet(Process.GetCurrentProcess().Handle);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MinimizeFootprint();

            this.Text = Environment.MachineName;
            UserList = new List<User>();
            ReadFromFile();//загрузка списка.      

            StartThreadChecking(false);

            InitSocket();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                notifyIcon1.Text = Environment.MachineName;
                notifyIcon1.Visible = true;
            } 
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            } 
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FatalClosing();
        }

        private void обновитьСписокПользователейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BuddyList.BeginUpdate();
            StartThreadChecking(false);
            BuddyList.EndUpdate();
            //progressBar1.BeginInvoke(CurrentValue, new object[] { progressBar1, 0 });
        }


        enum Statement
        {
            Nothing,
            Connecting,
            Speaking,
            Listening,

            ConnectingAndListening,
            SpeakingAndListening,

            NotSpeakingNotListening
        }

        Statement State = Statement.Nothing;

        private void InitCallIn()
        {
            try
            {
                if (udpClient == null)
                    udpClient = new UdpClient(1550);

                Thread senderThread = new Thread(new ThreadStart(Send));

                //Start the sender thread.
                senderThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "InitCallIn ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitCallOut()
        {
            try
            {
                //Start listening on port
                if (udpClient == null)
                    udpClient = new UdpClient(1550);

                Thread receiverThread = new Thread(new ThreadStart(Receive));

                //Start the receiver and sender thread.
                receiverThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "InitCallOut ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Остановка записи
        /// </summary>
        private void StopOutcoming()
        {
            if (_waveIn != null)
            {
                _waveIn.StopRecording();
                _waveIn.Dispose();
                _waveIn = null;
            }
        }

        private void StartCall()
        {
            //Get the IP we want to call.
            otherPartyIP = new IPEndPoint(IPAddress.Parse(IP), 1450);
            otherPartyEP = (EndPoint)otherPartyIP;

            //_bytesSent = 0;
            //_segmentFrames = 960;
            //_encoder = OpusEncoder.Create(48000, 1, FragLabs.Audio.Codecs.Opus.Application.Voip);
            //_encoder.Bitrate = 8192;
            //_bytesPerSegment = _encoder.FrameByteCount(_segmentFrames);

            _waveIn = new WaveIn(WaveCallbackInfo.FunctionCallback());

            //TODO Эта строка может влиять на переполнение буфера----------------------------------
            _waveIn.BufferMilliseconds = 100;
            //-------------------------------------------------------------------------------------
            _waveIn.DeviceNumber = 0;
            _waveIn.DataAvailable += _waveIn_DataAvailable;
            _waveIn.WaveFormat = new WaveFormat(48000, 16, 1);

            //_playBuffer = new BufferedWaveProvider(new WaveFormat(48000, 16, 1));
            _waveIn.StartRecording();
        }

        void _waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            // _playBuffer.AddSamples(e.Buffer, 0, e.BytesRecorded);
            try
            {
                udpClient.Send(e.Buffer, e.BytesRecorded, otherPartyIP.Address.ToString(), 1550);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Ошибка UDP отправки. State = " + State.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Send()
        {
            try
            {
                StartCall();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Ошибка передачи", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                //Increment flag by one.
                nUdpClientFlag += 1;

                //When flag is two then it means we have got out of loops in Send and Receive.
                while (nUdpClientFlag != 2 && !isFormClosing)
                { 
                    Thread.Sleep(1); 
                }

                //Clear the flag.
                nUdpClientFlag = 0;

                //Close the socket.
                if (udpClient != null && State != Statement.SpeakingAndListening)
                {
                    //MessageBox.Show("Send Close Client");
                    udpClient.Close();
                    udpClient = null;
                }

                if (State == Statement.SpeakingAndListening)
                    State = Statement.Listening;
                else
                {
                    State = Statement.Nothing;
                    IP = "";
                }
            }
        }

        /// <summary>
        /// Остановка чтения
        /// </summary>
        private void StopIncoming()
        {
            if (_waveOut != null)
            {
                _waveOut.Stop();
                _waveOut.Dispose();
                _waveOut = null;
            }
        }

        private void StartIncoming()
        {

            _playBuffer = new BufferedWaveProvider(new WaveFormat(48000, 16, 1));
          
            if (_waveOut == null)
                _waveOut = new WaveOut(WaveCallbackInfo.FunctionCallback());
            _waveOut.Volume = 10;
            _waveOut.DeviceNumber = 0;
            _waveOut.Init(_playBuffer);

            _waveOut.Play();
        }

        private void Receive()
        {
            try
            {
                bStop = false;
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                StartIncoming();

                while (!bStop && !isFormClosing)
                {
                    //Receive data.
                    try
                    {
                        byte[] byteData = udpClient.Receive(ref remoteEP);
                
                        if ((byteData == null || byteData.Length == 0) &&
                            State != Statement.NotSpeakingNotListening                                               
                            && State != Statement.Speaking
                            && State != Statement.SpeakingAndListening
                          )
                        {
                            //MessageBox.Show("BREAK. State = " + State.ToString());
                            break;
                        }

                        if (State == Statement.NotSpeakingNotListening)
                            State = Statement.Speaking;

                        _playBuffer.AddSamples(byteData, 0, byteData.Length);
                    }
                    catch (Exception e)
                    {
                        //ignore
                        //MessageBox.Show(e.Message, "Ошибка считывания буфера. State = " + State.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                //if (bStop)
                //    MessageBox.Show("BSTOP");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка приема", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close the socket.
                if (udpClient != null && State != Statement.SpeakingAndListening
                   && State != Statement.Speaking && !isFormClosing)
                {
                   // MessageBox.Show("Receive Close Client");
                    udpClient.Close();
                    udpClient = null;
                }

            }
        }

        private void InitSocket()
        {
            try
            {
                if (clientSocket != null)
                {
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    clientSocket = null;
                }

                // bIsCallActive = false;
                nUdpClientFlag = 0;

                //Using UDP sockets
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                EndPoint ourEP = new IPEndPoint(IPAddress.Any, 1450);
                //Listen asynchronously on port 1450 for coming messages (Invite, Bye, etc).
                clientSocket.Bind(ourEP);

                //Receive data from any IP.
                EndPoint remoteEP = (EndPoint)(new IPEndPoint(IPAddress.Any, 0));

                byteData = new byte[1024];
                //Receive data asynchornously.
                clientSocket.BeginReceiveFrom(byteData,
                                           0, byteData.Length,
                                           SocketFlags.None,
                                           ref remoteEP,
                                           new AsyncCallback(OnReceive),
                                           null);

                InitAdressBookListener();
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message, "Ошибка инициализации клиентского UDP сокета", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitAdressBookListener()
        {
            try
            {
                if (Listener != null)
                {
                    Listener.Stop();
                    Listener = null;
                }

                Listener = new TcpListener(7000);
                Listener.Start();
                Listener.BeginAcceptSocket(null, null);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка создания TCPListener для адресной книги", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void SendMessage(XProtocol.Command cmd, EndPoint sendToEP)
        {
            try
            {
                //Create the message to send.
                XProtocol.Data msgToSend = new XProtocol.Data();

                // msgToSend.strName = UserName;   //Name of the user.
                msgToSend.cmdCommand = cmd;         //Message to send.

                byte[] message = msgToSend.ToByte();

                //Send the message asynchronously.
                clientSocket.BeginSendTo(message, 0, message.Length, SocketFlags.None, sendToEP,
                    new AsyncCallback(OnSend), null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка отправки сообщения клиенту", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //SERVER SIDE
        private void OnReceive(IAsyncResult ar)
        {
           // if (isSocketOpen)
                try
                {
                    EndPoint receivedFromEP = new IPEndPoint(IPAddress.Any, 0);

                    //Get the IP from where we got a message.
                    clientSocket.EndReceiveFrom(ar, ref receivedFromEP);

                    //Convert the bytes received into an object of type Data.
                    XProtocol.Data msgReceived = new XProtocol.Data(byteData);

                    //Act according to the received message.
                    switch (msgReceived.cmdCommand)
                    {
                        //We have an incoming call.
                        case XProtocol.Command.Invite:
                            {
                                try
                                {
                                    IPEndPoint tempPoint = (IPEndPoint)receivedFromEP;

                                    //MessageBox.Show(tempPoint.Address.ToString() + ";IP = " + IP);

                                    if (IP == null) IP = "";

                                    if (String.Equals(IP, "") || String.Equals(tempPoint.Address.ToString(), IP))
                                    {
                                        otherPartyEP = receivedFromEP;
                                        otherPartyIP = (IPEndPoint)receivedFromEP;

                                        if (IP == "")
                                            IP = otherPartyIP.Address.ToString();

                                        //We have no active call.
                                        SendMessage(XProtocol.Command.OK, receivedFromEP);


                                        if (State == Statement.Nothing)
                                            State = Statement.Listening;
                                        else
                                            State = Statement.SpeakingAndListening;
                                        // button1.Enabled = false;
                                        InitCallOut();

                                        //button1.BeginInvoke(ChangeImg, new object[] { button1, Properties.Resources.microphonered_2904 });
                                        BeginInvoke(ShowTip, new object[] { notifyIcon1, 
                                    UserList.Find(x => IPAddress.Equals(x.IP, otherPartyIP.Address)).IP ,
                                "Входящий звонок", "Вам звонит "});
                                        BeginInvoke(CallUser, new object[] { BuddyList, otherPartyIP.Address, Color.Orange });
                                    }
                                    else
                                    {
                                        //We already have an existing call. Send a busy response.
                                        SendMessage(XProtocol.Command.Busy, receivedFromEP);
                                        BeginInvoke(ShowTip, new object[] { notifyIcon1, 
                                    UserList.Find(x => IPAddress.Equals(x.IP, tempPoint.Address)).IP ,
                                "Пропущенный звонок", "Вам звонил "});
                                    }
                                    
                                } catch (Exception exc)
                                {
                                    MessageBox.Show(exc.Message, "Invite Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                break;
                            }

                        //Remote party is busy.
                        case XProtocol.Command.Busy:
                            {
                                ConnectingTimer.Stop();

                                State = Statement.Nothing;
                                IP = "";
                                MessageBox.Show("Пользователь занят.", "VoiceChat", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                break;
                            }

                        //OK is received in response to an Invite.
                        case XProtocol.Command.OK:
                            {
                                //IPEndPoint ipend = receivedFromEP as IPEndPoint;
                                //
                                //Start a call.
                                InitCallIn();
                                button1.BeginInvoke(ChangeImg, new object[] { button1, Properties.Resources.microphonered_2904 });

                                if (State == Statement.ConnectingAndListening)
                                    State = Statement.SpeakingAndListening;
                                else
                                    State = Statement.Speaking;
                                break;
                            }

                        case XProtocol.Command.Bye:
                            {
                                IPEndPoint ipend = receivedFromEP as IPEndPoint;
                                BeginInvoke(CallUser, new object[] { BuddyList, ipend.Address, Color.White });
                                bStop = true;
                                //Check if the Bye command has indeed come from the user/IP with which we have
                                //a call established. This is used to prevent other users from sending a Bye, which
                                //would otherwise end the call.
                                //if (receivedFromEP.Equals(otherPartyEP) == true)
                                //{

                                    //End the call.
                                    //StopIncoming();
                                    //if (State != Statement.SpeakingAndListening)
                                        
                                    //nUdpClientFlag = 0;
                                    //bIsCallActive = false;
                                    if (State == Statement.SpeakingAndListening)
                                    {
                                        StopIncoming();
                                        State = Statement.Speaking;
                                    }
                                    else
                                    {
                                        State = Statement.Nothing;
                                        IP = "";
                                        StopOutcoming();
                                        StopIncoming();

                                        InitAdressBookListener();
                                    }
                                    IPEndPoint ipend1 = receivedFromEP as IPEndPoint;
                                    BeginInvoke(ShowTip, new object[] { notifyIcon1, 
                                    UserList.Find(x => IPAddress.Equals(x.IP, ipend1.Address)).IP ,
                                    "Собеседник отсоединился", ""});

                                    //button1.BeginInvoke(ChangeImg, new object[] { button1, Properties.Resources.microphoneblue_5728});
                                    // button1.Enabled = true;                               
                                //}
                                break;

                            }
                        case XProtocol.Command.Disconnect: 
                            State = Statement.Nothing;
                                        IP = "";
                                        StopOutcoming();
                                        StopIncoming();

                                        InitAdressBookListener();
                                    
                                    IPEndPoint ipend2 = receivedFromEP as IPEndPoint;
                                    BeginInvoke(ShowTip, new object[] { notifyIcon1, 
                                    UserList.Find(x => IPAddress.Equals(x.IP, ipend2.Address)).IP ,
                                    "Собеседник закрыл программу", ""});
                                    button1.BeginInvoke(ChangeImg, new object[] { button1, Properties.Resources.microphoneblue_5728 });
                                    IPEndPoint ipend0 = receivedFromEP as IPEndPoint;
                                BeginInvoke(CallUser, new object[] { BuddyList, ipend0.Address, Color.White });
                                nUdpClientFlag += 1;
                                StartThreadChecking(true);
                                    break;
                    }

                    byteData = new byte[1024];
                    //Get ready to receive more commands.
                    clientSocket.BeginReceiveFrom(byteData, 0, byteData.Length, SocketFlags.None,
                        ref receivedFromEP, new AsyncCallback(OnReceive), null);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Ошибка принятия звонка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        public void OnSend(IAsyncResult ar)
        {
            try
            {
                clientSocket.EndSendTo(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "VoiceChat-OnSend ()", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuddyList_MouseClick(object sender, MouseEventArgs e)
        {

            if (State == Statement.Nothing)
            {
                if (BuddyList.SelectedItems.Count == 1)
                    IP = BuddyList.SelectedItems[0].SubItems[1].Text;
                if (BuddyList.SelectedItems.Count == 0)
                    IP = "";
            }
            //else
            //    MessageBox.Show("Вы не можете звонить абоненту пока связаны с другим!", "Информация" , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(State.ToString());
        }

        private void служебнаяИнформацияКлиентаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BuddyList.SelectedItems.Count != 0)
            {
                ServiceInformation SI = new ServiceInformation();
                SI.Show(this);
                SI.label3.Text = BuddyList.SelectedItems[0].Text;
                SI.label4.Text = BuddyList.SelectedItems[0].SubItems[1].Text;
            }
            else MessageBox.Show("Выберите пользователя.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void админмодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasswordForm p = new PasswordForm();
            p.ShowDialog(this);
        }

        private void выйтиИзРежимаАдминистратораToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Выход из режима администратора",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                isAdmin = false;
                this.toolStripMenuItem1.Enabled = false;

                this.выйтиИзРежимаАдминистратораToolStripMenuItem.Visible = false;
                this.режимадминистратораToolStripMenuItem.Visible = true;
                this.AdminModLabel.Visible = false;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }

        private void BuddyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FatalClosing();
        }

        private void показатьОкноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            } 
        }

        private void BuddyList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item.BackColor != Color.Orange && State == Statement.Listening) e.Item.Selected = false;
        }

        private void StateTimer_Tick(object sender, EventArgs e)
        {
            switch (State)
            {
                             case Statement.Nothing: toolStripStatusLabel2.Text = "...";
                                    break;
                          case Statement.Connecting: toolStripStatusLabel2.Text = "соединение в процессе...";
                                    break;
                            case Statement.Speaking: toolStripStatusLabel2.Text = "передача...";
                                    break;
                           case Statement.Listening: toolStripStatusLabel2.Text = "прием...";
                                    break;
                case Statement.SpeakingAndListening: toolStripStatusLabel2.Text = "прием и передача...";
                                    break;
                                            default: toolStripStatusLabel2.Text = "...";
                                    break;
            }
        }

        private void ConnectingErrorMessage(Statement state)
        {
            if (state == Statement.Connecting || state == Statement.ConnectingAndListening)
            {
                InitSocket();
                StartThreadChecking(false);
                MessageBox.Show("Не удалось установить соединение. Проверьте подключение и удостоверьтесь, что пользователь Онлайн.",
                            "Ошибка соединения...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (state == Statement.Connecting)
                    State = Statement.Nothing;
                if (state == Statement.ConnectingAndListening)
                    State = Statement.Listening;
            }
        }

        private void ConnectingTimer_Tick(object sender, EventArgs e)
        {
            ConnectingTimer.Stop();

            ConnectingErrorMessage(State);
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Show();
        }

    }
}
