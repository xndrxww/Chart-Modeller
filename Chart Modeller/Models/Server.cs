using Chart_Modeller.Models;
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

        [XmlAttribute]
        public string ServerName { get; set; }

        [XmlAttribute]
        public string Login { get; set; }

        [XmlAttribute]
        public string Password { get; set; }

        public List<Database> Databases  = new List<Database>();
    }
}
