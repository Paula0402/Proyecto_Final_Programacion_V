using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV
{

    /// Clase base abstracta que representa a una persona dentro del sistema.
    /// Es la clase padre de Cliente y cualquier otro tipo de persona que se agregue después.

    internal abstract class Persona
    {

        ///Propiedades

        public string NombreCompleto { get; set; }


        public string NumeroDeTelefono { get; set; }


        public string CorreoElectronico { get; set; }


   



        /// Constructor que inicializa los datos básicos de una persona.

        protected Persona(string nombreCompleto, string numeroDeTelefono, string correoElectronico)
        {
            NombreCompleto = nombreCompleto;
            NumeroDeTelefono = numeroDeTelefono;
            CorreoElectronico = correoElectronico;
        }


        // Métodos abstractos 


        public abstract string ObtenerInformacionResumida();



        public bool CorreoElectronicoEsValido()
        {
            return !string.IsNullOrEmpty(CorreoElectronico)
                && CorreoElectronico.Contains("@")
                && CorreoElectronico.Contains(".");
        }


        public void MostrarInformacionEnConsola()
        {
            Console.WriteLine(ObtenerInformacionResumida());
        }
    }
}