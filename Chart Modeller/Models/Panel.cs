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

        public Panel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
    }
}
