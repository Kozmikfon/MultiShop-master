using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MultiShop.Cargo.DtoLayer.Dtos.ShipinkDtos
{
    public class ShipinkPackageDto
    {
        [JsonPropertyName("dimension_unit")]
        public string dimension_unit { get; set; } = "cm";

        [JsonPropertyName("height")]
        public int height { get; set; }

        [JsonPropertyName("length")]
        public int length { get; set; }

        [JsonPropertyName("width")]
        public int width { get; set; }

        [JsonPropertyName("weight")]
        public double weight { get; set; }

        [JsonPropertyName("weight_unit")]
        public string weight_unit { get; set; } = "kg";
    }

}
