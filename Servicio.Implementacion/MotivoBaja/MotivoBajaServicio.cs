namespace Servicio.Implementacion.MotivoBaja
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Dominio.Entidades.UnidadDeTrabajo;
    using Servicio.Interfaces.MotivoBaja;
    using Servicio.Interfaces.MotivoBaja.DTOs;
    using StructureMap;

    public class MotivoBajaServicio : IMotivoBajaServicio
    {
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        public MotivoBajaServicio()
        {
            unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();
        }

        public long Add(MotivoBajaDto entidad)
        {
            var entidadId = unidadDeTrabajo.MotivoBajaRepositorio.Insertar(new Dominio.Entidades.MotivoBaja
            {
                EstaEliminado = false,
                Descripcion = entidad.Descripcion
            });

            unidadDeTrabajo.Commit();

            return entidadId;
        }

        public void Delete(long id)
        {
            var entidad = unidadDeTrabajo.MotivoBajaRepositorio.Obtener(id);

            unidadDeTrabajo.MotivoBajaRepositorio.Eliminar(entidad);

            unidadDeTrabajo.Commit();
        }

        public IEnumerable<MotivoBajaDto> Get(string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.MotivoBaja, bool>> filtro = motivobaja =>
           !motivobaja.EstaEliminado && motivobaja.Descripcion.Contains(cadenaBuscar);

            var resultado = unidadDeTrabajo.MotivoBajaRepositorio.Obtener(filtro);

            return resultado.Select(x => new MotivoBajaDto
            {
                Descripcion = x.Descripcion,
                EstaEliminado = x.EstaEliminado,
                RowVersion = x.RowVersion,
                Id = x.Id
            }).ToList();
        }

        public MotivoBajaDto GetById(long id)
        {
            var entidad = unidadDeTrabajo.MotivoBajaRepositorio.Obtener(id);

            return new MotivoBajaDto
            {
                Descripcion = entidad.Descripcion,
                EstaEliminado = entidad.EstaEliminado,
                RowVersion = entidad.RowVersion,
                Id = entidad.Id
            };
        }

        public void Update(MotivoBajaDto entidad)
        {
            var entidadModificar = unidadDeTrabajo.MotivoBajaRepositorio.Obtener(entidad.Id);

            entidadModificar.Descripcion = entidad.Descripcion;

            unidadDeTrabajo.MotivoBajaRepositorio.Modificar(entidadModificar);

            unidadDeTrabajo.Commit();
        }
    }
}
