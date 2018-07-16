using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using model;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Windows;

namespace WinClient
{
    class Presenter
    {
        private readonly IMainForm _view;
        private readonly IModel _perf;

        string str;
        Socket socket;
        string[] array1;

        public Presenter (IMainForm view, IModel perf)
        {
            view.ShowForm();

            _view = view;
            _perf = perf;

            _view.Connect += _view_Connect;
            _view.GetInfo += _view_GetInfo;
            _view.StartService += _view_StartService;
            _view.StopService += _view_StopService;
        }

        void _view_StopService(object sender, EventArgs e)
        {
            _perf.SendMess(socket, "stop");
            str = _perf.GetMess(socket);
            if (str == "esc")
            {
                _view.ContentText = Environment.NewLine + "сервер разорвал соединение";
                _view.ListServiceVisible = false;
                _view.ConnectVisible = true; 
                _view.StartVisible = false; 
                _view.StopVisible = false; 
            }
            else
            {
                _view.ContentText = Environment.NewLine + str;
                
                string s = str.Substring(str.Length - 7, 7);
                _view.StartVisible = false; 
                _view.StopVisible = false;
                if (s == "Stopped")
                    _view.StartVisible = true; 
                if (s == "Running")
                    _view.StopVisible = true;
                if (s == "Pending")
                    _view_GetInfo(sender, e);
            }
        }

        void _view_StartService(object sender, EventArgs e)
        {
            _perf.SendMess(socket, "start");
            str = _perf.GetMess(socket);
            if (str == "esc")
            {
                _view.ContentText = Environment.NewLine + "сервер разорвал соединение";
                _view.ListServiceVisible = false;
                _view.ConnectVisible = true;
                _view.StartVisible = false;
                _view.StopVisible = false;
            }
            else
            {
                _view.ContentText = Environment.NewLine + str;
                
                string s = str.Substring(str.Length - 7, 7);
                _view.StartVisible = false;
                _view.StopVisible = false; 
                if (s == "Stopped")
                    _view.StartVisible = true; 
                if (s == "Running")
                    _view.StopVisible = true; 
                if (s == "Pending")
                    _view_GetInfo(sender, e);
            }
        }

        void _view_GetInfo(object sender, EventArgs e)
        {
            string serv = array1[_view.indexServ].ToString();
            _perf.SendMess(socket, serv);
            str = _perf.GetMess(socket);
            if (str == "esc")
            {
                _view.ContentText = Environment.NewLine + "сервер разорвал соединение";
                _view.ListServiceVisible = false;
                _view.ConnectVisible = true;
                _view.StartVisible = false;
                _view.StopVisible = false;
            }
            else
            {
                string s = str.Substring(str.Length - 7, 7);
                _view.StartVisible = false; 
                _view.StopVisible = false; 
                if (s == "Running")
                {
                    _view.ContentText = Environment.NewLine + str;
                    _view.StopVisible = true; 
                }
                if (s == "Stopped")
                {
                    _view.ContentText = Environment.NewLine + str;
                    _view.StartVisible = true;
                }
                if (s == "Pending")
                    _view_GetInfo(sender, e);
            }
        }

        void _view_Connect(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint IP_End = new IPEndPoint(_view.IP_Address, _view.Port);
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Connect(IP_End);
                if (socket.Connected)
                {
                    _view.ContentText = Environment.NewLine + Environment.NewLine + "подключен к серверу"; 
                    _view.ContentText = Environment.NewLine + "ожидание от сервера списка служб...";
                    
                    str = _perf.GetMess(socket);
                    array1 = str.Split(',');
                    _view.array = array1;
                    _view.ContentText = Environment.NewLine + "список служб получен";
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                _view.ConnectVisible = true;
            }
        }
    }
}
