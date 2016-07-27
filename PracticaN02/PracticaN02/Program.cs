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

            carrito.AgregarEntrada(new EntradaNiño(), 2, DateTime.Today, new DateTime(1,1,1,16,0,0), "El padrino");
            carrito.AgregarEntrada(new EntradaAdulto(), 2, DateTime.Today, new DateTime(1, 1, 1, 16, 0, 0), "El padrino");
            carrito.AgregarEntrada(new EntradaAdultoMayor(), 1, DateTime.Today, new DateTime(1, 1, 1, 16, 0, 0), "El padrino");

            Console.WriteLine($"{carrito.MostrarEntrada()}");
            Console.WriteLine($"Total a Pagar: {carrito.TotalAPagar()}");
            Console.WriteLine($"{carrito.Pagar(new Visa())}");

            Console.ReadKey();
        }
    }
}
