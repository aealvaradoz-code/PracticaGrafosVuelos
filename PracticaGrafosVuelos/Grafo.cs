using System;
using System.Collections.Generic;

public class Grafo
{
    private Dictionary<string, List<Vuelo>> ciudades;

    public Grafo()
    {
        ciudades = new Dictionary<string, List<Vuelo>>();
    }

    public void AgregarRuta(string origen, string destino, int costo)
    {
        if (!ciudades.ContainsKey(origen))
            ciudades[origen] = new List<Vuelo>();

        if (!ciudades.ContainsKey(destino))
            ciudades[destino] = new List<Vuelo>();

        ciudades[origen].Add(new Vuelo(destino, costo));
        ciudades[destino].Add(new Vuelo(origen, costo));
    }

    public void MostrarCiudades()
    {
        Console.WriteLine("\nCIUDADES REGISTRADAS");

        foreach (string ciudad in ciudades.Keys)
        {
            Console.WriteLine("- " + ciudad);
        }
    }

    public void MostrarRutas()
    {
        Console.WriteLine("\nRUTAS DISPONIBLES");

        foreach (var ciudad in ciudades)
        {
            foreach (var vuelo in ciudad.Value)
            {
                Console.WriteLine(
                    ciudad.Key + " -> " +
                    vuelo.Destino +
                    " ($" + vuelo.Costo + ")"
                );
            }
        }
    }

    public void BuscarRutaMasBarata(string origen, string destino)
    {
        Dictionary<string, int> distancias = new Dictionary<string, int>();
        Dictionary<string, string> anterior = new Dictionary<string, string>();
        HashSet<string> visitados = new HashSet<string>();

        foreach (string ciudad in ciudades.Keys)
        {
            distancias[ciudad] = int.MaxValue;
        }

        distancias[origen] = 0;

        while (visitados.Count < ciudades.Count)
        {
            string actual = null;
            int menor = int.MaxValue;

            foreach (string ciudad in ciudades.Keys)
            {
                if (!visitados.Contains(ciudad) &&
                    distancias[ciudad] < menor)
                {
                    menor = distancias[ciudad];
                    actual = ciudad;
                }
            }

            if (actual == null)
                break;

            visitados.Add(actual);

            foreach (Vuelo vuelo in ciudades[actual])
            {
                int nuevaDistancia =
                    distancias[actual] + vuelo.Costo;

                if (nuevaDistancia < distancias[vuelo.Destino])
                {
                    distancias[vuelo.Destino] = nuevaDistancia;
                    anterior[vuelo.Destino] = actual;
                }
            }
        }

        if (!distancias.ContainsKey(destino) ||
            distancias[destino] == int.MaxValue)
        {
            Console.WriteLine("\nNo existe una ruta disponible.");
            return;
        }

        List<string> ruta = new List<string>();

        string paso = destino;

        while (paso != origen)
        {
            ruta.Add(paso);
            paso = anterior[paso];
        }

        ruta.Add(origen);
        ruta.Reverse();

        Console.WriteLine("\nRUTA MAS BARATA");
        Console.WriteLine(string.Join(" -> ", ruta));
        Console.WriteLine("Costo total: $" + distancias[destino]);
    }
}