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
    public class AlbumRepositorio : IRepositorio<Album, int>
    {
        private BasedeDatos basedeDatos;

        public AlbumRepositorio(BasedeDatos basedeDatos)
        {
            this.basedeDatos = basedeDatos;
        }
        public void Actualizar(Album entidad)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro { Nombre = "Id", Valor= entidad.Id },
                new Parametro { Nombre = "Titulo", Valor= entidad.Titulo },
                new Parametro { Nombre = "Artista", Valor= entidad.ArtistaId}
            };

            basedeDatos
                .EjecutarComando("update dbo.Album Set Title = @Titulo, ArtistId = @Artista Where AlbumId = @Id", parametros.ToArray());

        }

        public void Agregar(Album entidad)
        {
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro { Nombre = "Titulo", Valor= entidad.Titulo },
                new Parametro { Nombre = "Artista", Valor= entidad.ArtistaId}
            };

            basedeDatos
                .EjecutarComando("insert into dbo.Album (Title, ArtistId) Values(@Titulo, @Artista)", parametros.ToArray());
        }

        public void Eliminar(int id)
        {
            basedeDatos
                .EjecutarComando("delete from dbo.Album where AlbumId =@Id", new Parametro[] { new Parametro { Nombre="Id", Valor=id } });

        }
        public Album IdAgregado(Album entidad)
        {
            entidad.Id = Convert.ToInt32(basedeDatos.EjecutarEscalar("SELECT IDENT_CURRENT ('Album')", new Parametro[] { }));
            return entidad;
        }


        public Album Traer(int id)
        {
            using (IDataReader reader = basedeDatos.EjecutarConsulta("Select * from Album where AlbumId = @Id", new Parametro[] { new Parametro { Nombre = "Id", Valor = id } }))
            {
                while (reader.Read())
                {
                    return new Album
                    {
                        Id = Convert.ToInt32(reader["AlbumId"]),
                        Titulo = reader["Title"].ToString(),
                        ArtistaId = Convert.ToInt32(reader["ArtistId"])
                    };
                }

                return null;
            }
        }

        public IEnumerable<Album> Traer()
        {
            using (IDataReader reader = basedeDatos.EjecutarConsulta("Select * from Album", new Parametro[] { }))
            {
                while (reader.Read())
                {
                    yield return new Album
                    {
                        Id = Convert.ToInt32(reader["AlbumId"]),
                        Titulo = reader["Title"].ToString(),
                        ArtistaId = Convert.ToInt32(reader["ArtistId"])
                    };
                }
            }
        }
    }

}
