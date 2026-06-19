
using SistemaVentas.Facturacion;


class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Iniciando Sistema de Ventas...\n");

        ModuloDeFacturacion modulo = new ModuloDeFacturacion();
        modulo.IniciarMenuPrincipal();
    }
}

