using System;

namespace CarritoCompras
{
    internal class EntradaAdulto : IEntrada
    {
        public decimal Precio()
        {
            return 12.00m;
        }
    }
}