using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronRepositorio.Dominio
{
    public abstract class Entidad<TKey>
    {
        public TKey Id { get; set; }
    }
}
