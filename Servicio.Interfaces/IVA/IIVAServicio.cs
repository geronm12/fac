using Servicio.Interfaces.IVA.DTOs;
using System.Collections.Generic;

namespace Servicio.Interfaces.IVA
{
    public interface IIVAServicio
    {
        long Add(IVADto entidad);

        void Update(IVADto entidad);

        void Delete(long id);

        IEnumerable<IVADto> Get(string cadenaBuscar);

        IVADto GetById(long id);
    }
}
