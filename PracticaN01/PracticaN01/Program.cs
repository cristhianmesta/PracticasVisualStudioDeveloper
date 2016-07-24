using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaN01
{
    class Program
    {
        static void Main(string[] args)
        {
            int otrocomprobante=0;
            do
            {

                Console.Write("Tipo de documento: " + "\n" + "[1] Factura" + "\n" + "[2] Boleta" + "\n" + "[3] Recibo" + "\n" + "Ingresar opción: ");
                var tipo = Console.ReadLine();
                switch (tipo.ToString().Trim())
                {
                    case "1":
                        IngresarFactura();
                        break;
                    case "2":
                        IngresarBoleta();
                        break;
                    case "3":
                        IngresarRecibo();
                        break;
                }
                Console.Write("\n");
                Console.WriteLine("¿Desea ingresar otro comprobante? [1] Sí [2]No ");
                Console.Write("Ingresar opción:");
                otrocomprobante = int.Parse(Console.ReadLine());
            } while (otrocomprobante == 1);
            Console.ReadKey();
        }

        static void IngresarFactura()
        {
            int numerodocumento;
            DateTime fechaemision;
            DateTime fechavencimiento;
            string ruc;
            string razonsocial;

            Console.Write("\n");
            Console.Write("Ingrese el número de la factura: ");
            numerodocumento = int.Parse (Console.ReadLine());

            Console.Write("Ingrese la fecha de emisión: ");
            fechaemision = DateTime.Parse(Console.ReadLine());

            Console.Write("Ingrese la fecha de vencimiento: ");
            fechavencimiento  = DateTime.Parse(Console.ReadLine());

            Console.Write("Ingrese el número de ruc: ");
            ruc  = Console.ReadLine().Trim();

            Console.Write("Ingrese la razón social: ");
            razonsocial  = Console.ReadLine().Trim();

            Factura oFactura = new Factura(numerodocumento,fechaemision, fechavencimiento, ruc, razonsocial);
            Comprobante oComprobante = oFactura as Factura;
            AgregarDetalle(1, oComprobante);

            oFactura.Imprimir();
        }

        static void IngresarBoleta()
        {
            int numerodocumento;
            DateTime fechaemision;
            string razonsocial;

            Console.Write("\n");
            Console.Write("Ingrese el número de la Boleta: ");
            numerodocumento = int.Parse(Console.ReadLine());

            Console.Write("Ingrese la fecha de emisión: ");
            fechaemision = DateTime.Parse(Console.ReadLine());

            Console.Write("Ingrese la razón social: ");
            razonsocial = Console.ReadLine().Trim();

            Boleta oBoleta = new Boleta (numerodocumento, fechaemision, razonsocial);
            Comprobante oComprobante = oBoleta as Boleta ;
            AgregarDetalle(2, oComprobante);

            oBoleta.Imprimir();
        }

        static void IngresarRecibo()
        {
            int numerodocumento;
            DateTime fechaemision;
            string ruc;
            string razonsocial;

            Console.Write("\n");
            Console.Write("Ingrese el número del recibo: ");
            numerodocumento = int.Parse(Console.ReadLine());

            Console.Write("Ingrese la fecha de emisión: ");
            fechaemision = DateTime.Parse(Console.ReadLine());

            Console.Write("Ingrese el número de ruc: ");
            ruc = Console.ReadLine().Trim();

            Console.Write("Ingrese la razón social: ");
            razonsocial = Console.ReadLine().Trim();

            Recibo oRecibo = new Recibo (numerodocumento, fechaemision, ruc, razonsocial);
            Comprobante oComprobante = oRecibo as Recibo;
            AgregarDetalle(3, oComprobante);

            oRecibo.Imprimir();
        }

        static void AgregarDetalle(int tipo, Comprobante objeto) {
            int otrodetalle=0;
            do
            {
                Console.Write("\n");
                Console.WriteLine("Agregar item:");

                Console.Write("Descripción: ");
                string descripcion = Console.ReadLine().Trim();

                int unidades = 1;
                if (tipo != 3)
                {
                    Console.Write("Cantidad: ");
                    unidades  = int.Parse(Console.ReadLine().Trim());
                }

                Console.Write("Precio: ");
                double monto = double.Parse(Console.ReadLine().Trim());

                objeto.AgregarDetalle(tipo, unidades, descripcion, monto);

                if (tipo < 3) {
                    Console.WriteLine("¿Desea ingresar otro detalle? [1] Sí [2]No");
                    Console.Write("Ingresar opción:");
                    otrodetalle = int.Parse(Console.ReadLine());
                }
            } while (otrodetalle == 1);
        }

    }
}
