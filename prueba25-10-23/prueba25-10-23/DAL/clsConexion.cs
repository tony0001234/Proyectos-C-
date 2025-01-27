using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace crud_p_canal_codigo_estudiante_.DAL
{
    class clsConexion
    {
        public static SqlConnection Conectar()
        {
            try
            {
                SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["UNICA"].ConnectionString);
                cn.Open();
                return cn;
            }
            catch (SqlException ex)
            {
                throw (ex);
            }
        }
        public static SqlConnection Close()
        {
            try
            {
                SqlConnection cn = new SqlConnection("SERVER=DESKTOP-ORUV2NS\\SQLEXPRESS; DATABASE=REGISTRO; integrated security=true");
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                    return cn;
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw (ex);
            }
        }
    }
}
