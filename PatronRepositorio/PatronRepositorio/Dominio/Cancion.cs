using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronRepositorio.Dominio
{
    class Cancion : Entidad<int>
    {
        public string Nombre { get; set; }
        public int AlbumId { get; set; }
        public int TipoDeMedioId { get; set; }
        public int GeneroId { get; set; }
        public string Comppositor { get; set; }
        public int Milisegundos { get; set; }
        public int Bytes { get; set; }
        public decimal PrecioUnitario { get; set; }

    }
}
