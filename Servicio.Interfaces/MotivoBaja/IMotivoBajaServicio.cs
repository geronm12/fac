namespace Servicio.Interfaces.MotivoBaja
{
    using Servicio.Interfaces.MotivoBaja.DTOs;
    using System.Collections.Generic;
    public interface IMotivoBajaServicio
    {
        long Add(MotivoBajaDto entidad);

        void Delete(long id);

        void Update(MotivoBajaDto entidad);

        IEnumerable<MotivoBajaDto> Get(string cadenaBuscar);

        MotivoBajaDto GetById(long id);
    }
}
