using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Models.Helpers {
    public class ConfigurationAddOrUpdate {
        [MaxLength(64), Required]
        public string Name { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int CPUID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int GPUID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int RAMID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int MBID { get; set; }

        [NotNull, Range(1, int.MaxValue), Required]
        public int STORAGEID { get; set; }
    }
}