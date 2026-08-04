using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinalProgramacionV.DAL;
using ProyectoFinalProgramacionV.DTOs;

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
            clienteDAL.Insertar(ConvertirADTO(cliente));
        }

        public void ActualizarCliente(Cliente cliente)
        {
            clienteDAL.Actualizar(ConvertirADTO(cliente));
        }

        public void EliminarCliente(string idCliente)
        {
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