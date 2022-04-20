using System;

namespace Servicio.Interfaces.Articulo.DTOs
{
    public class ArticuloVentaDto
    {
        public long Articulo { get; set; }
        public int Codigo { get; set; }
        public string CodigoBarra { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }

        //Restricciones 

        public bool ActivarLimiteVenta { get; set; }
        public decimal LimiteVenta { get; set; }
        public bool ActivarHoraVenta { get; set; }
        public DateTime HoraLimiteVenta { get; set; }
        public bool PermiteStockNegativo { get; set; }
        public bool DescuentaStock { get; set; }
    }
}
