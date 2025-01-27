using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ReporteCalificaciones
{
    public partial class Principal : Form
    {
        int pagina = 0;
        String nombre = "";
        String grado = "";
        String curp = "";
        String matricula = "";

        #region Tipos de Fuente

        /*Tipos de Letra*/
        static Font F6 = new Font("Arial", 6, FontStyle.Regular);
        static Font F8 = new Font("Arial", 8, FontStyle.Regular);
        static Font F10 = new Font("Arial", 10, FontStyle.Regular);
        static Font F12 = new Font("Arial", 12, FontStyle.Regular);
        static Font F14 = new Font("Arial", 14, FontStyle.Regular);
        static Font F16 = new Font("Arial", 16, FontStyle.Regular);
        static Font F20 = new Font("Arial", 20, FontStyle.Regular);
        static Font F24 = new Font("Arial", 24, FontStyle.Regular);
        static Font F24b = new Font("Arial", 24, FontStyle.Bold);
        static Font F32 = new Font("Arial", 32, FontStyle.Regular);
        static Pen P1 = new Pen(Color.Black, (float)0.01);
        static Pen Pb = new Pen(Color.LightBlue, (float)0.01);
        static Pen Pa = new Pen(Color.Lavender, (float)0.01);
        static Pen Pn = new Pen(Color.Orange, (float)0.02);
        static Brush Negro = Brushes.Black;
        static Brush Azul = Brushes.Blue;
        static Brush Verde = Brushes.Green;
        static Brush Rojo = Brushes.Red;
        static Brush Cafe = Brushes.Chocolate;
        static Brush Gris = Brushes.LightGray;

        #endregion

        public Principal()
        {
            InitializeComponent();

            Icon icono = new Icon("reporte.ico");
            reporte.Icon = icono;
            reporte.Text = "Reporte de calificaciones";
            reporte.WindowState = FormWindowState.Maximized;
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            grafica_calificaciones.Series[0].Points.AddXY("Matematicas", 8);
            grafica_calificaciones.Series[0].Points.AddXY("Español", 6);
            grafica_calificaciones.Series[0].Points.AddXY("Ingles", 9);
            grafica_calificaciones.Series[0].Points.AddXY("Quimica", 7);
            grafica_calificaciones.Series[0].Points.AddXY("Biologia", 10);
        }

        private void bt_generarReporte_Click(object sender, EventArgs e)
        {
            //Optener datos
            nombre = textBox_nombre.Text;
            grado = textBox_grado.Text;
            curp = textBox_curp.Text;
            matricula = textBox_matricula.Text;

            //Crear documento
            PrintDocument Documento = new PrintDocument();
            Documento.DefaultPageSettings.Color = true;
            Documento.DefaultPageSettings.PaperSize = new PaperSize("Carta", 850, 1100);
            Documento.DefaultPageSettings.Landscape = false;
            pagina = 0;

            Documento.DocumentName = nombre + " - " + matricula;

            reporte.Document = Documento;

            Documento.PrintPage += new PrintPageEventHandler(ObtieneDocumento);

            reporte.Activate();
            reporte.ShowDialog();
            Documento.Dispose();
        }

        private void ObtieneDocumento(object sender, PrintPageEventArgs e)
        {
            switch (pagina)
            {
                case 0:
                    //Construir pagina inicial con resultados generales
                    PaginaInicial(e);
                    break;
                case 1:
                    //Construir pagina 2 con resultado de prueba de Blower
                    PaginaCalificaciones(e);
                    break;
            }

            pagina++;

            if (pagina < 2)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                pagina = 0;
            }

        }

        private void PaginaInicial(PrintPageEventArgs e)
        {
            int x = 0;
            int y = 0;
            int espacio = 0;

            //Encabezado
            e.Graphics.DrawImage(Reporte.Properties.Resources.logoEscuela, 50, 10);
            e.Graphics.DrawString(matricula, F16, Verde, 210, 40);
            e.Graphics.DrawString("Calificaciones", F16, Negro, 650, 40);
            e.Graphics.DrawLine(P1, 50, 90, 800, 90);

            //Foto del alumno
            e.Graphics.DrawImage(picture_fotoAlumno.Image, 50, 100);

            //Datos generales
            x = 50;
            y = 400;
            espacio = 50;

            e.Graphics.DrawString("Fecha de emicion: " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString(), F12, Negro, x, y);
            y = y + espacio;
            e.Graphics.DrawString("Nombre: " + nombre, F12, Negro, x, y);
            y = y + espacio;
            e.Graphics.DrawString("Grado: " + grado, F12, Negro, x, y);
            y = y + espacio;
            e.Graphics.DrawString("Curp: " + curp, F12, Negro, x, y);
            y = y + espacio;
            e.Graphics.DrawString("Matricula: " + matricula, F12, Negro, x, y);
            y = y + espacio;
            e.Graphics.DrawLine(P1, x, y, 500, y);

            //Imagen de calificacion
            Image calificacion = Image.FromFile("calificacion_a.png");
            e.Graphics.DrawImage(calificacion, 450, 200);

            //Pie de pagina
            e.Graphics.DrawLine(P1, 50, 1040, 800, 1040);
            e.Graphics.DrawString(string.Format("Página: {0:D2}", pagina + 1), F8, Negro, 745, 1050);
        }

        private void PaginaCalificaciones(PrintPageEventArgs e)
        {
            //Encabezado
            e.Graphics.DrawImage(Reporte.Properties.Resources.logoEscuela, 50, 10);
            e.Graphics.DrawString(matricula, F16, Verde, 210, 40);
            e.Graphics.DrawString("Calificaciones", F16, Negro, 650, 40);
            e.Graphics.DrawLine(P1, 50, 90, 800, 90);

            var ImS = new MemoryStream();
            grafica_calificaciones.SaveImage(ImS, ChartImageFormat.Png);
            e.Graphics.DrawImage(new Bitmap(ImS), 120, 300);

            //Pie de pagina
            e.Graphics.DrawLine(P1, 50, 1040, 800, 1040);
            e.Graphics.DrawString(string.Format("Página: {0:D2}", pagina + 1), F8, Negro, 745, 1050);
        }

    }
}
