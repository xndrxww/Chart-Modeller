using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chart_Modeller
{
    public class Server
    {
        public Server()
        {
            
        }
        public Server(string serverName, string login, string password)
        {
            ServerName = serverName;
            Login = login;
            Password = password;
        }

        public string ServerName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
