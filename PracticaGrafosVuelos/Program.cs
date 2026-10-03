using System;
using System.IO;

class Program
{
    static void Main()
    {
        Grafo grafo = new Grafo();

        string archivo = "Vuelos.txt";

        if (!File.Exists(archivo))
        {
            Console.WriteLine("No se encontró el archivo Vuelos.txt");
            return;
        }

        string[] lineas = File.ReadAllLines(archivo);

        foreach (string linea in lineas)
        {
            string[] datos = linea.Split(',');

            if (datos.Length == 3)
            {
                string origen = datos[0].Trim();
                string destino = datos[1].Trim();
                int costo = int.Parse(datos[2].Trim());

                grafo.AgregarRuta(origen, destino, costo);
            }
        }

        int opcion = 0;

        while (opcion != 4)
        {
            Console.WriteLine("\n==============================");
            Console.WriteLine("     SISTEMA DE VUELOS");
            Console.WriteLine("==============================");
            Console.WriteLine("1. Mostrar ciudades");
            Console.WriteLine("2. Mostrar rutas");
            Console.WriteLine("3. Buscar ruta más barata");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            string? entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out opcion))
            {
                Console.WriteLine("\nOpción no válida.");
                continue;
            }

            switch (opcion)
            {
                case 1:
                    grafo.MostrarCiudades();
                    break;

                case 2:
                    grafo.MostrarRutas();
                    break;

                case 3:
                    Console.Write("\nIngrese la ciudad de origen: ");
                    string? origen = Console.ReadLine();

                    Console.Write("Ingrese la ciudad de destino: ");
                    string? destino = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(origen) &&
                        !string.IsNullOrWhiteSpace(destino))
                    {
                        grafo.BuscarRutaMasBarata(
                            origen.Trim(),
                            destino.Trim()
                        );
                    }
                    else
                    {
                        Console.WriteLine("\nDebe ingresar origen y destino.");
                    }

                    break;

                case 4:
                    Console.WriteLine("\nSaliendo del sistema...");
                    break;

                default:
                    Console.WriteLine("\nOpción no válida.");
                    break;
            }
        }
    }
}