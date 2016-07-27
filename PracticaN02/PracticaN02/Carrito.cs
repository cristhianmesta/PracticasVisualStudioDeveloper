using System;
using System.Collections.Generic;
using System.Text;

namespace CarritoCompras
{
    class Carrito
    {
        private List<Entrada> entradas;
        public Carrito()
        {
            entradas = new List<Entrada>();
        }

        public void AgregarEntrada(IEntrada entrada, int cantidad, DateTime dia, DateTime hora, string pelicula)
        {
            entradas.Add(new Entrada(entrada, cantidad, dia, hora, pelicula));
        }

        public decimal TotalAPagar()
        {
            decimal total = 0;
            entradas.ForEach(entrada => total += entrada.Precio());
            return total;
        }

        public string Pagar(IMedioPago medioPago)
        {
            return medioPago.Pagar();
        }

        public string MostrarEntrada()
        {
            StringBuilder impresion = new StringBuilder();
            entradas.ForEach(entrada =>  impresion.AppendLine($"{entrada.Pelicula} x {entrada.Cantidad} entradas a las {entrada.Hora.ToString("hh:mm")}"));
            return impresion.ToString();
        }

        class Entrada
        {

            public int Cantidad { get; private set; }
            public int Dia { get; private set; }
            public IEntrada TipoEntrada { get; private set; }
            public string Pelicula { get; private set; }
            public DateTime Hora { get; private set; }

            public Entrada(IEntrada entrada, int cantidad, DateTime dia, DateTime hora, string pelicula)
            {
                this.TipoEntrada = entrada;
                this.Cantidad = cantidad;
                this.Dia = (int)dia.DayOfWeek;
                this.Hora = hora;
                this.Pelicula = pelicula;
            }

            public decimal Precio()
            {
                return TipoEntrada.Precio(Dia) * Cantidad;
            }

        }

    }

}