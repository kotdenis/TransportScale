using System.ComponentModel.DataAnnotations;

namespace TransportScale.Dto.DtoModels
{
    public class TransportModel
    {
        [Required(ErrorMessage = "Brand of car is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Car plate is required")]
        public string Number { get; set; } = string.Empty;

        [Required(ErrorMessage = "Weight is required")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Number of axles is required")]
        public int AxisNumber { get; set; }

        [Required(ErrorMessage = "Imported cargo is required")]
        public string Cargo { get; set; } = string.Empty;
    }
}
