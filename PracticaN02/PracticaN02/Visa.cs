﻿using System;

namespace CarritoCompras
{
    class Visa : IMedioPago
    {
        public string Pagar()
        {
            return "Pagado con VISA";
        }
    }
}