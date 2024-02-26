using dominio;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace negocios
{
    public class CategoriaNegocio
    {
        public List<Categorias> Listar()
        {
            List<Categorias> lista = new List<Categorias>();
            AccesodeDatos datos = new AccesodeDatos();

            try
            {
                datos.setearConsulta("Select Id , Descripcion from CATEGORIAS");
                datos.ejecutarAccion();

                while (datos.lector.Read())
                {
                    Categorias aux = new Categorias();
                    aux.Id = (int)datos.lector["Id"];
                    aux.Descripcion = (string)datos.lector["Descripcion"];

                    lista.Add(aux);
                }
                return lista;

            }
            catch (Exception EX)
            {

                throw EX;
            }
        }
    }
}
