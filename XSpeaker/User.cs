using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace XSpeaker
{
    [Serializable()]
    public class User
    {
        //TODO
        public readonly int Port = 7000;
        public string Name { set; get; }
        public IPAddress IP { set; get; }
        /// <summary>
        /// Создает экземпляр класса User с заданным именем и IP-адресом.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <param name="IP">IP-адрес пользователя.</param>
        public User(string UserName, IPAddress UserIP)
        {
            Name = UserName;
            IP = UserIP;
        }
        /// <summary>
        /// Создает экземпляр класса User.
        /// </summary>
        public User() { }
    }
}
