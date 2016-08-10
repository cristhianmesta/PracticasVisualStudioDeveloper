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
                //Creacion de Album y artista
                Creaciones();

                //Consultas Linq
                OperacionesLinq();
                Console.Read();

        }

        static void Creaciones()
        {
            string cadenaConexion = @"Server=CRISTHIAN-NB\SQLEXPRESS; Database=Chinook;Trusted_Connection=True;";
            //var cadenaConexion = "Server=S222-VS-SERV;Database=Chinook;Trusted_Connection=True;";
            using (var bd = new SqlBasedeDatos(cadenaConexion))
            {
                TiendaMusica tiendamusica = new TiendaMusica(bd);

                //Crear artista
                Artista artista = new Artista();
                artista.Nombre = "Nuevo artista";
                tiendamusica.CrearArtista(artista);
                Console.WriteLine($"Creando artista... {artista.Nombre}  ID: {artista.Id}  \n");

                //Crear Album
                Album album = new Album();
                album.Titulo = "Nuevo album";
                album.ArtistaId = artista.Id;

                List<Cancion> canciones = new List<Cancion>();
                canciones.Add(
                    new Cancion
                    {
                        Nombre = "Nueva cancion",
                        AlbumId = album.Id,
                        TipoDeMedioId = 1,
                        GeneroId = 1,
                        Comppositor = "Nuevo Compositor",
                        Milisegundos = 50000,
                        Bytes = 1024054,
                        PrecioUnitario = 0.99m,
                    });

                tiendamusica.CrearAlbum(album, canciones);
            }
        }

        static void OperacionesLinq()
        {
            string cadenaConexion = @"Server=CRISTHIAN-NB\SQLEXPRESS; Database=Chinook;Trusted_Connection=True;";
            //var cadenaConexion = "Server=S222-VS-SERV;Database=Chinook;Trusted_Connection=True;";
            using (var bd = new SqlBasedeDatos(cadenaConexion))
            {

                var tiendamuscia = new TiendaMusica(bd);
                Console.WriteLine("\n****** Consulta LINQ 1: Lista de Artistas por Letra***** \n");
                tiendamuscia.ListarArtistasPorLetra("A").ForEach(Lista => Console.WriteLine($"{Lista.Nombre} | {Lista.Id}"));

                Console.WriteLine("\n****** Consulta LINQ 2.1: Lista de Albunes/Canciones por Artista\n");
                tiendamuscia.ListaPorArtista(8);

                Console.WriteLine("\n****** Consulta LINQ 2.2: Lista de Canciones por Letra\n");
                tiendamuscia.ListaDeCanciones("Q");

                Console.WriteLine("\n****** Consulta LINQ 3:  Las N Canciones mas largas\n");
                tiendamuscia.ListarCancionesMaLargas(10).ForEach(Lista => Console.WriteLine($"{Lista.Nombre} (Duración: {Lista.Milisegundos/60000} Min) "));

      
            }
        }

        #region Pruebas

        static void Pruebas()
        {
            //boostrap
            string cadenaConexion = @"Server=CRISTHIAN-NB\SQLEXPRESS; Database=Chinook;Trusted_Connection=True;";
            //var cadenaConexion = "Server=S222-VS-SERV;Database=Chinook;Trusted_Connection=True;";
            using (var bd = new SqlBasedeDatos(cadenaConexion))
            {

                Mostrardatos(bd);
                Console.WriteLine("Albums de AC/DC");
                MostrarAlbumsdeACDC(bd);
                Console.WriteLine("Albums de AC/DC usando repositorio");
                MostrarAlbumsdeACDC(bd, 1);

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
        #endregion


        #region Album

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

        #endregion

        #region Básico

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
                .EjecutarConsulta("Select * from Album where ArtistId = @Artista", new Parametro[] { new Parametro { Nombre = "Artista", Valor = 1 } }))
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0]} {reader[1]} {reader[2]}");
                }
                reader.Close();
            }
        }

        #endregion
    }
}
