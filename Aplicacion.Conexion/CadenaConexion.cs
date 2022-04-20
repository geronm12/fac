namespace Aplicacion.Conexion
{
    public static class CadenaConexion
    {
        private const string BaseDatos = "XCommerceDb";
        private const string Servidor = @"DESKTOP-U0O3HC5\SQLEXPRESS";
        private const string Usuario = "sa";
        private const string Password = "4518801";

        public static string ObtenerCadenaConexion => $"Data Source ={Servidor}; " +
                                                      $"Initial Catalog={BaseDatos}; " +
                                                      $"User Id={Usuario}; " +
                                                      $"Password={Password};";
    }
}
