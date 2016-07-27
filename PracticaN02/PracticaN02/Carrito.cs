using System;
using System.Collections.Generic;

namespace CarritoCompras
{
    class Carrito
    {
        private List<Entrada> entradas;
        public Carrito()
        {
            entradas = new List<Entrada>();
        }

        public void AgregarEntrada(IEntrada entrada, int cantidad, DateTime dia)
        {
            entradas.Add(new Entrada(entrada, cantidad, dia));
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

        class Entrada
        {

            public int Cantidad { get; private set; }
            public int Dia { get; private set; }
            public IEntrada TipoEntrada { get; private set; }


            public Entrada(IEntrada entrada, int cantidad, DateTime dia)
            {
                this.TipoEntrada = entrada;
                this.Cantidad = cantidad;
                this.Dia = (int)dia.DayOfWeek;
            }

            public decimal Precio()
            {
                return TipoEntrada.Precio(Dia) * Cantidad;
            }

        }

    }

}