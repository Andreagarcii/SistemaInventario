using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.Modelos
{
    public class Bodega
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(60, ErrorMessage = "El nombre no es mas de 60 carateres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        [MaxLength(60, ErrorMessage = "El nombre no es mas de 100 carateres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "El campo es requerido")]
        public bool Estado { get; set; }    
    }
}
