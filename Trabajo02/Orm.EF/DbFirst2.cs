using Orm.EF.DbFirstOpcion2;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Orm.EF
{
    public class DbFirst2
    {
        public static void Main(string[] args)
        {
            /// utilizacion de Contexto EF de manera Generica
            /// 
            Album album = Traer<Album>(x => x.AlbumId==1);
            Invoice invoice = Traer<Invoice>(x => x.InvoiceId == 1);

            Console.WriteLine($"El album es : {album.Title}");
        }

        
        static T Traer<T>(Expression<Func<T,bool>> predicado) where T : class
        {
            using (var bd = new ChinookOpcion2())
            {
                return bd.Set<T>().Single(predicado);
            }
        }
        private static void EjemploActualizacionMaestroDetalle()
        {
            Album albumAModificar;
            //ui
            albumAModificar = AlbumModificado();
            //backend
            ActualizarAlbum(albumAModificar);
        }

        private static Album AlbumModificado()
        {
            Album albumAModificar;
            using (var bd1 = new ChinookOpcion2())
            {
                albumAModificar = bd1.Album.Include("Track").Single(a => a.AlbumId == 349);
            }
            albumAModificar.Title = "Esto es una Prueba 3";

            Track trackAEliminar = albumAModificar.Track.Single(x => x.TrackId == 3509);
            albumAModificar
                .Track.Remove(trackAEliminar);

            albumAModificar
                .Track
                .Add(new Track()
                {
                    AlbumId = albumAModificar.AlbumId,
                    Name = "Nueva cancion 4",
                    MediaTypeId = 1,
                    GenreId = 1
                });

            albumAModificar.Track.ToList().ForEach(x => { x.Name = $"{x.Name} Modificado"; x.Composer = "Anonimo"; });
            return albumAModificar;
        }

        private static void ActualizarAlbum(Album albumModificado)
        {
            using (var bd2 = new ChinookOpcion2())
            {
                //debo obtener primero el album que esta en la BD
                // consulta a la entidad album y buca una ocurrencia identificada con el valor de albumModificado.AlbumId
                // Select * from Album Where AlbumId = 349 => albumModificado.AlbumId
                Album original = bd2.Album.Include("Track").Single(a => a.AlbumId == albumModificado.AlbumId);

                // actualizado las propiedades no Virtuales de album original
                bd2.Entry(original).CurrentValues.SetValues(albumModificado); // actualizando album
                /*
                SetValues:
                Esta funcion realiza la asignacion entre las propiedades de AlbumModificado y original
                siempre cuando la Nombre y El tipo de dato de la Propiedad sean iguales,
                Funciona solo con Tipo Nativos
                */

                //eliminando los tracks que fueron eliminado en albumModificado
                foreach (Track trackAEliminar in original.Track.ToList())
                {
                    if (!albumModificado.Track.Any(m => m.TrackId == trackAEliminar.TrackId))
                        bd2.Track.Remove(trackAEliminar);
                }

                //añadiendo los tracks que fueron añadidos a albumModificado
                foreach (Track trackModificado in albumModificado.Track)
                {
                    Track trackOriginal = original.Track.SingleOrDefault(x => x.TrackId == trackModificado.TrackId);

                    if (trackOriginal==null)
                    {
                        // track nuevos
                        Track nuevo = new Track()
                        {
                            AlbumId = trackModificado.AlbumId,
                            Name = trackModificado.Name,
                            MediaTypeId = trackModificado.MediaTypeId,
                            GenreId = trackModificado.GenreId
                        };
                        original.Track.Add(nuevo);
                    }
                    else {
                        //tracks modificados
                        //actualizo las propiedades del track modificado
                        bd2.Entry(trackOriginal).CurrentValues.SetValues(trackModificado);
                    }
                }
                // guardando los cambios en la BD apuntada por el DBContext bd2
                bd2.SaveChanges();
            }
        }

        void ActualizaHIjos<TPadre, THijo>(TPadre original, TPadre modificado)
        {


        }
        private static void ActualizaUnaEntidad()
        {
            Album albumAModificar;
            //ui
            using (var bd1 = new ChinookOpcion2())
            {
                albumAModificar = bd1.Album.Single(a => a.AlbumId == 349);
            }
            //la ui lo modifica
            albumAModificar.Title = "Album Modificado 3";

            // para grabar tengo que abrir otra BD => Contexto de EF
            using (var bd2 = new ChinookOpcion2())
            {
                //bd2.Album.Attach(albumAModificar);
                //bd2.Album.Remove(albumAModificar);

                bd2.Entry<Album>(albumAModificar).State = EntityState.Modified;
                bd2.SaveChanges();
            }
        }

        private static void CreacionEjemplo()
        {
            var bd = new ChinookOpcion2();
            //var artista = bd.Artist.Single(a => a.ArtistId == 1);
            var album = new Album()
            {
                ArtistId = 1,
                Title = "Highway to Hell"
            };

            album.Track.Add(new Track() {
                Album = album,
                Name = "Highway To Hell",
                MediaTypeId = 1,
                GenreId = 1 });

            bd.Album.Add(album); // similar a hacer un insert en la tabla Album
            album.Track.Add(new Track() { Album = album, Name = "High Voltage", MediaTypeId = 1, GenreId = 1 });
            bd.SaveChanges();
        }

        private static void CreacionEjemplo2()
        {
            // CREANDO UN HIJO Y SU PADRE
            var bd = new ChinookOpcion2();

            InvoiceLine invoiceLine = new InvoiceLine()
            {
                Invoice = new Invoice()
                {
                    CustomerId = 1,
                    InvoiceDate = DateTime.Today,
                    BillingAddress = "Jesus Maria",
                    BillingCity = "Lima",
                    BillingCountry = "Peru",
                    BillingPostalCode = "0051",
                    BillingState = "KA",
                    Total = 20,
                },
                TrackId = 1,
                UnitPrice = 10.0m,
                Quantity = 2
            };
            bd.InvoiceLine.Add(invoiceLine);
            bd.SaveChanges();
        }

        private static void CreacionEjemplo3()
        {
            var bd = new ChinookOpcion2();
            var factura = new Invoice()
            {
                CustomerId = 1,
                InvoiceDate = DateTime.Today.AddDays(1),
                BillingAddress = "Jesus Maria",
                BillingCity = "Lima",
                BillingCountry = "Peru",
                BillingPostalCode = "0051",
                BillingState = "KA",
                Total = 20,
            };

            InvoiceLine invoiceLine = new InvoiceLine()
            {
                Invoice = factura,
                TrackId = 1,
                UnitPrice = 10.0m,
                Quantity = 2
            };

            factura.InvoiceLine.Add(invoiceLine);

            bd.InvoiceLine.Add(invoiceLine);

            bd.SaveChanges();
        }


    }
}
