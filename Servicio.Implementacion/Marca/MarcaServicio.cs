using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dominio.Entidades.UnidadDeTrabajo;
using Servicio.Interfaces.Marca;
using Servicio.Interfaces.Marca.DTOs;
using StructureMap;

namespace Servicio.Implementacion.Marca
{
    public class MarcaServicio : IMarcaServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public MarcaServicio()
        {
            _unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
        }
        public long Add(MarcaDtos entidad)
        {
            var entidadId = _unidadDeTrabajo.MarcaRepositorio.Insertar(new Dominio.Entidades.Marca
            {
                Descripcion = entidad.Descripcion,
                EstaEliminado = false,
            });

            _unidadDeTrabajo.Commit();

            return entidadId;
        }

        public void Delete(long id)
        {
            var entidad = _unidadDeTrabajo.MarcaRepositorio.Obtener(id);

            _unidadDeTrabajo.MarcaRepositorio.Eliminar(entidad);

            _unidadDeTrabajo.Commit();
        }

        public IEnumerable<MarcaDtos> Get(string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.Marca, bool>> filtro = marca =>
           !marca.EstaEliminado && marca.Descripcion.Contains(cadenaBuscar);

            var resultado = _unidadDeTrabajo.MarcaRepositorio.Obtener(filtro);

            return resultado.Select(x => new MarcaDtos
            {
                Descripcion = x.Descripcion,
                RowVersion = x.RowVersion,
                Id = x.Id,
                EstaEliminado = x.EstaEliminado
            }).ToList();
        }

        public MarcaDtos GetById(long id)
        {
            var resultado = _unidadDeTrabajo.MarcaRepositorio.Obtener(id);

            return new MarcaDtos
            {
                Descripcion = resultado.Descripcion,
                Id = resultado.Id,
                RowVersion = resultado.RowVersion,
                EstaEliminado = resultado.EstaEliminado,
            };
        }

        

        public void Update(MarcaDtos entidad)
        {
            var entidadModificar = _unidadDeTrabajo.MarcaRepositorio.Obtener(entidad.Id);

            entidadModificar.Descripcion = entidad.Descripcion;

            _unidadDeTrabajo.MarcaRepositorio.Modificar(entidadModificar);

            _unidadDeTrabajo.Commit();
          
        }
    }
}
