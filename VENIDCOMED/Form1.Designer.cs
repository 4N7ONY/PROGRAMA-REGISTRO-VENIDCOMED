using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VENIDCOMED
{
    partial class Form1
    {
        private IContainer components = null;

        // Controles usados en el formulario
        private ComboBox cmbCategoria;
        private ComboBox cmbProductos;
        private DataGridView dgvVentas;
        private Label lblReloj;
        private System.Windows.Forms.Timer timerReloj;
        private TextBox txtCliente;
        private NumericUpDown nudCantidad;
        private RichTextBox rtbTicket;
        private Label lblTotalCaja;
        private TextBox txtPago;
        private Label lblVuelto;
        private Button btnNuevaOrden;
        private Button btnImprimirTicket;
        private Button btnRegistrar;
        private Button btnCerrarCaja;

        /// <summary>
        ///  Clean up any resources being used.
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

        private void InitializeComponent()
        {
            components = new Container();
            this.cmbCategoria = new ComboBox();
            this.cmbProductos = new ComboBox();
            this.dgvVentas = new DataGridView();
            this.lblReloj = new Label();
            this.timerReloj = new System.Windows.Forms.Timer(components);
            this.txtCliente = new TextBox();
            this.nudCantidad = new NumericUpDown();
            this.rtbTicket = new RichTextBox();
            this.lblTotalCaja = new Label();
            this.txtPago = new TextBox();
            this.lblVuelto = new Label();
            this.btnNuevaOrden = new Button();
            this.btnImprimirTicket = new Button();
            this.btnRegistrar = new Button();
            this.btnCerrarCaja = new Button();

            ((ISupportInitialize)(this.dgvVentas)).BeginInit();
            ((ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.SuspendLayout();

            // cmbCategoria
            this.cmbCategoria.Location = new Point(12, 12);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new Size(150, 23);
            this.cmbCategoria.TabIndex = 0;
            this.cmbCategoria.SelectedIndexChanged += new EventHandler(this.cmbCategoria_SelectedIndexChanged);

            // cmbProductos
            this.cmbProductos.Location = new Point(168, 12);
            this.cmbProductos.Name = "cmbProductos";
            this.cmbProductos.Size = new Size(250, 23);
            this.cmbProductos.TabIndex = 1;

            // txtCliente
            this.txtCliente.Location = new Point(12, 42);
            this.txtCliente.Name = "txtCliente";
            this.txtCliente.PlaceholderText = "Cliente";
            this.txtCliente.Size = new Size(200, 23);

            // nudCantidad
            this.nudCantidad.Location = new Point(218, 42);
            this.nudCantidad.Minimum = 1;
            this.nudCantidad.Maximum = 1000;
            this.nudCantidad.Value = 1;
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Size = new Size(60, 23);

            // btnRegistrar
            this.btnRegistrar.Location = new Point(288, 42);
            this.btnRegistrar.Name = "btnRegistrar";
            this.btnRegistrar.Size = new Size(130, 23);
            this.btnRegistrar.Text = "Registrar";
            this.btnRegistrar.Click += new EventHandler(this.btnRegistrar_Click);

            // rtbTicket
            this.rtbTicket.Location = new Point(12, 80);
            this.rtbTicket.Name = "rtbTicket";
            this.rtbTicket.Size = new Size(406, 200);

            // dgvVentas
            this.dgvVentas.Location = new Point(424, 12);
            this.dgvVentas.Name = "dgvVentas";
            this.dgvVentas.Size = new Size(480, 300);
            this.dgvVentas.AllowUserToAddRows = false;

            // lblTotalCaja
            this.lblTotalCaja.Location = new Point(12, 290);
            this.lblTotalCaja.Name = "lblTotalCaja";
            this.lblTotalCaja.Size = new Size(300, 23);
            this.lblTotalCaja.Text = "Caja Diaria: S/. 0.00";

            // txtPago
            this.txtPago.Location = new Point(12, 320);
            this.txtPago.Name = "txtPago";
            this.txtPago.PlaceholderText = "Pago";
            this.txtPago.Size = new Size(100, 23);
            this.txtPago.TextChanged += new EventHandler(this.txtPago_TextChanged);

            // lblVuelto
            this.lblVuelto.Location = new Point(118, 320);
            this.lblVuelto.Name = "lblVuelto";
            this.lblVuelto.Size = new Size(200, 23);
            this.lblVuelto.Text = "Vuelto: S/. 0.00";

            // lblReloj
            this.lblReloj.Location = new Point(12, 350);
            this.lblReloj.Name = "lblReloj";
            this.lblReloj.Size = new Size(200, 23);
            this.lblReloj.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            // timerReloj
            this.timerReloj.Interval = 1000;
            this.timerReloj.Tick += new EventHandler(this.timerReloj_Tick);
            this.timerReloj.Enabled = true;

            // btnNuevaOrden
            this.btnNuevaOrden.Location = new Point(12, 380);
            this.btnNuevaOrden.Name = "btnNuevaOrden";
            this.btnNuevaOrden.Size = new Size(120, 23);
            this.btnNuevaOrden.Text = "Nueva Orden";
            this.btnNuevaOrden.Click += new EventHandler(this.btnNuevaOrden_Click);

            // btnImprimirTicket
            this.btnImprimirTicket.Location = new Point(138, 380);
            this.btnImprimirTicket.Name = "btnImprimirTicket";
            this.btnImprimirTicket.Size = new Size(140, 23);
            this.btnImprimirTicket.Text = "Imprimir Ticket";
            this.btnImprimirTicket.Click += new EventHandler(this.btnImprimirTicket_Click);

            // btnCerrarCaja
            this.btnCerrarCaja.Location = new Point(284, 380);
            this.btnCerrarCaja.Name = "btnCerrarCaja";
            this.btnCerrarCaja.Size = new Size(134, 23);
            this.btnCerrarCaja.Text = "Cerrar Caja";
            this.btnCerrarCaja.Click += new EventHandler(this.btnCerrarCaja_Click);

            // Form1
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(920, 420);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.cmbProductos);
            this.Controls.Add(this.txtCliente);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(this.btnRegistrar);
            this.Controls.Add(this.rtbTicket);
            this.Controls.Add(this.dgvVentas);
            this.Controls.Add(this.lblTotalCaja);
            this.Controls.Add(this.txtPago);
            this.Controls.Add(this.lblVuelto);
            this.Controls.Add(this.lblReloj);
            this.Controls.Add(this.btnNuevaOrden);
            this.Controls.Add(this.btnImprimirTicket);
            this.Controls.Add(this.btnCerrarCaja);
            this.Name = "Form1";
            this.Text = "VENID COMED";
            this.Load += new EventHandler(this.Form1_Load);

            ((ISupportInitialize)(this.dgvVentas)).EndInit();
            ((ISupportInitialize)(this.nudCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
