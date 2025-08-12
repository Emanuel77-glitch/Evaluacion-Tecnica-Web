using System.ComponentModel.DataAnnotations;

namespace Evaluacion_Tecnica_Web.Models
{
    public class Producto
    {
        public int id { get; set; }
        
        public string nombre { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que cero.")]
        public decimal Precio { get; set; }

        public string categoria { get; set; }
    }
}
