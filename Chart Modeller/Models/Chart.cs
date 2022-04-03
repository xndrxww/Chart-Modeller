using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using LiveCharts;

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

        //public List<int>  ValuesInt = new List<int>();

        //public List<decimal> ValuesDecimal = new List<decimal>();

        //public List<double> ValuesDouble = new List<double>();
    }
}
