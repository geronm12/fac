using Servicio.Interfaces.ListaDePrecio.DTOs;
using System.Collections.Generic;

namespace Servicio.Interfaces.ListaDePrecio
{
    public interface IListaDePrecioServicios
    {
        long Add(ListaDePrecioDto entidad);

        void Update(ListaDePrecioDto entidad);

        void Delete(long id);

        IEnumerable<ListaDePrecioDto> Get(string cadenaBuscar);

        ListaDePrecioDto GetById(long id);
    }
}
