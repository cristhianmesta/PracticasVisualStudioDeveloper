using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronRepositorio.Dominio
{
    public class Album : Entidad<int>
    {
        public string Titulo { get; set; }

        public int ArtistaId { get; set; }
    }

    public class AlbumString : Entidad<String>
    {
        public string Titulo { get; set; }

        public int ArtistaId { get; set; }

    }

}
