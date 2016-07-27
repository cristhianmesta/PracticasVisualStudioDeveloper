namespace CarritoCompras
{
    class PayPal : IMedioPago 
    {
        public string Pagar()
        {
            return "Pagado por medio de PayPal";
        }
    }
}
