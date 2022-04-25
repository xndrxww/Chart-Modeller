using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Chart_Modeller.Models
{
    public class Value
    {
        public Value()
        {

        }

        public ArrayList ValuesList = new ArrayList();
        public Color StrokeColor { get; set; }
        public Color FillColor { get; set; }
        
        [XmlAttribute]
        public string Title { get; set; }
    }
}
