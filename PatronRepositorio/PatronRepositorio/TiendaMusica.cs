using PatronRepositorio.Dominio;
using PatronRepositorio.Implementaciones;
using PatronRepositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronRepositorio
{
    class TiendaMusica
    {
        public BasedeDatos bd; 
        public TiendaMusica(BasedeDatos basededatos)
        {
            this.bd = basededatos;
        }
 
        public Artista CrearArtista(Artista artista)
        {
            var artistarepositorio = new ArtistaRepositorio(bd);
            artistarepositorio.Agregar(artista);
            return artistarepositorio.IdAgregado(artista);  
        }

        public void CrearAlbum(Album album, List<Cancion> cancion)
        {
            var albumrepositorio = new AlbumRepositorio(bd);
            albumrepositorio.Agregar(album);
            album.Id = albumrepositorio.IdAgregado(album).Id;

            cancion.ForEach(c => c.AlbumId = album.Id);
            cancion.ForEach(c => CrearCancion(c));

            Console.WriteLine($"Creando album... {album.Titulo}  ID: {album.Id} ");
            cancion.ForEach(c => Console.WriteLine($"Creando canciones...  {c.Nombre} \n"));
        }

        public void CrearCancion(Cancion cancion)
        { 
            var cancionrepositorio = new CancionRepositorio(bd);
            cancionrepositorio.Agregar(cancion);
        }

        public List<Artista> ListarArtistasPorLetra(string letra)
        {
            IEnumerable<Artista> artista = new ArtistaRepositorio(bd).Traer();
            return artista.Where(a => a.Nombre.Substring(0,1).ToLower().Trim() == letra.ToLower().Trim()).ToList();
        }

        public List<string> ListarAlbumnesTotalCancion(int ArtistId)
        {
            List<string> Lista = new List<string>();
            IEnumerable<Album> album = new AlbumRepositorio(bd).Traer();
            IEnumerable<Cancion> cancion = new CancionRepositorio(bd).Traer();


            foreach (var a in album.Take(10).ToList())
            {
                Lista.Add($"{a.Titulo} \t Canciones: {TotalCanciones(a.Id)}");
            }
            return Lista;
        }

        public List<Cancion> ListarCancionesMaLargas(int top)
        {
            IEnumerable<Cancion> cancion = new CancionRepositorio(bd).Traer();

            return cancion.OrderByDescending(c=>c.Milisegundos).Take(top).ToList();
        }

        public int TotalCanciones(int AlbumId)
        {
            IEnumerable<Cancion> cancion = new CancionRepositorio(bd).Traer();

            return cancion.Where(c => c.AlbumId==AlbumId ).ToList().Count();
        }


        public void ListaPorArtista(int Id)
        {
            IEnumerable<Artista> artista = new ArtistaRepositorio(bd).Traer();
            IEnumerable<Album> album = new AlbumRepositorio(bd).Traer();
            IEnumerable<Cancion> cancion = new CancionRepositorio(bd).Traer();

            album
               .Join(artista, a => a.ArtistaId, b => b.Id, (a, b) => new { ArtistaId = b.Id, ArtistaNombre = b.Nombre, AlbumId = a.Id, AlbumNombre = a.Titulo })
               .Join(cancion, a => a.AlbumId, b => b.AlbumId, (a, b) => new
               {
                   IdArtista = a.ArtistaId,
                   NombreArtista = a.ArtistaNombre,
                   IdAlbum = a.AlbumId,
                   NombreAlbum = a.AlbumNombre,
                   IdCancion = b.Id,
                   NombreCancion = b.Nombre,
                   Duracion = b.Milisegundos / 1000,
                   Tamaño = b.Bytes / 1000000
               })
                .Where(a => a.IdArtista == Id)
                .OrderBy(a => a.NombreAlbum)
                .ToList()
                .ForEach(a => Console.WriteLine($"{a.NombreArtista} \t {a.NombreAlbum} \t {a.NombreCancion} | {a.Duracion} Seg "));
        }

        public void ListaDeCanciones(string letra)
        {
            IEnumerable<Artista> artista = new ArtistaRepositorio(bd).Traer();
            IEnumerable<Album> album = new AlbumRepositorio(bd).Traer();
            IEnumerable<Cancion> cancion = new CancionRepositorio(bd).Traer();

            album
               .Join(artista, a => a.ArtistaId, b => b.Id, (a, b) => new { ArtistaId = b.Id, ArtistaNombre = b.Nombre, AlbumId = a.Id, AlbumNombre = a.Titulo })
               .Join(cancion, a => a.AlbumId, b => b.AlbumId, (a, b) => new
               {
                   IdArtista = a.ArtistaId,
                   NombreArtista = a.ArtistaNombre,
                   IdAlbum = a.AlbumId,
                   NombreAlbum = a.AlbumNombre,
                   IdCancion = b.Id,
                   NombreCancion = b.Nombre,
                   Duracion = b.Milisegundos / 1000,
                   Tamaño = b.Bytes / 1000000
               })
                .Where(a => a.NombreCancion.ToLower().Substring(0, 1) == letra.ToLower())
                .OrderBy(a => a.NombreCancion)
                .ToList()
                .ForEach(a => Console.WriteLine($"{a.NombreCancion} | {a.Tamaño} MB | {a.Duracion} Seg \t \t {a.NombreArtista} - {a.NombreAlbum}"));
        }


    }


}
