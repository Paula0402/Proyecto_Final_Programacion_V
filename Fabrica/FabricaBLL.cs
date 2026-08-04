using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinalProgramacionV.BLL;

namespace ProyectoFinalProgramacionV.Fabrica
{
    internal static class FabricaBLL
    {
        public static ProductoBLL CrearProductoBLL()
        {
            return new ProductoBLL();
        }

        public static ClienteBLL CrearClienteBLL()
        {
            return new ClienteBLL();
        }

        public static VentaBLL CrearVentaBLL(ProductoBLL productoBLL, ClienteBLL clienteBLL)
        {
            return new VentaBLL(productoBLL, clienteBLL);
        }
    }
}