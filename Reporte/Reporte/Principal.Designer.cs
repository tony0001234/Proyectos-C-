namespace ReporteCalificaciones
{
    partial class Principal
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox_matricula = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_curp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_grado = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_nombre = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bt_generarReporte = new System.Windows.Forms.Button();
            this.grafica_calificaciones = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.reporte = new System.Windows.Forms.PrintPreviewDialog();
            this.picture_fotoAlumno = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grafica_calificaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_fotoAlumno)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox_matricula);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_curp);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_grado);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_nombre);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.picture_fotoAlumno);
            this.groupBox1.Location = new System.Drawing.Point(24, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(446, 269);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos reporte";
            // 
            // textBox_matricula
            // 
            this.textBox_matricula.Location = new System.Drawing.Point(219, 230);
            this.textBox_matricula.Name = "textBox_matricula";
            this.textBox_matricula.Size = new System.Drawing.Size(165, 20);
            this.textBox_matricula.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(216, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Matricula:";
            // 
            // textBox_curp
            // 
            this.textBox_curp.Location = new System.Drawing.Point(219, 173);
            this.textBox_curp.Name = "textBox_curp";
            this.textBox_curp.Size = new System.Drawing.Size(165, 20);
            this.textBox_curp.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(216, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Curp:";
            // 
            // textBox_grado
            // 
            this.textBox_grado.Location = new System.Drawing.Point(219, 108);
            this.textBox_grado.Name = "textBox_grado";
            this.textBox_grado.Size = new System.Drawing.Size(165, 20);
            this.textBox_grado.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Grado:";
            // 
            // textBox_nombre
            // 
            this.textBox_nombre.Location = new System.Drawing.Point(219, 40);
            this.textBox_nombre.Name = "textBox_nombre";
            this.textBox_nombre.Size = new System.Drawing.Size(165, 20);
            this.textBox_nombre.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre:";
            // 
            // bt_generarReporte
            // 
            this.bt_generarReporte.Location = new System.Drawing.Point(24, 12);
            this.bt_generarReporte.Name = "bt_generarReporte";
            this.bt_generarReporte.Size = new System.Drawing.Size(75, 23);
            this.bt_generarReporte.TabIndex = 1;
            this.bt_generarReporte.Text = "Generar";
            this.bt_generarReporte.UseVisualStyleBackColor = true;
            this.bt_generarReporte.Click += new System.EventHandler(this.bt_generarReporte_Click);
            // 
            // grafica_calificaciones
            // 
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.Maximum = 10D;
            chartArea1.Name = "ChartArea1";
            this.grafica_calificaciones.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.grafica_calificaciones.Legends.Add(legend1);
            this.grafica_calificaciones.Location = new System.Drawing.Point(24, 325);
            this.grafica_calificaciones.Name = "grafica_calificaciones";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar;
            series1.Legend = "Legend1";
            series1.Name = "Calificaciones";
            this.grafica_calificaciones.Series.Add(series1);
            this.grafica_calificaciones.Size = new System.Drawing.Size(446, 332);
            this.grafica_calificaciones.TabIndex = 2;
            // 
            // reporte
            // 
            this.reporte.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.reporte.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.reporte.ClientSize = new System.Drawing.Size(400, 300);
            this.reporte.Enabled = true;
            this.reporte.Icon = ((System.Drawing.Icon)(resources.GetObject("reporte.Icon")));
            this.reporte.Name = "reporte";
            this.reporte.Visible = false;
            // 
            // picture_fotoAlumno
            // 
            this.picture_fotoAlumno.Image = global::Reporte.Properties.Resources.imagenEstudiante;
            this.picture_fotoAlumno.Location = new System.Drawing.Point(15, 30);
            this.picture_fotoAlumno.Name = "picture_fotoAlumno";
            this.picture_fotoAlumno.Size = new System.Drawing.Size(155, 185);
            this.picture_fotoAlumno.TabIndex = 0;
            this.picture_fotoAlumno.TabStop = false;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 669);
            this.Controls.Add(this.grafica_calificaciones);
            this.Controls.Add(this.bt_generarReporte);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de calificaciones";
            this.Load += new System.EventHandler(this.Principal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grafica_calificaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_fotoAlumno)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox_matricula;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_curp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_grado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_nombre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picture_fotoAlumno;
        private System.Windows.Forms.Button bt_generarReporte;
        private System.Windows.Forms.DataVisualization.Charting.Chart grafica_calificaciones;
        private System.Windows.Forms.PrintPreviewDialog reporte;
    }
}

