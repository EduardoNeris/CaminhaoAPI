using CaminhaoAPI.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace CaminhaoAPI.Models
{
    public class Caminhao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public ModeloEnum Modelo { get; set; }

        [Required]
        [Range(1900, 2100)]
        public int AnoFabricacao { get; set; }

        [Required]
        [StringLength(17)]
        public int CodigoChassi { get; set; }

        [Required]
        [StringLength(30)]
        public string? Cor { get; set; }

        public PlantaEnum Planta { get; set; }
    }
}
