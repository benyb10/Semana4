using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semana4
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();

            InicializarComponentesPersonalizados();
        }
        // Vector para almacenar los puntos que formarán la figura 

        private Point[] puntos;

        // Matriz de ejemplo (podría representar una transformación) 

        private int[,] matriz;




        private void InicializarComponentesPersonalizados()

        {

            // Configuración básica del formulario 

            this.Text = "Aplicación Gráfica en .NET";

            this.Size = new Size(800, 600);



            // TextBox para ingresar el número de puntos (solo se permiten números) 

            TextBox txtNumeroPuntos = new TextBox();

            txtNumeroPuntos.Location = new Point(20, 20);

            txtNumeroPuntos.Width = 100;

            txtNumeroPuntos.Name = "txtNumeroPuntos";

            // Validación: solo se permiten dígitos 

            txtNumeroPuntos.KeyPress += TxtNumeroPuntos_KeyPress;

            this.Controls.Add(txtNumeroPuntos);



            // ComboBox para seleccionar el tipo de figura 

            ComboBox cmbFigura = new ComboBox();

            cmbFigura.Location = new Point(140, 20);

            cmbFigura.Width = 150;

            cmbFigura.Name = "cmbFigura";

            cmbFigura.Items.Add("Triángulo");

            cmbFigura.Items.Add("Cuadrado");

            cmbFigura.Items.Add("Círculo");

            // Aquí se podría agregar validación adicional o acciones al cambiar la selección 

            cmbFigura.SelectedIndexChanged += CmbFigura_SelectedIndexChanged;

            this.Controls.Add(cmbFigura);



            // Botón para generar y dibujar la figura 

            Button btnDibujar = new Button();

            btnDibujar.Location = new Point(310, 20);

            btnDibujar.Text = "Dibujar";

            btnDibujar.Click += BtnDibujar_Click;
            this.Controls.Add(btnDibujar);
        }
            
            private void BtnDibujar_Click(object sender, EventArgs e)
        {
            ComboBox cmbFigura = (ComboBox)this.Controls["cmbFigura"];

            if (cmbFigura.SelectedItem != null)
            {
                string figuraSeleccionada = cmbFigura.SelectedItem.ToString();

                Graphics g = this.CreateGraphics();

                DibujarFigura(g, figuraSeleccionada);
            }
            else
            {
                MessageBox.Show("Por favor seleccione una figura.");
            }
        }


        // Validación en el TextBox: permite solo dígitos y teclas de control 
        private void TxtNumeroPuntos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }



        private void CmbFigura_SelectedIndexChanged(object sender, EventArgs e)

        {
            // Aquí se puede implementar lógica adicional si se requiere cambiar 
            // el comportamiento según la figura seleccionada. 
        }

        // Método para dibujar la figura en el formulario 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (puntos != null && puntos.Length > 0)
            {
                // Dibujar un polígono uniendo los puntos almacenados en el vector 
                e.Graphics.DrawPolygon(Pens.Blue, puntos);
                // Dibujar cada punto con un pequeño círculo rojo 
                foreach (var punto in puntos)
                {
                    e.Graphics.FillEllipse(Brushes.Red, punto.X - 3, punto.Y - 3, 6, 6);
                }

            }

        }

        private void DibujarFigura(Graphics g, string figuraSeleccionada)
        {
            Pen lapiz = new Pen(Color.Blue, 2);
            int x = 200; // Coordenada X de inicio
            int y = 150; // Coordenada Y de inicio

            if (figuraSeleccionada == "Triángulo")
            {
                Point[] puntosTriangulo = {
            new Point(x, y + 100),
            new Point(x + 50, y),
            new Point(x + 100, y + 100)
        };
                g.DrawPolygon(lapiz, puntosTriangulo);
            }
            else if (figuraSeleccionada == "Cuadrado")
            {
                int lado = 100; // Lado del cuadrado
                g.DrawRectangle(lapiz, x, y, lado, lado); // Ancho y alto iguales
            }
            else if (figuraSeleccionada == "Círculo")
            {
                int radio = 100; // Radio
                g.DrawEllipse(lapiz, x, y, radio, radio); // Ancho y alto iguales
            }
        }

    }

}

