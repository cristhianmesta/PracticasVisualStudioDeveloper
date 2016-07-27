namespace CarritoCompras
{
    class DepositoCuenta : IMedioPago
    {
        public string Pagar()
        {
            return "Depositó previamente a cuenta";
        }
    }
}