﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

#nullable disable

namespace BreathInDrinkUITest.Models
{
    public partial class Drinkers
    {
        public Drinkers()
        {
            Promille = new HashSet<Promille>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Promille> Promille { get; set; }
    }
}