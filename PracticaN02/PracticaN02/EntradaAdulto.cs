using System;

namespace CarritoCompras
{
    internal class EntradaAdulto : IEntrada
    {
        public decimal Precio(int dia)
        {
            decimal precio=0;
            if (dia==1 || dia==3 || dia==4 || dia==5) precio = 18.00m;
            if (dia==2) precio = 12.00m;
            if (dia==6 || dia==7) precio = 20.00m;
            return precio;
        }
    }
}