namespace Servicio.Implementacion.Articulo
{
    using Dominio.Entidades.UnidadDeTrabajo;
    using Servicio.Interfaces.Articulo;
    using Servicio.Interfaces.Articulo.DTOs;
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class ArticuloServicio : IArticuloServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public ArticuloServicio()
        {
            _unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
        }
       
        public long Add(ArticuloDto entidad)
        {
            var entidadId = _unidadDeTrabajo.ArticuloRepositorio.Insertar(new Dominio.Entidades.Articulo
            {
                
                EstaEliminado = false,
                Descripcion = entidad.Descripcion,
                ActivarHoraVenta = entidad.ActivarHoraVenta,
                ActivarLimiteVenta = entidad.ActivarLimiteVenta,
                HoraLimiteVenta = entidad.HoraLimiteVenta,
                Abreviatura = entidad.Abreviatura,
                Codigo = entidad.Codigo,
                CodigoBarra = entidad.CodigoBarra,
                CodigoDeProveedor = entidad.CodigoDeProveedor,
                DescuentaStock = entidad.DescuentaStock,
                Detalle = entidad.Detalle,
                PrecioCosto = entidad.PrecioCosto,
                Foto = entidad.Foto,
                MarcaId = entidad.MarcaId,
                IvaId = entidad.IvaId,
                LimiteVenta = entidad.LimiteVenta,
                PermiteStockNegativo = entidad.PermiteStockNegativo,
                RubroId = entidad.RubroId,
                Stock = entidad.Stock,
                StockMinimo = entidad.StockMinimo,
                TipoArticulo = entidad.TipoArticulo,
                Ubicacion = entidad.Ubicacion,
                UnidadMedidaId = entidad.UnidadMedidaId,
                

            });

            _unidadDeTrabajo.Commit();

            return entidadId;
        }

       
         public void Delete(long id)
        {
            var entidad = _unidadDeTrabajo.ArticuloRepositorio.Obtener(id);
           
            _unidadDeTrabajo.ArticuloRepositorio.Eliminar(entidad);
           
            _unidadDeTrabajo.Commit();
        }

        public IEnumerable<ArticuloDto> Get(string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.Articulo, bool>> filtro = articulo =>
           !articulo.EstaEliminado && articulo.Descripcion.Contains(cadenaBuscar);

            var resultad = _unidadDeTrabajo.ArticuloRepositorio.Obtener(filtro);

            return resultad.Select(x => new ArticuloDto
            {
                Id = x.Id,
                EstaEliminado = x.EstaEliminado,
                Descripcion = x.Descripcion,
                ActivarHoraVenta = x.ActivarHoraVenta,
                ActivarLimiteVenta = x.ActivarLimiteVenta,
                HoraLimiteVenta = x.HoraLimiteVenta,
                Abreviatura = x.Abreviatura,
                Codigo = x.Codigo,
                CodigoBarra = x.CodigoBarra,
                CodigoDeProveedor = x.CodigoDeProveedor,
                DescuentaStock = x.DescuentaStock,
                Detalle = x.Detalle,
                PrecioCosto = x.PrecioCosto,
                Foto = x.Foto,
                MarcaId  = x.MarcaId,
                IvaId = x.IvaId,
                LimiteVenta = x.LimiteVenta,
                PermiteStockNegativo = x.PermiteStockNegativo,
                RubroId = x.RubroId,
                Stock = x.Stock,
                StockMinimo = x.StockMinimo,
                Ubicacion = x.Ubicacion,
                UnidadMedidaId = x.UnidadMedidaId,
                TipoArticulo = x.TipoArticulo,
                RowVersion = x.RowVersion,
              }).ToList();
        }

