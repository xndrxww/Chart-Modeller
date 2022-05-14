using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;
using LiveCharts;
using LiveCharts.Wpf;

namespace Chart_Modeller.Models
{
    //Класс для работы с данными графиков
    public class Chart
    {
        public Chart()
        {

        }

        public Chart(string name)
        {
            Name = name;
        }

        [XmlAttribute]
        public string Name { get; set; }
        
        [XmlAttribute]
        public string Type { get; set; }

        public List<string> LabelsList = new List<string>(); 

        public List<Value> Values = new List<Value>();
    }
}
