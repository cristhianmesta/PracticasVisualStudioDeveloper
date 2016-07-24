using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaN01
{
    abstract class Comprobante
    {
        protected int NumeroDocumento { get;  set; }
        protected DateTime FechaEmision { get; set; }
        protected string RazonSocial { get; set; }

        protected List<int> Unidades { get; set; }
        protected List<string> Descripcion { get; set; }
        protected List<double> Precios { get; set; }
        protected List<double> Importes { get; set; }

        public Comprobante()
        {
            Descripcion = new List<string>();
            Unidades = new List<int>();
            Precios = new List<double>();
            Importes = new List<double>();
        }

        public void AgregarDetalle(int tipo, int iunidades, string idescripcion, double imonto)
        {
            Descripcion.Add(idescripcion);
            Precios.Add(imonto);
            Unidades.Add(iunidades);
            switch (tipo)
            {
                case 1:
                    Importes.Add(iunidades * imonto);
                    break;
                case 2:
                    Importes.Add(iunidades * imonto);
                    break;
                case 3:
                    Importes.Add(imonto);
                    break;
            }
        }

        public void MonstrarDetalle(int tipo)
        {
            for (int i = 0; i < Descripcion.Count; i++)
            {
                switch (tipo) {
                    case 1:
                        Console.WriteLine($"{i + 1}. ({Unidades[i]} UND.) {Descripcion[i]} \t  S/. {Precios[i].ToString("F")} \t S/. {Importes[i].ToString("F")}");
                        break;
                    case 2:
                        Console.WriteLine($"{i + 1}. ({Unidades[i]} UND.) {Descripcion[i]} \t  S/. {Precios[i].ToString("F")} \t S/. {Importes[i].ToString("F")}");
                        break;
                    case 3:
                        Console.WriteLine($"{Descripcion[i]} \t S/. {Importes[i].ToString("F")}");
                        break;

                }
            } 
        }

        protected double SubTotal(int tipo) {
            double Suma = 0;
            for (int i = 0; i < Descripcion.Count; i++)
            {
                Suma = Suma + Importes[i];
            }
            return Suma;
        }

        public abstract double Impuesto();
        public abstract double Total();
        public abstract void Imprimir();

    }

    class Boleta : Comprobante
    {

        public Boleta(int numerodocumento, DateTime fechaemision, string razonsocial)
        {
            this.NumeroDocumento = numerodocumento;
            this.FechaEmision = fechaemision;
            this.RazonSocial = razonsocial;
            //this.Descripcion = descripcion;
            //this.Importes = importes;
        }

        public override double Impuesto()
        {
            return 0;
        }

        public override double Total()
        {
            return SubTotal(2) + Impuesto();
        }

        public override void Imprimir()
        {
            Console.WriteLine("\n");
            Console.WriteLine($"N° {NumeroDocumento}");
            Console.WriteLine($"Razón Social: {RazonSocial}");
            Console.WriteLine($"F. Emisión: {FechaEmision}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("=", 70)));
            MonstrarDetalle(2);
            Console.WriteLine(string.Concat(Enumerable.Repeat("=", 70)));
            Console.WriteLine($"TOTAL \t\t: S/. {Total().ToString("F")}");
        }
    }

    class Factura : Comprobante
    {
        private DateTime FechaVencimiento { get; set; }
        private string Ruc { get; set; }

        public Factura(int numerodocumento, DateTime fechaemision, DateTime fechavencimiento, string ruc, string razonsocial)
        {
            this.NumeroDocumento = numerodocumento;
            this.FechaEmision = fechaemision;
            this.FechaVencimiento = fechavencimiento;
            this.Ruc = ruc;
            this.RazonSocial = razonsocial;
            //this.Descripcion = descripcion;
            //this.Importes = importes;
        }

        public override double Impuesto()
        {
            return SubTotal(1) * 0.18;
        }

        public override double Total()
        {
            return SubTotal(1) + Impuesto();
        }

        public override void Imprimir()
        {
            Console.WriteLine("\n");
            Console.WriteLine($"N° {NumeroDocumento}");
            Console.WriteLine($"RUC: {Ruc}");
            Console.WriteLine($"Razón Social: {RazonSocial}");
            Console.WriteLine($"F. Emisión: {FechaEmision}");
            Console.WriteLine($"F. Vencimiento: {FechaVencimiento}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("=",70)));
            MonstrarDetalle(1);
            Console.WriteLine(string.Concat(Enumerable.Repeat("=", 70)));
            Console.WriteLine($"SUB TOTAL \t: S/. {SubTotal(2).ToString("F")}");
            Console.WriteLine($"IMPUESTO (18%) \t: S/. {Impuesto().ToString("F")}");
            Console.WriteLine($"TOTAL \t\t: S/. {Total().ToString("F")}");
        }
    }

    class Recibo : Comprobante
    {
        private string Ruc { get;  set; }

        public Recibo(int numerodocumento, DateTime fechaemision, string ruc, string razonsocial)
        {
            this.NumeroDocumento = numerodocumento;
            this.FechaEmision = fechaemision;
            this.Ruc = ruc;
            this.RazonSocial = razonsocial;
            //this.Descripcion = descripcion;
            //this.Importes = importes;
        }

        public override double Impuesto()
        {
            return SubTotal(3) * 0.10;
        }

        public override double Total()
        {
            return SubTotal(3) - Impuesto();
        }

        public override void Imprimir()
        {
            Console.WriteLine("\n");
            Console.WriteLine($"N° {NumeroDocumento}");
            Console.WriteLine($"RUC: {Ruc}");
            Console.WriteLine($"Razón Social: {RazonSocial}");
            Console.WriteLine($"F. Emisión: {FechaEmision}");
            Console.WriteLine(string.Concat(Enumerable.Repeat("=", 70)));
            MonstrarDetalle(3);
            Console.WriteLine(string.Concat(Enumerable.Repeat("=", 70)));
            Console.WriteLine($"SUB TOTAL \t: {SubTotal(3).ToString ("F")}");
            Console.WriteLine($"IMPUESTO (10%) \t: {Impuesto().ToString("F")}");
            Console.WriteLine($"TOTAL \t\t: S/. {Total().ToString("F")}");
        }
    }
}
