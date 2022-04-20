namespace Presentacion.Core.Articulo.Clases
{
    using Aplicacion.Constantes.Clases;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FacturaView
    {
        public FacturaView()
        {
            PorcentajeDescuento = 0m;

            if (Items == null)
            {
                Items = new List<ItemsView>();
            }

        }

        public TipoComprobante TipoComprobante { get; set; }

        public int NumeroDeFactura { get; set; }

        public long VendedorId { get; set; }

        public string ApYNomDelVendedor { get; set; }

        public DateTime Fecha { get; set; }

        public long ClienteId { get; set; }

        public string ApYNomDelCliente { get; set; }

        public string CuilDelCliente { get; set; }

        public string DireccionDelCliente { get; set; }

        public string CondicionIvaDelCliente { get; set; }

        public long ListaDePrecioID { get; set; }

        public List<ItemsView> Items { get; set; }

        public decimal SubTotal => Items.Sum(x => x.SubTotal);

        public decimal PorcentajeDescuento { get; set; }

        public decimal MontoDeDescuento
        {
            get => Porcentaje.Calcular(SubTotal, PorcentajeDescuento);
        }

        public decimal Total => SubTotal - MontoDeDescuento;
    }
}
