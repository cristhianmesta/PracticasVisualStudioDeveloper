using Orm.EF.DbFirstOpcion1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orm.EF
{
    class DbFirsOpcion1
    {
        static void Main(string[] args)
        {
            var bd = new ChinookOpcion1();

            var artista1 = bd.Artist.Single(a => a.ArtistId == 1);
            ImprimeArtista(artista1);
            var artista2 = bd.Artist.SingleOrDefault(a => a.ArtistId == 2646546);
            ImprimeArtista(artista2);

        }
        private static void ImprimeArtista(Artist artista)
        {
            Console.WriteLine($"{artista.Name} Nro de Albums: {artista.Album.Count()}");
            artista
                .Album
                .ToList()
                .ForEach(
                        a =>
                            Console.WriteLine($"{a.Title} canciones : {a.Track.Count()}")
                        );

        }
        private static void Albums()
        {
            //traer albums

            var albums = new ChinookOpcion1().Album.Where(a => a.ArtistId == 1).ToList();

            albums.ForEach(a => Console.WriteLine($"{a.Title} {a.Artist.Name}"));
        }
    }
}
