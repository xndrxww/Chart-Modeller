﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chart_Modeller.Models
{
    public class Panel
    {
        public Panel()
        {

        }

        public Panel(string name)
        {
            Name = name;
        }

        [XmlAttribute]
        public string Name { get; set; }

        public List<Chart> Charts = new List<Chart>();
    }
}
