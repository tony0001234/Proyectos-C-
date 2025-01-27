using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ejercicio_de_repaso
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string nArchivo = "nArchivo.txt";
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombre = txNombre.Text.Trim();
            string codigo = txCodigo.Text.Trim();
            double sueldo;
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(codigo) || !double.TryParse(txSueldo.Text, out sueldo))
            {
                MessageBox.Show("porfavor ingrese datos validos");
                return;
            }
            using (StreamWriter sw = new StreamWriter(nArchivo, true))
            {
                sw.WriteLine($"{nombre}, {codigo}, {sueldo}");
            }
            limpiarCampos();
        }
        public void limpiarCampos()
        {
            txCodigo.Clear();
            txSueldo.Clear();
            txNombre.Clear();
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (File.Exists(nArchivo))
            {
                string copiaTemp = File.ReadAllText(nArchivo);
                txVer.Text = copiaTemp;
            }
            limpiarCampos ();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            string codigo = txCodigo.Text;
            if(string.IsNullOrEmpty(codigo))
            {
                MessageBox.Show("porfavor ingrese un codigo para borrar su registro");
                return;
            }
            if(!File.Exists(nArchivo))
            {
                MessageBox.Show("El archivo aun no ha sido creado");
                return;
            }
            string[] copiaTemp = File.ReadAllLines(nArchivo);

            using(StreamWriter sw =  new StreamWriter(nArchivo))
            {
                foreach(string s in copiaTemp)
                {
                    if (!s.Contains(codigo))
                    {
                        sw.WriteLine(s);
                    }
                }
            }
            limpiarCampos();
            txVer.Clear();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            string nuevoNombre = txNombre.Text.Trim();
            string codigo = txCodigo.Text.Trim();
            double nuevoSueldo;
            if (!File.Exists(nArchivo))
            {
                MessageBox.Show("El archivo aun no ha sido creado");
                return;
            }
            if (string.IsNullOrEmpty(nuevoNombre) || string.IsNullOrEmpty(codigo) || !double.TryParse(txSueldo.Text, out nuevoSueldo))
            {
                MessageBox.Show("porfavor ingrese datos validos");
                return;
            }
            string[] copiaTemp = File.ReadAllLines(nArchivo);

            for (int i = 0; i < copiaTemp.Length; i++)
            {
                if (copiaTemp[i].Contains(codigo))
                {
                     copiaTemp[i] = $"{nuevoNombre}, {codigo}, {nuevoSueldo}";
                     break;
                }
            }
            File.WriteAllLines(nArchivo, copiaTemp);
            MessageBox.Show("Edicion realizada con exito");
            limpiarCampos();
        }
    }
}
