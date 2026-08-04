using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV.DTOs
{
    internal class ClienteDTO
    {
        public string IdCliente { get; set; }
        public string NombreCompleto { get; set; }
        public string NumeroTelefono { get; set; }
        public string CorreoElectronico { get; set; }
    }
}