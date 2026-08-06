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
    internal class ProductoBLL
    {
        private IProductoDAL productoDAL;

        public ProductoBLL()
        {
            productoDAL = new ProductoDAL();
        }

        public void AgregarProducto(Producto producto)
        {
            ValidarProducto(producto);

            if (BuscarProductoPorCodigo(producto.CodigoDeArticulo) != null)
                throw new Exception($"Ya existe un producto con el codigo '{producto.CodigoDeArticulo}'. Usa un codigo diferente.");

            productoDAL.Insertar(ConvertirADTO(producto));
        }

        public void ActualizarProducto(Producto producto)
        {
            ValidarProducto(producto);
            productoDAL.Actualizar(ConvertirADTO(producto));
        }

        public void EliminarProducto(string codigoArticulo)
        {
            if (string.IsNullOrWhiteSpace(codigoArticulo))
                throw new Exception("El codigo del producto es obligatorio.");

            productoDAL.Eliminar(codigoArticulo);
        }

        public Producto BuscarProductoPorCodigo(string codigoArticulo)
        {
            ProductoDTO dto = productoDAL.BuscarPorCodigo(codigoArticulo);
            return dto == null ? null : ConvertirADominio(dto);
        }

        public List<Producto> ObtenerTodosLosProductos()
        {
            return productoDAL.ObtenerTodos().Select(ConvertirADominio).ToList();
        }

        // Validacion de reglas de negocio antes de guardar un producto
        private void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.CodigoDeArticulo))
                throw new Exception("El codigo del producto es obligatorio.");

            if (string.IsNullOrWhiteSpace(producto.NombreDelArticulo))
                throw new Exception("El nombre del producto es obligatorio.");

            if (producto.NombreDelArticulo.Length < 3)
                throw new Exception("El nombre del producto debe tener al menos 3 caracteres.");

            if (producto.PrecioUnitario <= 0)
                throw new Exception("El precio unitario debe ser mayor a cero.");

            if (producto.CantidadEnStock < 0)
                throw new Exception("El stock no puede ser negativo.");
        }

        private ProductoDTO ConvertirADTO(Producto producto)
        {
            return new ProductoDTO
            {
                CodigoArticulo = producto.CodigoDeArticulo,
                NombreArticulo = producto.NombreDelArticulo,
                DescripcionArticulo = producto.DescripcionDelArticulo,
                PrecioUnitario = producto.PrecioUnitario,
                CantidadEnStock = producto.CantidadEnStock,
                StockMinimo = producto.StockMinimoPermitido
            };
        }

        private Producto ConvertirADominio(ProductoDTO dto)
        {
            return new Producto(dto.CodigoArticulo, dto.NombreArticulo, dto.DescripcionArticulo,
                                 dto.PrecioUnitario, dto.CantidadEnStock, dto.StockMinimo);
        }
    }
}