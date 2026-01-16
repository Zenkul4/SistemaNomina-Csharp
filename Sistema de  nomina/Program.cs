using System;
using System.Collections.Generic;
using System.Linq;

namespace SistemaNomina
{
    class Program
    {

        static List<Empleado> listaEmpleados = new List<Empleado>();
        static HashSet<string> nssRegistrados = new HashSet<string>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("    SISTEMA DE GESTIÓN DE NÓMINA");
                Console.WriteLine("=========================================");
                Console.WriteLine("1. Registrar Nuevo Empleado");
                Console.WriteLine("2. Importar Datos (Generar 1k Usuarios)");
                Console.WriteLine("3. Ver Lista de Empleados (Vista Detallada)");
                Console.WriteLine("4. Buscar y Gestionar Empleado");
                Console.WriteLine("5. Generar Reporte Detallado");
                Console.WriteLine("6. Salir");
                Console.Write("\nSeleccione una opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": RegistrarEmpleadoUI(); break;
                    case "2": ImportarDatosReales(); break;
                    case "3": VerEmpleadosUI(); break;
                    case "4": BuscarYGestionarUI(); break;
                    case "5": GenerarReporteDetallado(); break;
                    case "6": return;
                }
            }
        }

        static void AgregarEmpleado(Empleado emp)
        {
            if (nssRegistrados.Contains(emp.NumeroSeguroSocial)) return;

            listaEmpleados.Add(emp);
            nssRegistrados.Add(emp.NumeroSeguroSocial);
        }

        static void ImportarDatosReales()
        {
            Console.WriteLine("\nGenerando 1,000 registros únicos...");

          
            string[] nombres = { "Juan", "Ana", "Luis", "Maria", "Carlos", "Sofia", "Pedro", "Laura", "Miguel", "Elena", "Roberto", "Lucia" };
            string[] apellidos = { "Perez", "Gomez", "Rodriguez", "Diaz", "Hernandez", "Lopez", "Gonzalez", "Martinez", "Sanchez", "Ramirez", "Torres", "Flores" };
            Random rnd = new Random();

            for (int i = 1; i <= 1000; i++)
            {
                string nom = nombres[rnd.Next(nombres.Length)];
                string ape = apellidos[rnd.Next(apellidos.Length)];
                string nss = $"NSS-{10000 + i}";

                int tipo = (i % 4) + 1; 

                if (tipo == 1)
                    AgregarEmpleado(new EmpleadoAsalariado(nom, ape, nss, rnd.Next(800, 1500)));
                else if (tipo == 2)
                    AgregarEmpleado(new EmpleadoPorHoras(nom, ape, nss, 20, rnd.Next(30, 50)));
                else if (tipo == 3)
                    AgregarEmpleado(new EmpleadoPorComision(nom, ape, nss, rnd.Next(10000, 20000), 0.05m));
                else
                    AgregarEmpleado(new EmpleadoAsalariadoPorComision(nom, ape, nss, rnd.Next(8000, 15000), 0.04m, 400));
            }

            Console.WriteLine($"\nÉXITO: Se han cargado {listaEmpleados.Count} empleados en memoria.");
            PresioneTecla();
        }

        static void VerEmpleadosUI()
        {
            if (listaEmpleados.Count == 0)
            {
                Console.WriteLine("No hay datos. Por favor seleccione la opción 2 para importar.");
                PresioneTecla();
                return;
            }

            Console.Clear();
            Console.WriteLine("============================================================");
            Console.WriteLine($"   LISTADO COMPLETO ({listaEmpleados.Count} EMPLEADOS)");
            Console.WriteLine("============================================================");

            int contador = 1;
            foreach (var e in listaEmpleados)
            {
                
                Console.WriteLine($"\n--- [REGISTRO #{contador}] " + new string('-', 40));

                Console.WriteLine(e.ToString());

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($">>> PAGO TOTAL A LA FECHA: {e.CalcularPago():C2}");
                Console.ResetColor();

                contador++;
            }

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("Fin del listado.");
            PresioneTecla();
        }

        static void GenerarReporteDetallado()
        {
            if (listaEmpleados.Count == 0)
            {
                Console.WriteLine("\nNo hay datos para generar el reporte.");
                PresioneTecla();
                return;
            }

            Console.Clear();
            Console.WriteLine("=========================================");
            Console.WriteLine("   REPORTE DE NÓMINA POR CATEGORÍA");
            Console.WriteLine("=========================================\n");

            decimal granTotal = 0;
            var grupos = listaEmpleados.GroupBy(e => e.ObtenerTipo());

            Console.WriteLine("{0,-20} | {1,8} | {2,15} | {3,15}", "CATEGORÍA", "CANT.", "SUBTOTAL", "PROMEDIO");
            Console.WriteLine(new string('-', 70));

            foreach (var grupo in grupos)
            {
                decimal subtotal = grupo.Sum(e => e.CalcularPago());
                granTotal += subtotal;
                decimal promedio = subtotal / grupo.Count();

                Console.WriteLine("{0,-20} | {1,8} | {2,15:C2} | {3,15:C2}",
                    grupo.Key, grupo.Count(), subtotal, promedio);
            }

            Console.WriteLine(new string('-', 70));
            Console.WriteLine($"TOTAL NÓMINA GENERAL: {granTotal:C2}");
            Console.WriteLine($"TOTAL EMPLEADOS:      {listaEmpleados.Count}");

            PresioneTecla();
        }

        static void RegistrarEmpleadoUI()
        {
            Console.WriteLine("\n--- Registro Manual ---");
            Console.Write("NSS: "); string nss = Console.ReadLine();
            Console.Write("Nombre: "); string nom = Console.ReadLine();
            Console.Write("Apellido: "); string ape = Console.ReadLine();

            Console.WriteLine("Tipos: 1.Asalariado, 2.Por Horas, 3.Comisión, 4.Asalariado+Com");
            Console.Write("Seleccione Tipo: ");
            string tipo = Console.ReadLine();

            try
            {
                if (tipo == "1")
                {
                    Console.Write("Salario Semanal: ");
                    AgregarEmpleado(new EmpleadoAsalariado(nom, ape, nss, decimal.Parse(Console.ReadLine())));
                }
                else if (tipo == "2")
                {
                    Console.Write("Sueldo Hora: "); decimal sh = decimal.Parse(Console.ReadLine());
                    Console.Write("Horas Trab: "); decimal ht = decimal.Parse(Console.ReadLine());
                    AgregarEmpleado(new EmpleadoPorHoras(nom, ape, nss, sh, ht));
                }
                else if (tipo == "3")
                {
                    Console.Write("Ventas: "); decimal v = decimal.Parse(Console.ReadLine());
                    Console.Write("Tarifa (ej 0.05): "); decimal t = decimal.Parse(Console.ReadLine());
                    AgregarEmpleado(new EmpleadoPorComision(nom, ape, nss, v, t));
                }
                else if (tipo == "4")
                {
                    Console.Write("Ventas: "); decimal v = decimal.Parse(Console.ReadLine());
                    Console.Write("Tarifa: "); decimal t = decimal.Parse(Console.ReadLine());
                    Console.Write("Salario Base: "); decimal b = decimal.Parse(Console.ReadLine());
                    AgregarEmpleado(new EmpleadoAsalariadoPorComision(nom, ape, nss, v, t, b));
                }
                Console.WriteLine("Registrado.");
            }
            catch { Console.WriteLine("Error formato."); }
            PresioneTecla();
        }

        static void BuscarYGestionarUI()
        {
            Console.Write("\nIngrese NSS a buscar: ");
            string nss = Console.ReadLine();
            var emp = listaEmpleados.FirstOrDefault(e => e.NumeroSeguroSocial == nss);

            if (emp != null)
            {
                Console.WriteLine("\n" + new string('-', 40));
                Console.WriteLine(emp.ToString());
                Console.WriteLine($">>> PAGO: {emp.CalcularPago():C2}");
                Console.WriteLine(new string('-', 40));

                Console.WriteLine("\n1. Editar Nombre\n2. Regresar");
                if (Console.ReadLine() == "1")
                {
                    Console.Write("Nuevo Nombre: ");
                    string n = Console.ReadLine();
                    if (!string.IsNullOrEmpty(n)) emp.PrimerNombre = n;
                    Console.WriteLine("Actualizado.");
                }
            }
            else Console.WriteLine("No encontrado.");
            PresioneTecla();
        }

        static void PresioneTecla()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}