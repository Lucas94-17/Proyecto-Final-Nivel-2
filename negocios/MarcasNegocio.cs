using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocios
{
    public class MarcasNegocio
    {
        public List<Marcas> Listar()
        {
            List<Marcas> lista = new List<Marcas>();
            AccesodeDatos datos = new AccesodeDatos();

            try
            {
                datos.setearConsulta("Select Id , Descripcion from MARCAS");
                datos.ejecutarAccion();

                while (datos.lector.Read())
                {
                    Marcas aux = new Marcas();
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
