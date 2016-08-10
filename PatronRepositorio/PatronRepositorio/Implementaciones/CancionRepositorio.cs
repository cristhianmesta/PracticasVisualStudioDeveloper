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
    class CancionRepositorio : IRepositorio<Cancion, int>
    {
        private BasedeDatos basedeDatos;
        public CancionRepositorio(BasedeDatos basedeDatos)
        {
            this.basedeDatos = basedeDatos;
        }
        public void Agregar(Cancion entidad)
        {
            string sql = "INSERT INTO dbo.Track(Name, AlbumId, MediaTypeId, GenreId, Composer, Milliseconds, Bytes, UnitPrice) VALUES(@Name, @AlbumId, @MediaTypeId, @GenreId, @Composer, @Milliseconds, @Bytes, @UnitPrice)";
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro { Nombre = "Name", Valor = entidad.Nombre  },
                new Parametro { Nombre = "AlbumId", Valor = entidad.AlbumId  },
                new Parametro { Nombre = "MediaTypeId", Valor = entidad.TipoDeMedioId  },
                new Parametro { Nombre = "GenreId", Valor = entidad.GeneroId  },
                new Parametro { Nombre = "Composer", Valor = entidad.Comppositor  },
                new Parametro { Nombre = "Milliseconds", Valor = entidad.Milisegundos  },
                new Parametro { Nombre = "Bytes", Valor = entidad.Bytes  },
                new Parametro { Nombre = "UnitPrice", Valor = entidad.PrecioUnitario  }
            };
            basedeDatos.EjecutarComando(sql, parametros.ToArray());

        }
        public void Actualizar(Cancion entidad)
        {
            string sql = "UPDATE dbo.Track SET Name = @Name, AlbumId = @AlbumId, MediaTypeId = @MediaTypeId, GenreId = @GenreId, Composer = @Composer, Milliseconds = @Milliseconds, Bytes = @Bytes, UnitPrice = @UnitPrice WHERE TrackId=@Id";
            List<Parametro> parametros = new List<Parametro>
            {
                new Parametro { Nombre = "Id", Valor = entidad.Id  },
                new Parametro { Nombre = "Name", Valor = entidad.Nombre  },
                new Parametro { Nombre = "AlbumId", Valor = entidad.AlbumId  },
                new Parametro { Nombre = "MediaTypeId", Valor = entidad.TipoDeMedioId  },
                new Parametro { Nombre = "GenreId", Valor = entidad.GeneroId  },
                new Parametro { Nombre = "Composer", Valor = entidad.Comppositor  },
                new Parametro { Nombre = "Milliseconds", Valor = entidad.Milisegundos  },
                new Parametro { Nombre = "Bytes", Valor = entidad.Bytes  },
                new Parametro { Nombre = "UnitPrice", Valor = entidad.PrecioUnitario  }
            };
            basedeDatos.EjecutarComando(sql, parametros.ToArray());
        }

        public void Eliminar(int id)
        {
            basedeDatos.EjecutarComando("DELETE FROM Track WHERE TrackId=@Id", new Parametro[]{ new Parametro { Nombre = "Id", Valor=id } });
        }

        public Cancion IdAgregado(Cancion entidad)
        {
            entidad.Id = Convert.ToInt32(basedeDatos.EjecutarEscalar("SELECT SCOPE_IDENTITY()", new Parametro[] { }));
            return entidad;
        }

        public Cancion Traer(int id)
        {
            using (IDataReader reader = basedeDatos.EjecutarConsulta("SELECT * FROM Track TrackId=@Id", new Parametro[] { new Parametro { Nombre = "Id", Valor = id } }))
            {
                while (reader.Read()) {
                    return new Cancion
                    {
                        Id = Convert.ToInt32(reader["TrackId"]),
                        Nombre = reader["TrackId"].ToString(),
                        AlbumId = Convert.ToInt32(reader["AlbumId"]),
                        TipoDeMedioId = Convert.ToInt32(reader["MediaTypeId"]),
                        GeneroId = Convert.ToInt32(reader["GenreId"]),
                        Comppositor = reader["Composer"].ToString(),
                        Milisegundos = Convert.ToInt32(reader["Milliseconds"]),
                        Bytes = Convert.ToInt32(reader["TrackId"]),
                        PrecioUnitario = Convert.ToInt32(reader["TrackId"]),
                    };
                }
                return null;
            }
        }

        public IEnumerable<Cancion> Traer()
        {
            using (IDataReader reader = basedeDatos.EjecutarConsulta("SELECT * FROM Track", new Parametro[] {  }))
            {
                while (reader.Read())
                {
                    yield return new Cancion
                    {
                        Id = Convert.ToInt32(reader["TrackId"]),
                        Nombre = reader["Name"].ToString(),
                        AlbumId = Convert.ToInt32(reader["AlbumId"]),
                        TipoDeMedioId = Convert.ToInt32(reader["MediaTypeId"]),
                        GeneroId = Convert.ToInt32(reader["GenreId"]),
                        Comppositor = reader["Composer"].ToString(),
                        Milisegundos = Convert.ToInt32(reader["Milliseconds"]),
                        Bytes = Convert.ToInt32(reader["Bytes"]),
                        PrecioUnitario = Convert.ToDecimal(reader["UnitPrice"]),
                    };
                }
            }
        }

 
    }
}
