using PatronRepositorio.Dominio;
using PatronRepositorio.Implementaciones;
using PatronRepositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronRepositorio
{
    class Program
    {
        static void Main(string[] args)
        {
            //boostrap
            string cadenaConexion = @"Server=CRISTHIAN-NB\SQLEXPRESS; Database=Chinook;Trusted_Connection=True;";
            //var cadenaConexion = "Server=S222-VS-SERV;Database=Chinook;Trusted_Connection=True;";
            using (var bd = new SqlBasedeDatos(cadenaConexion))
            {
                /*
                Mostrardatos(bd);
                Console.WriteLine("Albums de AC/DC");
                MostrarAlbumsdeACDC(bd);
                Console.WriteLine("Albums de AC/DC usando repositorio");
                MostrarAlbumsdeACDC(bd, 1);
                             */
                Album album = new Album();
                album.Titulo = "Otro mas Nuevo album ACDC";
                album.ArtistaId = 1;
                InsertarAlbum(bd, album);

                album.Id = 348;
                album.Titulo = "jejejeje";
                album.ArtistaId = 1;
                ModificarAlbum(bd, album);

                EliminarAlbum(bd, 350);
       
                Console.Read();
            }

        }

        private static void InsertarAlbum(BasedeDatos bd, Album album)
        {
            var repositorio = new AlbumRepositorio(bd);
            repositorio.Agregar(album);
            Console.WriteLine($" Se insertó el album Titulo = {album.Titulo}, ID Artista = {album.ArtistaId}");
        }

        private static void ModificarAlbum(BasedeDatos bd, Album album)
        {
            var repositorio = new AlbumRepositorio(bd);
            repositorio.Actualizar(album);
            Console.WriteLine($" Se actualizó el album el album Titulo = {album.Titulo}, ID Artista = {album.ArtistaId}");
        }

        private static void EliminarAlbum(BasedeDatos bd, int id)
        {
            var repositorio = new AlbumRepositorio(bd);
            repositorio.Eliminar(id);
            Console.WriteLine($" Se elíminó el album {id}");
        }

        private static void MostrarAlbumsdeACDC(BasedeDatos bd, int id)
        {
            var repositorio = new AlbumRepositorio(bd);
            var album = repositorio.Traer(id);
            Console.WriteLine($"{album.Id} {album.Titulo} {album.ArtistaId}");
        }

        private static void Mostrardatos(BasedeDatos baseDatos)
        {
            using (IDataReader reader = baseDatos
                .EjecutarConsulta("Select top 10 * from Album", new Parametro[] { }))
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]}");
                }
                reader.Close();
            }
        }

        private static void MostrarAlbumsdeACDC(BasedeDatos baseDatos)
        {

            using (IDataReader reader = baseDatos
                .EjecutarConsulta("Select * from Album where ArtistId = @Artista", new Parametro[] { new Parametro { Nombre = "Artista", Valor = 1 }  }))
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]}");
                }
                reader.Close();
            }
        }
    }
}
