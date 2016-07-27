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

        public void AgregarEntrada(IEntrada entrada, int cantidad)
        {
            entradas.Add(new Entrada(entrada, cantidad));
        }

        public decimal TotalAPagar()
        {
            decimal total = 0;
            entradas.ForEach(entrada => total += entrada.Precio());
            return total;

        }

        public void Pagar(IMedioPago medioPago)
        {
            //throw new NotImplementedException();
        }

        class Entrada
        {
            public Entrada(IEntrada entrada, int cantidad)
            {
                this.TipoEntrada = entrada;
                this.Cantidad = cantidad;
            }

            public int Cantidad { get; private set; }
            public IEntrada TipoEntrada { get; private set; }

            public decimal Precio()
            {
                return TipoEntrada.Precio() * Cantidad;
            }

        }
    }

}