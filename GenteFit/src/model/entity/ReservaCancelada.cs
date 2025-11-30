using System;

namespace GenteFit.src.model.entity
{
    public class ReservaCancelada
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int SesionId { get; set; }
        public DateTime FechaCancelacion { get; set; } = DateTime.Now;
        public string? Motivo { get; set; }
        public int? PosicionEnCancelacion { get; set; }
        public string EstadoPrevio { get; set; } = "";
    }
}

