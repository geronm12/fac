namespace Servicio.Implementacion.Rubro
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Dominio.Entidades.UnidadDeTrabajo;
    using Servicio.Interfaces.Rubro;
    using Servicio.Interfaces.Rubro.RubroDTOs;
    using StructureMap;

    public class RubroServicio : IRubroServicio
    {
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public RubroServicio()
        {
            unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
        }

        public long Add(RubroDto entidad)
        {
            var entidadId = unidadDeTrabajo.RubroRepositorio.Insertar(new Dominio.Entidades.Rubro
            {
                EstaEliminado = false,
                Descripcion = entidad.Descripcion,
            });

            unidadDeTrabajo.Commit();
            return entidadId;
        }

        public void Delete(long id)
        {
            var entidadId = unidadDeTrabajo.RubroRepositorio.Obtener(id);
           
            unidadDeTrabajo.RubroRepositorio.Eliminar(entidadId);

            unidadDeTrabajo.Commit();
        }

        public IEnumerable<RubroDto> Get(string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.Rubro, bool>> filtro = rubro =>
           !rubro.EstaEliminado && rubro.Descripcion.Contains(cadenaBuscar);

            var resultado = unidadDeTrabajo.RubroRepositorio.Obtener(filtro);

            return resultado.Select(x => new RubroDto
            {
                EstaEliminado = x.EstaEliminado,
                Descripcion  = x.Descripcion,
                RowVersion = x.RowVersion,
                Id = x.Id,
             }).ToList();
        }

        public RubroDto GetById(long id)
        {
            var resultado = unidadDeTrabajo.RubroRepositorio.Obtener(id);

            return new RubroDto
            {
                Descripcion = resultado.Descripcion,
                EstaEliminado = resultado.EstaEliminado,
                RowVersion = resultado.RowVersion,
                Id = resultado.Id
            };
        }

        public void Update(RubroDto entidad)
        {
            var entidadModificar = unidadDeTrabajo.RubroRepositorio.Obtener(entidad.Id);

            entidadModificar.Descripcion = entidad.Descripcion;

            unidadDeTrabajo.RubroRepositorio.Modificar(entidadModificar);

            unidadDeTrabajo.Commit();
        }
    }
}
