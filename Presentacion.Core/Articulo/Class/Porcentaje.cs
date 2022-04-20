namespace Presentacion.Core.Articulo.Clases
{
    public static class Porcentaje
    {
        public static decimal Calcular(decimal monto, decimal porcentaje)
        {
            return monto * (porcentaje / 100m);
        }
    }
}
