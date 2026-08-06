using ProyectoFinalProgramacionV.DAL;
using ProyectoFinalProgramacionV.DTOs;
using ProyectoFinalProgramacionV.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacionV.BLL
{
    internal class ClienteBLL
    {
        private IClienteDAL clienteDAL;

        public ClienteBLL()
        {
            clienteDAL = new ClienteDAL();
        }

        public void AgregarCliente(Cliente cliente)
        {
            ValidarCliente(cliente);

            if (BuscarClientePorId(cliente.IdentificadorDeCliente) != null)
                throw new Exception($"Ya existe un cliente con el identificador '{cliente.IdentificadorDeCliente}'. Usa uno diferente.");

            clienteDAL.Insertar(ConvertirADTO(cliente));
        }

        public void ActualizarCliente(Cliente cliente)
        {
            ValidarCliente(cliente);
            clienteDAL.Actualizar(ConvertirADTO(cliente));
        }

        public void EliminarCliente(string idCliente)
        {
            if (string.IsNullOrWhiteSpace(idCliente))
                throw new Exception("El identificador del cliente es obligatorio.");

            clienteDAL.Eliminar(idCliente);
        }

        public Cliente BuscarClientePorId(string idCliente)
        {
            ClienteDTO dto = clienteDAL.BuscarPorId(idCliente);
            return dto == null ? null : ConvertirADominio(dto);
        }

        public List<Cliente> ObtenerTodosLosClientes()
        {
            return clienteDAL.ObtenerTodos().Select(ConvertirADominio).ToList();
        }

        // Validacion de reglas de negocio antes de guardar un cliente
        private void ValidarCliente(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.IdentificadorDeCliente))
                throw new Exception("El identificador del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.NombreCompleto))
                throw new Exception("El nombre del cliente es obligatorio.");

            if (cliente.NombreCompleto.Length < 3)
                throw new Exception("El nombre del cliente debe tener al menos 3 caracteres.");

            if (!string.IsNullOrWhiteSpace(cliente.CorreoElectronico) && !cliente.CorreoElectronicoEsValido())
                throw new Exception("El correo electronico no tiene un formato valido.");
        }

        private ClienteDTO ConvertirADTO(Cliente cliente)
        {
            return new ClienteDTO
            {
                IdCliente = cliente.IdentificadorDeCliente,
                NombreCompleto = cliente.NombreCompleto,
                NumeroTelefono = cliente.NumeroDeTelefono,
                CorreoElectronico = cliente.CorreoElectronico
            };
        }

        private Cliente ConvertirADominio(ClienteDTO dto)
        {
            return new Cliente(dto.IdCliente, dto.NombreCompleto, dto.NumeroTelefono, dto.CorreoElectronico);
        }
    }
}