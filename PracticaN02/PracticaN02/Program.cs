using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarritoCompras
{
    class Program
    {
        static void Main(string[] args)
        {
            Carrito carrito = new Carrito();

            carrito.AgregarEntrada(new EntradaNiño(), 2);
            carrito.AgregarEntrada(new EntradaAdulto(), 2);
            carrito.AgregarEntrada(new EntradaAdultoMayor(), 1);

            Console.WriteLine($"Total a Pagar: {carrito.TotalAPagar()}");
            IMedioPago medioPago = new Visa();
            carrito.Pagar(medioPago);

            Console.ReadKey();
        }
    }
}
