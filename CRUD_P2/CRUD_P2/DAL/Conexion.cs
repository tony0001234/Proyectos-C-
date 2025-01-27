using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_P2
{
    class Conexion
    {
        public static SqlConnection Conectar()
        {
            SqlConnection cn = new SqlConnection("SERVER=DESKTOP-ORUV2NS\\SQLEXPRESS; DATABASE=REGISTRO; integrated security=true");
            cn.Open();
            return cn;
        }
    }
}
