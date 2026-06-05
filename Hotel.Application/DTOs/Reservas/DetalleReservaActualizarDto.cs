using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.DTOs.Reservas
{
    public class DetalleReservaActualizarDto
    {
        public int HabitacionId { get; set; }
        public decimal PrecioPorNoche { get; set; }
        public int CantidadDeNoches { get; set; }
        public decimal Subtotal { get; set; }
    }
}
