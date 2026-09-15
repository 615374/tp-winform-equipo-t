using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, 
                                           M.Id AS IdMarca, M.Descripcion AS Marca, 
                                           C.Id AS IdCategoria, C.Descripcion AS Categoria 
                                    FROM ARTICULOS A 
                                    LEFT JOIN MARCAS M ON A.IdMarca = M.Id 
                                    LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id";

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                ImagenNegocio imagenNegocio = new ImagenNegocio();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    // Instanciar objetos antes de asignar propiedades
                    if (!(datos.Lector["Marca"] is DBNull))
                    {
                        aux.Marca = new Marca();
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    }

                    if (!(datos.Lector["Categoria"] is DBNull))
                    {
                        aux.Categoria = new Categoria();
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    }

                    // Cargar lista de imágenes asociadas a cada artículo
                    aux.Imagenes = imagenNegocio.listarPorIdArticulo(aux.Id);

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // METODO FILTRAR ARTICULOS
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
            string consulta = @"SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio,
                           M.Id AS IdMarca, M.Descripcion AS Marca,
                           C.Id AS IdCategoria, C.Descripcion AS Categoria
                    FROM ARTICULOS A
                    LEFT JOIN MARCAS M ON A.IdMarca = M.Id
                    LEFT JOIN CATEGORIAS C ON A.IdCategoria = C.Id
                    WHERE ";

            if (campo == "Nombre")
            {
                if (criterio == "Comienza con")
                {
                    consulta += "A.Nombre LIKE @filtro";
                }
                else if (criterio == "Termina con")
                {
                    consulta += "A.Nombre LIKE @filtro";
                }
                else
                {
                    consulta += "A.Nombre LIKE @filtro";
                }
            }
            else if (campo == "Codigo")
            {
                if (criterio == "Comienza con")
                {
                    consulta += "A.Codigo LIKE @filtro";
                }
                else if (criterio == "Termina con")
                {
                    consulta += "A.Codigo LIKE @filtro";
                }
                else
                {
                    consulta += "A.Codigo LIKE @filtro";
                }
            }
            else if (campo == "Precio")
            {
                if (criterio == "Mayor a")
                {
                    consulta += "A.Precio > @filtro";
                }
                else if (criterio == "Menor a")
                    {
                        consulta += "A.Precio < @filtro";
                    }
                else
                {
                    consulta += "A.Precio = @filtro";
                }
            }
            if(campo == "Nombre" || campo == "Codigo")
            {
                if (criterio == "Comienza con")
                {
                    datos.setearParametro("@filtro", filtro + "%");
                }
                else if (criterio == "Termina con")
                {
                    datos.setearParametro("@filtro", "%" + filtro);
                }
                else
                {
                    datos.setearParametro("@filtro", "%" + filtro + "%");
                }
            }
            else if (campo == "Precio")
            {
                decimal precioFiltro;
                if (decimal.TryParse(filtro, out precioFiltro))
                {
                    datos.setearParametro("@filtro", precioFiltro);
                }
                else
                {
                    throw new Exception("El filtro de precio debe ser un número válido.");
                }
            }
            datos.setearConsulta(consulta);
            datos.ejecutarLectura();
            ImagenNegocio imagenNegocio = new ImagenNegocio();
            while (datos.Lector.Read())
            {
                Articulo aux = new Articulo();
                aux.Id = (int)datos.Lector["Id"];
                aux.Codigo = (string)datos.Lector["Codigo"];
                aux.Nombre = (string)datos.Lector["Nombre"];

                if (!(datos.Lector["Descripcion"] is DBNull))
                {
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                }
                aux.Precio = (decimal)datos.Lector["Precio"];
                if (!(datos.Lector["Marca"] is DBNull))
                {
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                }
                if (!(datos.Lector["Categoria"] is DBNull))
                {
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                }
                aux.Imagenes = imagenNegocio.listarPorIdArticulo(aux.Id);
                lista.Add(aux);
            }
            return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}