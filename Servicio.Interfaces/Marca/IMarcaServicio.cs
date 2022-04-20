using Servicio.Interfaces.Marca.DTOs;
using System.Collections.Generic;

namespace Servicio.Interfaces.Marca
{
    public interface IMarcaServicio
    {
        long Add(MarcaDtos entidad);

        void Update(MarcaDtos entidad);

        void Delete(long id);

        IEnumerable<MarcaDtos> Get(string cadenaBuscar);

        MarcaDtos GetById(long id);
    }
}
