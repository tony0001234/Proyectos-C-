using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using crud_p_canal_codigo_estudiante_.DAL;
using prueba25_10_23.PL;

namespace prueba25_10_23
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clsConexion.Conectar();
            MessageBox.Show("Conexion con base de datos exitosa");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsConexion.Conectar();
            string usuario = "", contraseña = "";
            usuario = txtUsuario.Text;
            contraseña = txtPass.Text;
            string consulta = $"SELECT * FROM USUARIO WHERE USUARIO='{usuario}' AND CONTRASEÑA='{contraseña}' ";
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

            frmMenu ventana = new frmMenu();
            ventana.Show();
            this.Hide();

        }
    }
}
