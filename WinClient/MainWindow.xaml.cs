using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace WinClient
{
    public interface IMainForm
    {
        event EventHandler Connect;
        event EventHandler GetInfo;
        event EventHandler StartService;
        event EventHandler StopService;

        string ContentText {  set; }
        IPAddress IP_Address { get; }
        int Port { get; }
        string[] array { set; }
        int indexServ { get; }
        void ShowForm();
        bool ConnectVisible { set; }
        bool StartVisible { set; }
        bool StopVisible { set; }
        bool ListServiceVisible { set; }

    }

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IMainForm 
    {
        public event EventHandler Connect;
        public event EventHandler GetInfo;
        public event EventHandler StartService;
        public event EventHandler StopService;
        public MainWindow()
        {
            InitializeComponent();
            startService.IsEnabled = false;
            stopService.IsEnabled = false;
            but.IsEnabled = false;

        }

        //string str;
        //Socket socket;
        string[] array1;
        bool listClc;
        //int oldIndex = -1;
        //string serv;

        public void ShowForm()
        {
            this.Show();
        }

        public string ContentText // Это пишется в классе вьюхи
        {
            //get { return textWindow.Text; }
            set { textWindow.Text += value; textWindow.ScrollToEnd(); }
        }

        public IPAddress IP_Address
        { 
          get { return IPAddress.Parse(serverIP.Text); }
        }

        public int Port
        {
            get { return int.Parse(portServer.Text); }
        }

        public string[] array
        {           
            set
            {
                array1 = value;
                listService.Items.Clear();
                for (int i = 0; i < array1.Length; i++)
                    listService.Items.Add (array1[i]);
            }
        }

        public int indexServ
        {
            get { return listService.SelectedIndex; }
        }

        public bool ConnectVisible 
        {
            set
            { 
                if (value) connect.IsEnabled = true;
                else connect.IsEnabled = false;
            } 
        }

        public bool StartVisible
        {
            set
            {
                if (value) startService.IsEnabled = true;
                else startService.IsEnabled = false;
            }
        }

        public bool StopVisible
        {
            set
            {
                if (value) stopService.IsEnabled = true;
                else stopService.IsEnabled = false;
            }
        }

        public bool ListServiceVisible { set { if (value) listService.IsEnabled = true; else listService.IsEnabled = false; } }




        private void connect_Click(object sender, RoutedEventArgs e)
        {
            listClc = false;
            listService.IsEnabled = true;
            if (Connect != null)
                Connect(null, EventArgs.Empty);
            connect.IsEnabled = false;
            listClc = true;
        }

        
        private void listService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listClc)
            if (GetInfo != null)
                GetInfo(null, EventArgs.Empty);
        }

        private void Button_Click(object sender, RoutedEventArgs e)  //получить инфу
        {
            if (GetInfo != null)
                GetInfo(null, EventArgs.Empty);
            /*string serv = array1[listService.SelectedIndex].ToString();
            SendMess(socket, serv);
            str = GetMess(socket);
            if (str == "esc")
            {
                textWindow.Text += Environment.NewLine + "сервер разорвал соединение";
                textWindow.ScrollToEnd();
                connect.IsEnabled = true;
                but.IsEnabled = false;
            }
            else
            {
                textWindow.Text += Environment.NewLine + str;
                textWindow.ScrollToEnd();
                string s = str.Substring(str.Length - 7, 7);
                startService.IsEnabled = false;
                stopService.IsEnabled = false;
                if (s == "Running")
                {
                    stopService.IsEnabled = true;
                    //startService.IsEnabled = false;
                }
                if (s == "Stopped")
                {
                    startService.IsEnabled = true;
                    //stopService.IsEnabled = false;
                }
            }*/
        }

        private void startService_Click(object sender, RoutedEventArgs e)
        {
            if (StartService != null)
                StartService(null, EventArgs.Empty);
        }

        private void stopService_Click(object sender, RoutedEventArgs e)
        {
            if (StopService != null)
                StopService(null, EventArgs.Empty);
        }
    }
}
