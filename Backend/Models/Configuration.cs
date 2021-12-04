using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

using Models.Parts;

namespace Models {
    public class Configuration {

        [Key]
        public int ID { get; set; }

        [MaxLength(64), Required]
        public string Name { get; set; }

        [Required]
        public Processor CPU { get; set; }

        [Required]
        public GraphicsCard GPU { get; set; }

        [Required]
        public RAM RAM { get; set; }

        [Required]
        public Motherboard MB { get; set; }

        [Required]
        public Storage STORAGE { get; set; }

    }
}