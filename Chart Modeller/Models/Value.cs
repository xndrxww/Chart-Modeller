using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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
    }
}
