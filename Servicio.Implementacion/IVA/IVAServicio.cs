namespace Servicio.Implementacion.IVA
{
    using Dominio.Entidades.UnidadDeTrabajo;
    using Servicio.Interfaces.IVA;
    using Servicio.Interfaces.IVA.DTOs;
    using StructureMap;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class IVAServicio : IIVAServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public IVAServicio()
        {

            _unidadDeTrabajo = ObjectFactory.GetInstance<IUnidadDeTrabajo>();

        }
        public long Add(IVADto entidad)
        {
            var entidadId = _unidadDeTrabajo.IvaRepositorio.Insertar(new Dominio.Entidades.Iva
            {
                EstaEliminado = false,
                Descripcion = entidad.Descripcion,
                Id = entidad.Id,
                 Porcentaje = entidad.Porcentaje,
                RowVersion = entidad.RowVersion,
            }) ;

            _unidadDeTrabajo.Commit();

            return entidadId;
        }

        public void Delete(long id)
        {
            var entidadId = _unidadDeTrabajo.IvaRepositorio.Obtener(id);

            _unidadDeTrabajo.IvaRepositorio.Eliminar(entidadId);

            _unidadDeTrabajo.Commit();
        }

        public IEnumerable<IVADto> Get(string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.Iva, bool>> filtro = iva =>
         !iva.EstaEliminado && iva.Descripcion.Contains(cadenaBuscar);

            var resultado = _unidadDeTrabajo.IvaRepositorio.Obtener(filtro);

            return resultado.Select(x => new IVADto
            {
                Id = x.Id,
                EstaEliminado = x.EstaEliminado,
                Descripcion = x.Descripcion,
                Porcentaje = x.Porcentaje,
                RowVersion = x.RowVersion,
            }).ToList();
               

            
        }

        public IVADto GetById(long id)
        {
            var x = _unidadDeTrabajo.IvaRepositorio.Obtener(id);

            return new IVADto
            {
                Id = x.Id,
                EstaEliminado = x.EstaEliminado,
                Descripcion = x.Descripcion,
                Porcentaje = x.Porcentaje,
                RowVersion = x.RowVersion,
            };
        }

        public void Update(IVADto entidad)
        {
            var entidadModificar = _unidadDeTrabajo.IvaRepositorio.Obtener(entidad.Id);

            entidadModificar.Descripcion = entidad.Descripcion;

            entidadModificar.Porcentaje = entidad.Porcentaje;

            _unidadDeTrabajo.IvaRepositorio.Modificar(entidadModificar);

            _unidadDeTrabajo.Commit();
           
        }
    }
}
