using System;
using System.Collections.Generic;
using System.Text;

namespace Hotel.Application.DTOs.Habitaciones
{
    public class HabitacionDto
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int TipoHabitacionId { get; set; }
        public string TipoHabitacionNombre { get; set; } = null!;
        public int Capacidad { get; set; }
        public string Estado { get; set; } = null!;
    }
}
