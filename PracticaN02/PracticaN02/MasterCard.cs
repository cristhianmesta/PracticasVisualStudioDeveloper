using System;

namespace CarritoCompras
{
    class MasterCard : IMedioPago
    {
        public string Pagar()
        {
            return "Pagado con MasterCard";
        }
    }
}