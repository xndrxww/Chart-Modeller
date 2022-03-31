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

        //public SeriesCollection SeriesCollection { get; set; }

        private SeriesCollection seriesCollection;
        [XmlIgnore]
        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set { seriesCollection = value; }
        }

        [XmlElement("SeriesCollection")]
        public object MySeriesSerializable
        {
            get { return SeriesCollection; }
            set { SeriesCollection = value as SeriesCollection; }
        }
    }
}
