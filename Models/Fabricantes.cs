﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TestHiberusNet.Models
{
    public partial class Fabricantes
    {
        public Fabricantes()
        {
            Terminales = new HashSet<Terminales>();
        }

        public int IdFab { get; set; }
        public string FabName { get; set; }
        public string FabDesc { get; set; }

        public virtual ICollection<Terminales> Terminales { get; set; }
    }
}