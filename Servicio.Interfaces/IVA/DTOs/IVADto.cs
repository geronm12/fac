namespace Servicio.Interfaces.IVA.DTOs
{
    using Servicio.Interfaces.Base;
    public class IVADto : BaseDto
    {
        public string Descripcion { get; set; }

        public decimal Porcentaje { get; set; }
    }
}
