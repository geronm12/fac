using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dominio.Entidades.UnidadDeTrabajo;
using Servicio.Interfaces.ListaDePrecio;
using Servicio.Interfaces.ListaDePrecio.DTOs;
using StructureMap;

namespace Servicio.Implementacion.ListaDePrecios
{
    public class ListaDePrecioServicios : IListaDePrecioServicios
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        public ListaDePrecioServicios()
        {
            _unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
        }
        public long Add(ListaDePrecioDto entidad)
        {
            var entidadId = _unidadDeTrabajo.ListaPrecioRepositorio.Insertar(new Dominio.Entidades.ListaPrecio
            {
                EstaEliminado = false,
                Descripcion = entidad.Descripcion,
                PorcentajeGanancia = entidad.PorcentajeGanancia,
                NecesitaAutorizacion = entidad.NecesitaAutorizacion,
            }
           );

            _unidadDeTrabajo.Commit();

            return entidadId;
        }

        public void Delete(long id)
        {
            var entidad = _unidadDeTrabajo.ListaPrecioRepositorio.Obtener(id);

            _unidadDeTrabajo.ListaPrecioRepositorio.Eliminar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public IEnumerable<ListaDePrecioDto> Get(string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.ListaPrecio, bool>> filtro = ListaPrecio =>
                !ListaPrecio.EstaEliminado && ListaPrecio.Descripcion.Contains(cadenaBuscar);

            var resultado = _unidadDeTrabajo.ListaPrecioRepositorio.Obtener(filtro);

            return resultado.Select(x => new ListaDePrecioDto
            {
                Id = x.Id,
                EstaEliminado = x.EstaEliminado,
                Descripcion = x.Descripcion,
                PorcentajeGanancia = x.PorcentajeGanancia,
                NecesitaAutorizacion = x.NecesitaAutorizacion,
                RowVersion = x.RowVersion
            }).ToList();
        }

        public ListaDePrecioDto GetById(long id)
        {
            var resultado = _unidadDeTrabajo.ListaPrecioRepositorio.Obtener(id);

            return new ListaDePrecioDto
            {
                Id = resultado.Id,
                EstaEliminado = resultado.EstaEliminado,
                Descripcion = resultado.Descripcion,
                PorcentajeGanancia = resultado.PorcentajeGanancia,
                NecesitaAutorizacion = resultado.NecesitaAutorizacion,
                RowVersion = resultado.RowVersion
            };
        }

        public void Update(ListaDePrecioDto entidad)
        {
            var entidadModificar = _unidadDeTrabajo.ListaPrecioRepositorio.Obtener(entidad.Id);

            entidadModificar.Descripcion = entidad.Descripcion;
            entidadModificar.PorcentajeGanancia = entidad.PorcentajeGanancia;
            entidadModificar.NecesitaAutorizacion = entidad.NecesitaAutorizacion;

            _unidadDeTrabajo.ListaPrecioRepositorio.Modificar(entidadModificar);

            _unidadDeTrabajo.Commit();
        }
    }
}
