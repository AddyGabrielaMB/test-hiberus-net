﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TestHiberusNet.Models
{
    public partial class Terminales
    {
        public int IdTerminal { get; set; }
        public int IdFab { get; set; }
        public int IdEstado { get; set; }
        public DateTime FechaFabricacion { get; set; }
        public DateTime FechaEstado { get; set; }
        public string TerminalDesc { get; set; }
        public string TerminalName { get; set; }

        public virtual Estado IdEstadoNavigation { get; set; }
        public virtual Fabricantes IdFabNavigation { get; set; }
    }
}