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
        public void agregar(Articulo articulo) 
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                /* Versión acortada del Insert incluyendo el método de setearParametro por cada valor
                string consulta = @"Insert into ARTICULOS 
                                (Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values
                                (@Id, @Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio)";
                datos.setearParametro("@Id", articulo.Id);
                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion);
                datos.setearParametro("@IdMarca", articulo.Marca.Id);
                datos.setearParametro("@IdCategoria", articulo.Categoria.Id);
                datos.setearParametro("@Precio", articulo.Precio);*/

                string consulta = @"Insert into ARTICULOS 
                                (Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio) values 
                                ("+articulo.Id+","+articulo.Codigo+","+articulo.Nombre+","+articulo.Descripcion+","
                                +articulo.Marca.Id+","+articulo.Categoria.Id+","+articulo.Precio+")";
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
            }
            catch (Exception ex) {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }            
        }
    }
}