using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoFinalProgramacionV.DAL;
using ProyectoFinalProgramacionV.DTOs;

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
            productoDAL.Insertar(ConvertirADTO(producto));
        }

        public void ActualizarProducto(Producto producto)
        {
            productoDAL.Actualizar(ConvertirADTO(producto));
        }

        public void EliminarProducto(string codigoArticulo)
        {
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