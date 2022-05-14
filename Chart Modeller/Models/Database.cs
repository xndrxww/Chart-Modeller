using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chart_Modeller.Models
{
    //Класс для работы с данными баз данных
    public class Database
    {
        public Database()
        {

        }

        public Database(string name)
        {
            Name = name;
        }

        [XmlAttribute]
        public string Name { get; set; }

        public List<Panel> Panels = new List<Panel>();
    }
}
