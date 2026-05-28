using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.DTOs.Habitaciones
{
    public class HabitacionEditarDto
    {
        public int Numero { get; set; }
        public int TipoHabitacionId { get; set; }
        public int Capacidad { get; set; }
        public string Estado { get; set; } = null!;
    }
}
