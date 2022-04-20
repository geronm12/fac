namespace Presentacion.Core.Articulo.Clases
{
    public class ItemsView
    {
        public ItemsView()
        {
            Cantidad = 0m;
            PrecioArticulo = 0m;
        }
        public long ArticuloId { get; set; }

        public long ListaPrecioId { get; set; }

        public long Codigoarticulo { get; set; }

        public string CodigoDeBarraArticulo { get; set; }

        public string DescripcionArticulo { get; set; }

        public decimal PrecioArticulo { get; set; }

        public decimal Cantidad { get; set; }

        public decimal SubTotal => Cantidad * PrecioArticulo;
    }
}
