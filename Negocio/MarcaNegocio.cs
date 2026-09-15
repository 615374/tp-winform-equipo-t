using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {
        //METODO LISTAR MARCAS
        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
        //el try catch es para manejar los errores que puedan surgir al momento de ejecutar la consulta
        try
        {
             datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");
             datos.ejecutarLectura();

            while (datos.Lector.Read())
            {
                Marca aux = new Marca();

                aux.Id = (int) datos.Lector["Id"];
                aux.Descripcion = (string) datos.Lector["Descripcion"];
                
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

        //METODO AGREGAR MARCA
        public void agregar(Marca nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO MARCAS (Descripcion) VALUES (@Descripcion)");
                datos.setearParametro("@Descripcion", nueva.Descripcion);
                datos.ejecutarAccion();
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

        //METODO MODIFICAR MARCA
        public void modificar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @Descripcion WHERE Id = @Id");
                datos.setearParametro("@Descripcion", marca.Descripcion);
                datos.setearParametro("@Id", marca.Id);
                datos.ejecutarAccion();
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

        //METODO ELIMINAR MARCA
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
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


