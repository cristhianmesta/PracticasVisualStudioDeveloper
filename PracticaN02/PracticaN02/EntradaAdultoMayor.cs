using System;

namespace CarritoCompras
{
    internal class EntradaAdultoMayor : IEntrada
    {
        public decimal Precio()
        {
            return 10.00m;
        }
    }
}