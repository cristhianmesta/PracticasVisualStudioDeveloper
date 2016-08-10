using PatronRepositorio.Dominio;
using PatronRepositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace PatronRepositorio.Implementaciones
{
    class ArtistaRepositorio : IRepositorio<Artista, int>
    {
        private BasedeDatos basedeDatos;

        public ArtistaRepositorio(BasedeDatos basedeDatos)
        {
            this.basedeDatos = basedeDatos;
        }

        public void Agregar(Artista entidad)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro { Nombre = "Name", Valor= entidad.Nombre },
            };

            basedeDatos.EjecutarComando("INSERT INTO dbo.Artist (Name) VALUES (@Name)", parametros.ToArray());
        }

        public void Actualizar(Artista entidad)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro { Nombre = "Id", Valor= entidad.Id },
                new Parametro { Nombre = "Nombre", Valor= entidad.Nombre },
            };

            basedeDatos.EjecutarComando("UPDATE dbo.Artist SET Name=@Nombre WHERE ArtistId=@Id", parametros.ToArray());
        }


        public void Eliminar(int id)
        {
            basedeDatos.EjecutarComando("DELETE FROM dbo.Artist WHERE ArtistId=@Id", new Parametro[] { new Parametro { Nombre = "Id", Valor = id } });
        }

        public Artista IdAgregado(Artista entidad)
        {
            entidad.Id = Convert.ToInt32(basedeDatos.EjecutarEscalar("SELECT IDENT_CURRENT ('Artist')", new Parametro[] { }));
            return entidad;
        }

        public Artista AgregarYObtenerID(Artista entidad)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro { Nombre = "Name", Valor= entidad.Nombre },
            };

            entidad.Id = Convert.ToInt32( basedeDatos.EjecutarComandoyEscalar("INSERT INTO dbo.Artist (Name) VALUES (@Name)", "SELECT SCOPE_IDENTITY() ", parametros.ToArray()));
            return entidad;
        }

        public Artista Traer(int id)
        {
            using(IDataReader reader = basedeDatos.EjecutarConsulta("SELECT ArtistId, Name FROM Artist", new Parametro[] { new Parametro { Nombre = "Id", Valor = id } } ) )
            {
                while (reader.Read())
                {
                    return new Artista { Id = Convert.ToInt32(reader["ArtistId"]), Nombre = reader["Name"].ToString() };
                }
                return null;
            }
        }

        public IEnumerable<Artista> Traer()
        {
            using (IDataReader reader = basedeDatos.EjecutarConsulta("SELECT ArtistId, Name FROM Artist", new Parametro[] {  }))
            {
                while (reader.Read())
                {
                    yield return new Artista { Id = Convert.ToInt32(reader["ArtistId"]), Nombre = reader["Name"].ToString() };
                }
            }
        }


    }
}
