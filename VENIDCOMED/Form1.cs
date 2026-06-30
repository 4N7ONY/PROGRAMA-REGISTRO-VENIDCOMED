using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace VENIDCOMED
{
    public partial class Form1 : Form
    {
        // Catálogo de productos
        string[] nombresComidas = { "Chicharrón Mixto", "Chicharrón de Chancho", "Chicharrón de Hongo", "Ceviche de Hongo", "Plato GRANJA PORCON", "Lomo saltado de res a lo POBRE", "Trucha a la plancha", "Trucha a la plancha con hongo", "Milanesa de Pollo", "1/4 Cuy frito", "Cuy frito entero", "Parrilla de Res", "Parrilla de Alpaca" };
        double[] preciosComidas = { 25.00, 20.00, 20.00, 20.00, 45.00, 25.00, 25.00, 30.00, 20.00, 25.00, 90.00, 25.00, 25.00 };

        string[] nombresBebidas = { "Coca cola 500ml", "Coca cola 1L", "Inka cola 500ml", "Inka cola 1L", "Agua 500ml", "Panizara (Jarra) 1L", "Jugo de Frambuesa 1L" };
        double[] preciosBebidas = { 4.00, 10.00, 4.00, 10.00, 2.00, 10.00, 10.00 };

        // Variables de control
        double totalCajaDiaria = 0.0;
        double totalOrdenActual = 0.0; //Para saber cuánto debe el cliente

        public Form1()
        {
            InitializeComponent();
        }

        // --- Login simple en memoria ---
        private bool MostrarLogin()
        {
            using (Form login = new Form())
            {
                login.StartPosition = FormStartPosition.CenterParent;
                login.FormBorderStyle = FormBorderStyle.FixedDialog;
                login.ClientSize = new System.Drawing.Size(300, 160);
                login.Text = "Login";

                Label lblUser = new Label() { Left = 10, Top = 10, Text = "Usuario", Width = 80 };
                TextBox txtUser = new TextBox() { Left = 100, Top = 10, Width = 170 };

                Label lblPass = new Label() { Left = 10, Top = 40, Text = "Contraseña", Width = 80 };
                TextBox txtPass = new TextBox() { Left = 100, Top = 40, Width = 170, UseSystemPasswordChar = true };

                Button btnOk = new Button() { Text = "Entrar", Left = 100, Width = 80, Top = 80, DialogResult = DialogResult.OK };
                Button btnCancel = new Button() { Text = "Cancelar", Left = 190, Width = 80, Top = 80, DialogResult = DialogResult.Cancel };

                login.Controls.Add(lblUser);
                login.Controls.Add(txtUser);
                login.Controls.Add(lblPass);
                login.Controls.Add(txtPass);
                login.Controls.Add(btnOk);
                login.Controls.Add(btnCancel);

                login.AcceptButton = btnOk;
                login.CancelButton = btnCancel;

                if (login.ShowDialog(this) == DialogResult.OK)
                {
                    // Credenciales en memoria: admin / 1234
                    string user = txtUser.Text.Trim();
                    string pass = txtPass.Text;
                    return (user == "admin" && pass == "1234");
                }

                return false;
            }
        }

        private bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            // Requiere al menos 2 caracteres
            if (name.Trim().Length < 2) return false;

            foreach (char c in name)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '\'') continue;
                return false;
            }

            return true;
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir control chars, letras, espacios, guión y apóstrofe
            if (char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || char.IsLetter(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == '\'')
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void cmbMetodoPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMetodoPago == null) return;

            string metodo = cmbMetodoPago.SelectedItem?.ToString() ?? "Efectivo";
            if (metodo == "Efectivo")
            {
                txtPago.Enabled = true;
                txtPago.Text = string.Empty;
                txtPago.Focus();
            }
            else
            {
                // Para métodos no efectivos, marcar el pago como el total (no editable)
                txtPago.Enabled = false;
                txtPago.Text = totalOrdenActual.ToString("F2");
                CalcularVuelto();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCategoria.Items.Add("Comidas");
            cmbCategoria.Items.Add("Bebidas");
            cmbCategoria.SelectedIndex = 0;

            // Configurar dgvPedidos
            dgvPedidos.ColumnCount = 5;
            dgvPedidos.Columns[0].Name = "Ítem";
            dgvPedidos.Columns[1].Name = "Cant.";
            dgvPedidos.Columns[2].Name = "P. Unitario";
            dgvPedidos.Columns[3].Name = "Total";
            dgvPedidos.Columns[4].Name = "RowIndex";
            dgvPedidos.Columns[4].Visible = false; // Columna oculta para guardar índice
            
            // Hacer las columnas de datos de solo lectura
            for (int i = 0; i < 4; i++)
            {
                dgvPedidos.Columns[i].ReadOnly = true;
            }
            
            // Agregar columna de botones
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.HeaderText = "Eliminar";
            btnEliminar.Text = "🗑️";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.ReadOnly = false; // Permitir interacción con el botón
            dgvPedidos.Columns.Add(btnEliminar);
            
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPedidos.AllowUserToAddRows = false;
            dgvPedidos.CellContentClick += dgvPedidos_CellContentClick;

            dgvVentas.ColumnCount = 6;
            dgvVentas.Columns[0].Name = "Fecha/Hora";
            dgvVentas.Columns[1].Name = "Cliente";
            dgvVentas.Columns[2].Name = "Categoría";
            dgvVentas.Columns[3].Name = "Ítem";
            dgvVentas.Columns[4].Name = "Cant.";
            dgvVentas.Columns[5].Name = "Total (S/.)";

            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.ReadOnly = true;
        }

        // EVENTO DEL RELO
        private void timerReloj_Tick(object sender, EventArgs e)
        {
            lblReloj.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbProductos.Items.Clear();
            if (cmbCategoria.SelectedItem.ToString() == "Comidas")
                cmbProductos.Items.AddRange(nombresComidas);
            else
                cmbProductos.Items.AddRange(nombresBebidas);

            if (cmbProductos.Items.Count > 0) cmbProductos.SelectedIndex = 0;
            ActualizarPrecioUnitario();
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarPrecioUnitario();
        }

        private void ActualizarPrecioUnitario()
        {
            int index = cmbProductos.SelectedIndex;
            if (index < 0) return;

            double precio = cmbCategoria.SelectedItem.ToString() == "Comidas" ? preciosComidas[index] : preciosBebidas[index];
            lblPrecioUnitario.Text = $"Precio Unitario: S/. {precio:F2}";
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCliente.Text))
            {
                MessageBox.Show("Falta el nombre del cliente");
                return;
            }

            int index = cmbProductos.SelectedIndex;
            int cantidad = (int)nudCantidad.Value;
            if (index < 0 || cantidad <= 0) return;

            string nombreItem = cmbCategoria.SelectedItem.ToString() == "Comidas" ? nombresComidas[index] : nombresBebidas[index];
            double precioItem = cmbCategoria.SelectedItem.ToString() == "Comidas" ? preciosComidas[index] : preciosBebidas[index];

            double totalVenta = cantidad * precioItem;

            // Sumamos al total del día y al total de la orden actual
            totalCajaDiaria += totalVenta;
            totalOrdenActual += totalVenta;

            // Registrar en la tabla de ventas del día
            string fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            dgvVentas.Rows.Add(fechaHora, txtCliente.Text, cmbCategoria.SelectedItem.ToString(), nombreItem, cantidad, totalVenta.ToString("F2"));

            // Agregar a dgvPedidos para esta orden
            int rowIndex = dgvPedidos.Rows.Add(nombreItem, cantidad, precioItem.ToString("F2"), totalVenta.ToString("F2"));
            dgvPedidos.Rows[rowIndex].Cells[4].Value = rowIndex; // Guardar el índice

            // NO limpiar campos después de registrar, mantener el cliente
            nudCantidad.Value = 1;
            txtPago.Clear();
            lblVuelto.Text = "Vuelto: S/. 0.00";

            // Actualizar vistas
            ActualizarVistaPrevia();
            lblTotalCaja.Text = $"Caja Diaria: S/. {totalCajaDiaria:F2}";
            lblTotalPedido.Text = $"Total Pedido: S/. {totalOrdenActual:F2}";
            CalcularVuelto();
        }

        // Manejar clic en botón Eliminar
        private void dgvPedidos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.RowIndex >= 0) // Columna de Eliminar (índice 5 es la columna de botones)
            {
                // Obtener el total de la fila que se va a eliminar
                if (e.RowIndex < dgvPedidos.Rows.Count)
                {
                    double totalFila = double.Parse(dgvPedidos.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "0");
                    
                    // Restar del total actual y del total diario
                    totalOrdenActual -= totalFila;
                    totalCajaDiaria -= totalFila;

                    // Eliminar la fila
                    dgvPedidos.Rows.RemoveAt(e.RowIndex);

                    // Actualizar vistas
                    ActualizarVistaPrevia();
                    lblTotalCaja.Text = $"Caja Diaria: S/. {totalCajaDiaria:F2}";
                    lblTotalPedido.Text = $"Total Pedido: S/. {totalOrdenActual:F2}";
                    CalcularVuelto();
                }
            }
        }

        //Calcula el vuelto cada vez que escribes en txtPago
        private void txtPago_TextChanged(object sender, EventArgs e)
        {
            CalcularVuelto();
            ActualizarVistaPrevia();
        }

        private void CalcularVuelto()
        {
            // Determinar el método de pago (por defecto "Efectivo" si no hay selección)
            string metodo = (cmbMetodoPago != null && cmbMetodoPago.SelectedItem != null) ? cmbMetodoPago.SelectedItem.ToString() : "Efectivo";

            if (metodo != "Efectivo")
            {
                // Para métodos no efectivos no se utiliza el campo de efectivo ni hay vuelto
                lblVuelto.Text = "Método no efectivo - sin vuelto";
                return;
            }

            // Si es efectivo, mostrar cuánto pagó y cuánto es el vuelto, o mensajes claros si falta/monto no ingresado
            if (double.TryParse(txtPago.Text, out double pagoCon))
            {
                double vuelto = pagoCon - totalOrdenActual;
                if (vuelto >= 0)
                {
                    lblVuelto.Text = $"Pagó: S/. {pagoCon:F2} | Vuelto: S/. {vuelto:F2}";
                }
                else
                {
                    lblVuelto.Text = $"Faltan: S/. {Math.Abs(vuelto):F2}";
                }
            }
            else
            {
                lblVuelto.Text = "Ingrese monto pagado";
            }
        }

        // Generar contenido del ticket
        private string GenerarContenidoTicket()
        {
            double subtotal = totalOrdenActual / 1.18;
            double igv = totalOrdenActual - subtotal;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("==========================================");
            sb.AppendLine("       RESTAURANTE 'VENID COMED'          ");
            sb.AppendLine("==========================================");
            sb.AppendLine($"Fecha y Hora : {DateTime.Now}");
            sb.AppendLine($"Cliente      : {txtCliente.Text.ToUpper()}");
            sb.AppendLine("------------------------------------------");
            sb.AppendLine("DETALLE DE COMPRA:");
            
            foreach (DataGridViewRow row in dgvPedidos.Rows)
            {
                string item = row.Cells[0].Value?.ToString() ?? "";
                string cantidad = row.Cells[1].Value?.ToString() ?? "0";
                string unitario = row.Cells[2].Value?.ToString() ?? "0";
                string total = row.Cells[3].Value?.ToString() ?? "0";
                sb.AppendLine($"- {cantidad}x {item} (S/. {total})");
            }

            sb.AppendLine($"Metodo Pago  : {(cmbMetodoPago?.SelectedItem?.ToString() ?? "N/A")}");
            sb.AppendLine("------------------------------------------");
            sb.AppendLine($"SUBTOTAL     : S/. {subtotal:F2}");
            sb.AppendLine($"IGV (18%)    : S/. {igv:F2}");
            sb.AppendLine($"TOTAL A PAGAR: S/. {totalOrdenActual:F2}");
            sb.AppendLine("------------------------------------------");

            if (double.TryParse(txtPago.Text, out double pagoCon))
            {
                sb.AppendLine($"PAGÓ CON     : S/. {pagoCon:F2}");
                if ((cmbMetodoPago?.SelectedItem?.ToString() ?? "Efectivo") == "Efectivo")
                    sb.AppendLine($"VUELTO       : S/. {(pagoCon - totalOrdenActual):F2}");
                else
                    sb.AppendLine($"VUELTO       : S/. 0.00 (Pago no efectivo)");
            }

            sb.AppendLine("========================================================");
            sb.AppendLine("        ¡GRACIAS POR SU COMPRA - VENID COMED!           ");
            sb.AppendLine("========================================================");

            return sb.ToString();
        }

        // Actualizar vista previa
        private void ActualizarVistaPrevia()
        {
            if (rtbTicketPreview != null && totalOrdenActual > 0)
            {
                rtbTicketPreview.Text = GenerarContenidoTicket();
            }
        }

        //Imprimir ticket individual en TXT
        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {
            if (totalOrdenActual == 0)
            {
                MessageBox.Show("Falta cobrar");
                return;
            }

            // Asegurar nombre de archivo válido
            string clienteSafe = string.IsNullOrWhiteSpace(txtCliente.Text) ? "CLIENTE" : txtCliente.Text;
            foreach (char c in Path.GetInvalidFileNameChars()) clienteSafe = clienteSafe.Replace(c, '_');
            string nombreArchivo = $"Ticket_{clienteSafe}_{DateTime.Now.ToString("HH-mm-ss")}.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(nombreArchivo, false, Encoding.UTF8))
                {
                    sw.Write(GenerarContenidoTicket());
                }

                MessageBox.Show($"Ticket generado\nSe guardó como: {nombreArchivo}", "Éxito");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el ticket: " + ex.Message, "Error");
            }
        }

        // Limpiar para el siguiente cliente
        private void btnNuevaOrden_Click(object sender, EventArgs e)
        {
            txtCliente.Clear();
            nudCantidad.Value = 1;
            dgvPedidos.Rows.Clear();
            txtPago.Clear();
            lblVuelto.Text = "Vuelto: S/. 0.00";
            lblTotalPedido.Text = "Total Pedido: S/. 0.00";
            totalOrdenActual = 0; // Reiniciamos la deuda del cliente actual
            if (rtbTicketPreview != null) rtbTicketPreview.Clear();
            txtCliente.Focus();
        }

        // Exportar a Excel (CSV Mejorado)
        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count == 0)
            {
                MessageBox.Show("Caja vacía");
                return;
            }

            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            string rutaArchivo = $"Reporte{fecha}.csv";

            try
            {
                // Se agreguo un BOM de UTF8 para que Excel lea los tildes a la perfección
                using (StreamWriter sw = new StreamWriter(rutaArchivo, false, new UTF8Encoding(true)))
                {
                    // Encabezados limpios para que se conviertan en tabla fácil
                    sw.WriteLine("Fecha/Hora,Cliente,Categoria,Item,Cantidad,Total(S/.)");

                    foreach (DataGridViewRow row in dgvVentas.Rows)
                    {
                        sw.WriteLine($"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value},{row.Cells[4].Value},{row.Cells[5].Value}");
                    }

                    sw.WriteLine(",,,,,,");
                    sw.WriteLine($",,,,TOTAL RECAUDADO DEL DIA,{totalCajaDiaria:F2}");
                }

                MessageBox.Show($"Caja cerrada\nData exportada a: {rutaArchivo}\nCierre Exitoso");

                // Reseto total
                dgvVentas.Rows.Clear();
                btnNuevaOrden.PerformClick();
                totalCajaDiaria = 0;
                lblTotalCaja.Text = "Caja Diaria: S/. 0.00";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fatal al guardar: " + ex.Message, "Error");
            }
        }
    }
}
