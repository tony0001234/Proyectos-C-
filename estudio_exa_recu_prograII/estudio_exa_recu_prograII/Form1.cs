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

namespace estudio_exa_recu_prograII
{
    public partial class Form1 : Form
    {
        string archivoEmpleados = "empleados.txt"
        public Form1()
        {
            InitializeComponent();

            CargarEmp();
            lstEmpleados.SelectedIndexChanged += lstEmpleados_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void CargarEmp()
        {
            lstEmpleados.Items.Clear();
            if(File.Exists(archivoEmpleados))
            {
                using (StreamReader Lectura = new StreamReader(archivoEmpleados))
                {
                    string linea;
                    while ((linea = Lectura.ReadLine()) != null)
                    {
                        string[] campos = linea.Split(',');
                        if (campos.Length == 3)
                        {
                            string nombre = campos[0];
                            string codigo = campos[1];
                            double sueldo = Convert.ToDouble(campos[2]);
                            lstEmpleados.Items.Add(new Empleado { nombre = nombre, Codigo = codigo, Sueldo = sueldo });
                        }
                    }
                }
        }
    }

        private void button1_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string codigo = txtCodigo.Text.Trim();
            double sueldo;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(codigo) || !double.TryParse(txtSueldo.Text, out sueldo))
            {
                MessageBox.Show("Por favor, ingrese datos validos.");
                return;
            }
            using (StreamWriter EscribeEnArchivo = new StreamWriter(archivoEmpleados, true))
            {
                EscribeEnArchivo.WriteLine($"{nombre}, {codigo}, {sueldo}");
            }

            CargarEmp();
            LimpiarCampos();
        }
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtCodigo.Clear();
            txtSueldo.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(lstEmpleados.SelectedItems != null)
            {
                Empleado empleado = (Empleado)lstEmpleados.SelectedItem;
                string nuevoNombre = txtNombre.Text.Trim();
                string nuevoCodigo = txtCodigo.Text.Trim();
                double nuevoSueldo = Convert.ToDouble(txtSueldo.Text);

                if(string.IsNullOrEmpty(nuevoNombre) || string.IsNullOrEmpty(nuevoCodigo) || !double.TryParse(txtSueldo.Text, out nuevoSueldo))
                {
                    MessageBox.Show("Porfavor, ingrese datos validos.");
                    return;
                }
                string nuevaLinea = $"{nuevoNombre}, {nuevoCodigo}, {nuevoSueldo}";
                string[] lineas = File.ReadAllLines(archivoEmpleados);
                for(int i = 0; i < lineas.Length; i++)
                {
                    if (lineas[i].Contains(empleado.Codigo))
                    {
                        lineas[i] = nuevaLinea;
                        break;
                    }
                }
                File.WriteAllLines(archivoEmpleados, lineas);
                CargarEmp();
                LimpiarCampos();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(lstEmpleados.SelectedItems != null)
            {
                Empleado empleado = (Empleado)lstEmpleados.SelectedItem;
                string[] lineas = File.ReadAllLines (archivoEmpleados);
                using (StreamWriter EscribeEnArchivo = new StreamWriter(archivoEmpleados))
                {
                    foreach (string line in lineas)
                    {
                        if (!lineas.Contains(empleado.Codigo))
                        {
                            EscribeEnArchivo.WriteLine(line);
                        }
                    }
                    CargarEmp();
                    LimpiarCampos ();
                }
            }
        }
        private void lstEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lstEmpleados_SelectedItem != null)
            {
                Empleado empleado = (Empleado)lstEmpleados.SelectedItem;
                txtNombre.Text = empleado.Nombre;
                txtCodigo.Text = empleado.Codigo;
                txtSueldo.Text = empleado.Sueldo.ToString();
            }
        }
        public class Empleado
        {
            public string Nombre { get; set; }
            public string Sueldo { get; set; }
            public string Codigo { get; set; }

            public override string ToString()
            {
                return $"{Nombre} ({Codigo})- Sueldo: {Sueldo:C}";
            }
        }
