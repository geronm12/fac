namespace Servicio.Interfaces.ListaDePrecio.DTOs
{
    using Servicio.Interfaces.Base;

    public class ListaDePrecioDto : BaseDto
    {
        public string Descripcion { get; set; }

        public decimal PorcentajeGanancia { get; set; }

        public bool NecesitaAutorizacion { get; set; }
    }
}
