using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV.Modelos
{
    internal class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string ContrasenaSinEncriptar { get; set; } // solo trabaja en memoria así, nunca se guarda asi en la BD

        public Usuario(string nombreUsuario, string nombreCompleto, string contrasenaSinEncriptar)
        {
            NombreUsuario = nombreUsuario;
            NombreCompleto = nombreCompleto;
            ContrasenaSinEncriptar = contrasenaSinEncriptar;
        }
    }
}