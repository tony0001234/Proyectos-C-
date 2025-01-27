using crud_p_canal_codigo_estudiante_.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace crud_p_canal_codigo_estudiante_
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsConexion.Conectar();
            string consulta = "SELECT * FROM USUARIO WHERE USUARIO='"+txtUsuario.Text+"' AND CONTRASEÑA='"+txtPass+"' ";
            SqlCommand comando = new SqlCommand(consulta, clsConexion.Conectar());
            DataTable dt = new DataTable();
            if (txtUsuario.Text == "" && txtPass.Text == "" || txtUsuario.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Porfavor escriba su nombre y usuario");
            }
            else
            {
                try
                {
                    SqlDataReader lector = comando.ExecuteReader();

                    if (lector.HasRows == true)
                    {
                        MessageBox.Show("Bienvenido");
                        txtPrueba.Text =lector.GetString(1);
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña invalidos porfavor intente nuevamente");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.ToString());
                }
            }
            clsConexion.Close();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            clsConexion.Conectar();
            MessageBox.Show("Conexion con base de datos exitosa");
        }
    }
}
