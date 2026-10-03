public class Vuelo
{
    public string Destino { get; set; }
    public int Costo { get; set; }

    public Vuelo(string destino, int costo)
    {
        Destino = destino;
        Costo = costo;
    }
}