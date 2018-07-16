using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Windows;

namespace model
{
    public interface IModel
    {
        void SendMess(Socket handler, string str2);
        string GetMess(Socket socket);

    }
    public class ClassModel:IModel
    {
        public void SendMess(Socket handler, string str2)
        {
            try
            {
                byte[] msg = Encoding.UTF8.GetBytes(str2);
                handler.Send(msg);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка отправки:" +Environment.NewLine+ ex.Message);
            }
        }

        public string GetMess(Socket socket)
        {
            try
            {
                byte[] bytes = new byte[10240];

                int bytesRec = socket.Receive(bytes);

                return Encoding.UTF8.GetString(bytes, 0, bytesRec);
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ошибка приема:" + Environment.NewLine + ex.Message);
                return null;
            }
        }
    }
}