        public ArticuloVentaDto GetByCodigo(string codigo, long listaPrecioId)
        {
            int _codigo = -1;

            int.TryParse(codigo, out _codigo);

            Expression<Func<Dominio.Entidades.Articulo, bool>> filtro = x =>
           !x.EstaEliminado && (x.Codigo == _codigo || x.CodigoBarra == codigo)
           && x.Precios.Any(p => p.ListaPrecioId == listaPrecioId);

            var articulo = _unidadDeTrabajo.ArticuloRepositorio.Obtener(filtro, "Precios").FirstOrDefault();

            if(articulo != null)
            {
                var fechaActual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

                return new ArticuloVentaDto
                {
                    Articulo = articulo.Id,
                    Codigo = articulo.Codigo,
                    CodigoBarra = articulo.CodigoBarra,
                    Descripcion = articulo.Descripcion,
                    ActivarHoraVenta = articulo.ActivarHoraVenta,
                    ActivarLimiteVenta = articulo.ActivarLimiteVenta,
                    DescuentaStock = articulo.DescuentaStock,
                    HoraLimiteVenta = articulo.HoraLimiteVenta,
                    LimiteVenta = articulo.LimiteVenta,
                    
                    PermiteStockNegativo = articulo.PermiteStockNegativo,
                    Precio = articulo.Precios.FirstOrDefault(x => x.ListaPrecioId == listaPrecioId
                                                                && x.FechaActualizacion == articulo.Precios
                                                                .Where(p => p.ListaPrecioId == listaPrecioId &&
                                                                p.FechaActualizacion <= fechaActual).Max(f => f.FechaActualizacion)).PrecioPublico
                };
            }

            return null;
        }

        public ArticuloDto GetById(long id)
        {
            var resultado = _unidadDeTrabajo.ArticuloRepositorio.Obtener(id);

            return new ArticuloDto
            {
                Id = resultado.Id,
                EstaEliminado = resultado.EstaEliminado,
                Descripcion = resultado.Descripcion,
                ActivarHoraVenta = resultado.ActivarHoraVenta,
                ActivarLimiteVenta = resultado.ActivarLimiteVenta,
                HoraLimiteVenta = resultado.HoraLimiteVenta,
                Abreviatura = resultado.Abreviatura,
                Codigo = resultado.Codigo,
                CodigoBarra = resultado.CodigoBarra,
                CodigoDeProveedor = resultado.CodigoDeProveedor,
                DescuentaStock = resultado.DescuentaStock,
                Detalle = resultado.Detalle,
                PrecioCosto = resultado.PrecioCosto,
                Foto = resultado.Foto,
                MarcaId = resultado.MarcaId,
                IvaId = resultado.IvaId,
                LimiteVenta = resultado.LimiteVenta,
                PermiteStockNegativo = resultado.PermiteStockNegativo,
                RubroId = resultado.RubroId,
                Stock = resultado.Stock,
                TipoArticulo = resultado.TipoArticulo,
                StockMinimo = resultado.StockMinimo,
                Ubicacion = resultado.Ubicacion,
                UnidadMedidaId = resultado.UnidadMedidaId,
                RowVersion = resultado.RowVersion,
            };
        }

        public void Update(ArticuloDto entidad)
        {
            var entidadModificar = _unidadDeTrabajo.ArticuloRepositorio.Obtener(entidad.Id);

            entidadModificar.Descripcion = entidad.Descripcion;
            entidadModificar.Abreviatura = entidad.Abreviatura;
            entidadModificar.ActivarHoraVenta = entidad.ActivarHoraVenta;
            entidadModificar.ActivarLimiteVenta = entidad.ActivarLimiteVenta;
            entidadModificar.LimiteVenta = entidad.LimiteVenta;
            entidadModificar.HoraLimiteVenta = entidad.HoraLimiteVenta;
            entidadModificar.Codigo = entidad.Codigo;
            entidadModificar.CodigoBarra = entidad.CodigoBarra;
            entidadModificar.CodigoDeProveedor = entidad.CodigoDeProveedor;
            entidadModificar.DescuentaStock = entidad.DescuentaStock;
            entidadModificar.Detalle = entidad.Detalle;
            entidadModificar.PrecioCosto = entidad.PrecioCosto;
            entidadModificar.Foto = entidad.Foto;
            entidadModificar.Ubicacion = entidad.Ubicacion;
            entidadModificar.MarcaId = entidad.MarcaId;
            entidadModificar.RubroId = entidad.RubroId;
            entidadModificar.UnidadMedidaId = entidad.UnidadMedidaId;
            entidadModificar.IvaId = entidad.IvaId;
            entidadModificar.TipoArticulo = entidad.TipoArticulo;


            _unidadDeTrabajo.ArticuloRepositorio.Modificar(entidadModificar);

            _unidadDeTrabajo.Commit();
        }
    }
}
