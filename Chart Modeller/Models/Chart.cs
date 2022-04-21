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

        public string Title { get; set; } //сделать лист
        //public List<Color> StrokeColor = new List<Color>(); //сделать лист
        //public List<Color> FillColor = new List<Color>(); //сделать лист

        public Color StrokeColor { get; set; }
        public Color FillColor { get; set; }

        public List<string> LabelsList = new List<string>(); 

        public ArrayList ValuesList = new ArrayList();
    }
}
