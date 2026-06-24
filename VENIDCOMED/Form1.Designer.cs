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
        private RichTextBox rtbTicketPreview;
        private Label lblTotalCaja;
        private TextBox txtPago;
        private ComboBox cmbMetodoPago;
        private Label lblMetodoPago;
        private Label lblVuelto;
        private Button btnNuevaOrden;
        private Button btnImprimirTicket;
        private Button btnRegistrar;
        private Button btnCerrarCaja;
        private DataGridView dgvPedidos;
        private Label lblPrecioUnitario;
        private Label lblTotalPedido;
        private Label label1;

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
            cmbCategoria = new ComboBox();
            cmbProductos = new ComboBox();
            cmbMetodoPago = new ComboBox();
            lblMetodoPago = new Label();
            dgvVentas = new DataGridView();
            lblReloj = new Label();
            timerReloj = new System.Windows.Forms.Timer(components);
            txtCliente = new TextBox();
            nudCantidad = new NumericUpDown();
            dgvPedidos = new DataGridView();
            lblTotalCaja = new Label();
            txtPago = new TextBox();
            lblVuelto = new Label();
            btnNuevaOrden = new Button();
            btnImprimirTicket = new Button();
            btnRegistrar = new Button();
            btnCerrarCaja = new Button();
            rtbTicketPreview = new RichTextBox();
            label1 = new Label();
            lblPrecioUnitario = new Label();
            lblTotalPedido = new Label();
            ((ISupportInitialize)dgvVentas).BeginInit();
            ((ISupportInitialize)nudCantidad).BeginInit();
            ((ISupportInitialize)dgvPedidos).BeginInit();
            SuspendLayout();
            // 
            // cmbCategoria
            // 
            cmbCategoria.Location = new Point(12, 12);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(150, 23);
            cmbCategoria.TabIndex = 0;
            cmbCategoria.SelectedIndexChanged += cmbCategoria_SelectedIndexChanged;
            // 
            // cmbProductos
            // 
            cmbProductos.Location = new Point(168, 12);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(250, 23);
            cmbProductos.TabIndex = 1;
            cmbProductos.SelectedIndexChanged += cmbProductos_SelectedIndexChanged;
            // 
            // cmbMetodoPago
            // 
            cmbMetodoPago.Items.AddRange(new object[] { "Efectivo", "Yape", "Plin", "Transferencia" });
            cmbMetodoPago.Location = new Point(118, 72);
            cmbMetodoPago.Name = "cmbMetodoPago";
            cmbMetodoPago.Size = new Size(150, 23);
            cmbMetodoPago.TabIndex = 16;
            // 
            // lblMetodoPago
            // 
            lblMetodoPago.Location = new Point(12, 72);
            lblMetodoPago.Name = "lblMetodoPago";
            lblMetodoPago.Size = new Size(100, 23);
            lblMetodoPago.TabIndex = 15;
            lblMetodoPago.Text = "Método Pago:";
            // 
            // dgvVentas
            // 
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.BackgroundColor = Color.LightCyan;
            dgvVentas.Location = new Point(424, 42);
            dgvVentas.Name = "dgvVentas";
            dgvVentas.Size = new Size(480, 310);
            dgvVentas.TabIndex = 6;
            // 
            // lblReloj
            // 
            lblReloj.Location = new Point(919, 454);
            lblReloj.Name = "lblReloj";
            lblReloj.Size = new Size(200, 23);
            lblReloj.TabIndex = 10;
            lblReloj.Text = "23/06/2026 21:20:37";
            // 
            // timerReloj
            // 
            timerReloj.Enabled = true;
            timerReloj.Interval = 1000;
            timerReloj.Tick += timerReloj_Tick;
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(12, 42);
            txtCliente.Name = "txtCliente";
            txtCliente.PlaceholderText = "Cliente";
            txtCliente.Size = new Size(200, 23);
            txtCliente.TabIndex = 2;
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(218, 42);
            nudCantidad.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(60, 23);
            nudCantidad.TabIndex = 3;
            nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // dgvPedidos
            // 
            dgvPedidos.BackgroundColor = Color.White;
            dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPedidos.Location = new Point(12, 127);
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.RowHeadersVisible = false;
            dgvPedidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPedidos.Size = new Size(406, 247);
            dgvPedidos.TabIndex = 5;
            dgvPedidos.CellContentClick += dgvPedidos_CellContentClick;
            // 
            // lblTotalCaja
            // 
            lblTotalCaja.Location = new Point(424, 355);
            lblTotalCaja.Name = "lblTotalCaja";
            lblTotalCaja.Size = new Size(300, 23);
            lblTotalCaja.TabIndex = 7;
            lblTotalCaja.Text = "Caja Diaria: S/. 0.00";
            // 
            // txtPago
            // 
            txtPago.Location = new Point(12, 98);
            txtPago.Name = "txtPago";
            txtPago.PlaceholderText = "Pago";
            txtPago.Size = new Size(100, 23);
            txtPago.TabIndex = 8;
            txtPago.TextChanged += txtPago_TextChanged;
            // 
            // lblVuelto
            // 
            lblVuelto.Location = new Point(118, 98);
            lblVuelto.Name = "lblVuelto";
            lblVuelto.Size = new Size(200, 23);
            lblVuelto.TabIndex = 9;
            lblVuelto.Text = "Vuelto: S/. 0.00";
            // 
            // btnNuevaOrden
            // 
            btnNuevaOrden.Location = new Point(12, 380);
            btnNuevaOrden.Name = "btnNuevaOrden";
            btnNuevaOrden.Size = new Size(120, 23);
            btnNuevaOrden.TabIndex = 11;
            btnNuevaOrden.Text = "Nueva Orden";
            btnNuevaOrden.Click += btnNuevaOrden_Click;
            // 
            // btnImprimirTicket
            // 
            btnImprimirTicket.Location = new Point(138, 380);
            btnImprimirTicket.Name = "btnImprimirTicket";
            btnImprimirTicket.Size = new Size(140, 23);
            btnImprimirTicket.TabIndex = 12;
            btnImprimirTicket.Text = "Imprimir Ticket";
            btnImprimirTicket.Click += btnImprimirTicket_Click;
            // 
            // btnRegistrar
            // 
            btnRegistrar.Location = new Point(288, 42);
            btnRegistrar.Name = "btnRegistrar";
            btnRegistrar.Size = new Size(130, 23);
            btnRegistrar.TabIndex = 4;
            btnRegistrar.Text = "Registrar";
            btnRegistrar.Click += btnRegistrar_Click;
            // 
            // btnCerrarCaja
            // 
            btnCerrarCaja.Location = new Point(284, 380);
            btnCerrarCaja.Name = "btnCerrarCaja";
            btnCerrarCaja.Size = new Size(134, 23);
            btnCerrarCaja.TabIndex = 13;
            btnCerrarCaja.Text = "Cerrar Caja";
            btnCerrarCaja.Click += btnCerrarCaja_Click;
            // 
            // rtbTicketPreview
            // 
            rtbTicketPreview.BackColor = SystemColors.Control;
            rtbTicketPreview.Location = new Point(919, 12);
            rtbTicketPreview.Name = "rtbTicketPreview";
            rtbTicketPreview.ReadOnly = true;
            rtbTicketPreview.Size = new Size(480, 439);
            rtbTicketPreview.TabIndex = 14;
            rtbTicketPreview.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(541, 9);
            label1.Name = "label1";
            label1.Size = new Size(212, 30);
            label1.TabIndex = 17;
            label1.Text = "REGISTROS DEL DIA";
            // 
            // lblPrecioUnitario
            // 
            lblPrecioUnitario.AutoSize = true;
            lblPrecioUnitario.Location = new Point(424, 355);
            lblPrecioUnitario.Name = "lblPrecioUnitario";
            lblPrecioUnitario.Size = new Size(88, 15);
            lblPrecioUnitario.TabIndex = 18;
            lblPrecioUnitario.Text = "Precio Unitario:";
            // 
            // lblTotalPedido
            // 
            lblTotalPedido.AutoSize = true;
            lblTotalPedido.Location = new Point(288, 98);
            lblTotalPedido.Name = "lblTotalPedido";
            lblTotalPedido.Size = new Size(76, 15);
            lblTotalPedido.TabIndex = 19;
            lblTotalPedido.Text = "Total Pedido:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(1476, 604);
            Controls.Add(label1);
            Controls.Add(cmbCategoria);
            Controls.Add(cmbProductos);
            Controls.Add(lblMetodoPago);
            Controls.Add(cmbMetodoPago);
            Controls.Add(txtCliente);
            Controls.Add(nudCantidad);
            Controls.Add(btnRegistrar);
            Controls.Add(dgvVentas);
            Controls.Add(dgvPedidos);
            Controls.Add(rtbTicketPreview);
            Controls.Add(lblTotalCaja);
            Controls.Add(txtPago);
            Controls.Add(lblVuelto);
            Controls.Add(lblReloj);
            Controls.Add(btnNuevaOrden);
            Controls.Add(btnImprimirTicket);
            Controls.Add(btnCerrarCaja);
            Controls.Add(lblPrecioUnitario);
            Controls.Add(lblTotalPedido);
            Name = "Form1";
            Text = "VENID COMED";
            Load += Form1_Load;
            ((ISupportInitialize)dgvVentas).EndInit();
            ((ISupportInitialize)nudCantidad).EndInit();
            ((ISupportInitialize)dgvPedidos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
