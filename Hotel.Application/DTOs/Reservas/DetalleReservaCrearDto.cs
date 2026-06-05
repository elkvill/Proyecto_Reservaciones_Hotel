using System.ComponentModel.DataAnnotations;

namespace Hotel.Application.DTOs.Reservas
{
    public class DetalleReservaCrearDto
    {
        //public int ReservaId { get; set; }
        //public int HabitacionId { get; set; }
        ////public int HabitacionNumero { get; set; } no estoy seguro si es necesario enviar el numero de habitacion,
        ////ya que se puede obtener a través del id de la habitacion
        //public decimal PrecioPorNoche { get; set; }
        //public int CantidadDeNoches { get; set; }
        //public decimal Subtotal { get; set; }



        [Required(ErrorMessage = "El ID de la habitación es requerido.")]
        public int HabitacionId { get; set; }

        [Required(ErrorMessage = "El precio por noche es requerido.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio por noche debe ser mayor a cero.")]
        public decimal PrecioPorNoche { get; set; }
    }
}
