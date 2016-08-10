using PatronRepositorio.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronRepositorio.Interfaces
{
    interface IRepositorio<TEntidad, TKey>  where TEntidad : Entidad<TKey>
    {
        void Agregar(TEntidad entidad);

        void Actualizar(TEntidad entidad);

        void Eliminar(TKey id);
    
        TEntidad IdAgregado(TEntidad entidad);

        TEntidad Traer(TKey id);
 
        IEnumerable<TEntidad> Traer();

    }
}
