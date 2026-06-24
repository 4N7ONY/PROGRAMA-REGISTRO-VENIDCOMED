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

        // Variables de control de dinero
        double totalCajaDiaria = 0.0;
        double totalOrdenActual = 0.0; // ¡NUEVO! Para saber cuánto debe el cliente de ahorita

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbCategoria.Items.Add("Comidas");
            cmbCategoria.Items.Add("Bebidas");
            cmbCategoria.SelectedIndex = 0;

            // Configuración de la tabla maestra
            dgvVentas.ColumnCount = 6;
            dgvVentas.Columns[0].Name = "Fecha/Hora";
            dgvVentas.Columns[1].Name = "Cliente";
            dgvVentas.Columns[2].Name = "Categoría";
            dgvVentas.Columns[3].Name = "Ítem";
            dgvVentas.Columns[4].Name = "Cant.";
            dgvVentas.Columns[5].Name = "Total (S/.)";

            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.AllowUserToAddRows = false;
            dgvVentas.ReadOnly = true; // Para que no editen la tabla a mano
        }

        // EVENTO DEL RELOJ EN VIVO
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

            // Registrar en la tabla
            string fechaHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            dgvVentas.Rows.Add(fechaHora, txtCliente.Text, cmbCategoria.SelectedItem.ToString(), nombreItem, cantidad, totalVenta.ToString("F2"));

            // Actualizar la vista rápida del ticket en pantalla
            rtbTicket.Text += $"- {cantidad}x {nombreItem} (S/. {totalVenta:F2})\n";

            lblTotalCaja.Text = $"Caja Diaria: S/. {totalCajaDiaria:F2}";
            CalcularVuelto(); // Llama a la función del vuelto por si ya habían puesto billete
        }

        // ¡NUEVO! Calcula el vuelto cada vez que escribes en txtPago
        private void txtPago_TextChanged(object sender, EventArgs e)
        {
            CalcularVuelto();
        }

        private void CalcularVuelto()
        {
            if (double.TryParse(txtPago.Text, out double pagoCon))
            {
                double vuelto = pagoCon - totalOrdenActual;
                if (vuelto >= 0)
                    lblVuelto.Text = $"Vuelto: S/. {vuelto:F2}";
                else
                    lblVuelto.Text = "Falta dinero";
            }
            else
            {
                lblVuelto.Text = "Vuelto: S/. 0.00";
            }
        }

        // ¡NUEVO! Imprimir ticket individual en TXT
        private void btnImprimirTicket_Click(object sender, EventArgs e)
        {
            if (totalOrdenActual == 0)
            {
                MessageBox.Show("Falta cobrar");
                return;
            }

            // Matemática de impuestos
            double subtotal = totalOrdenActual / 1.18;
            double igv = totalOrdenActual - subtotal;

            string nombreArchivo = $"Ticket_{txtCliente.Text}_{DateTime.Now.ToString("HH-mm-ss")}.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(nombreArchivo, false, Encoding.UTF8))
                {
                    sw.WriteLine("==========================================");
                    sw.WriteLine("       RESTAURANTE 'VENID COMED'          ");
                    sw.WriteLine("==========================================");
                    sw.WriteLine($"Fecha y Hora : {DateTime.Now}");
                    sw.WriteLine($"Cliente      : {txtCliente.Text.ToUpper()}");
                    sw.WriteLine("------------------------------------------");
                    sw.WriteLine("DETALLE DE COMPRA:");
                    sw.WriteLine(rtbTicket.Text); // Imprime todo lo que se agregó a la orden
                    sw.WriteLine("------------------------------------------");
                    sw.WriteLine($"SUBTOTAL     : S/. {subtotal:F2}");
                    sw.WriteLine($"IGV (18%)    : S/. {igv:F2}");
                    sw.WriteLine($"TOTAL A PAGAR: S/. {totalOrdenActual:F2}");
                    sw.WriteLine("------------------------------------------");

                    if (double.TryParse(txtPago.Text, out double pagoCon))
                    {
                        sw.WriteLine($"PAGÓ CON     : S/. {pagoCon:F2}");
                        sw.WriteLine($"VUELTO       : S/. {(pagoCon - totalOrdenActual):F2}");
                    }

                    sw.WriteLine("========================================================");
                    sw.WriteLine("        ¡GRACIAS POR SU COMPRA - VENID COMED!           ");
                    sw.WriteLine("========================================================");
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
            rtbTicket.Clear();
            txtPago.Clear();
            lblVuelto.Text = "Vuelto: S/. 0.00";
            totalOrdenActual = 0; // Reiniciamos la deuda del cliente actual
            txtCliente.Focus();
        }

        // Exportar a Excel (CSV Mejorado)
        private void btnCerrarCaja_Click(object sender, EventArgs e)
        {
            if (dgvVentas.Rows.Count == 0)
            {
                MessageBox.Show("Caja vacía. ¡A vender más mañana!", "Cierre");
                return;
            }

            string fecha = DateTime.Now.ToString("dd-MM-yyyy");
            string rutaArchivo = $"Reporte_Avanzado_{fecha}.csv";

            try
            {
                // Le agregué un BOM de UTF8 para que Excel lea los tildes a la perfección
                using (StreamWriter sw = new StreamWriter(rutaArchivo, false, new UTF8Encoding(true)))
                {
                    // Encabezados limpios para que se conviertan en tabla fácil
                    sw.WriteLine("Fecha/Hora,Cliente,Categoria,Item,Cantidad,Total(S/.)");

                    foreach (DataGridViewRow row in dgvVentas.Rows)
                    {
                        sw.WriteLine($"{row.Cells[0].Value},{row.Cells[1].Value},{row.Cells[2].Value},{row.Cells[3].Value},{row.Cells[4].Value},{row.Cells[5].Value}");
                    }

                    sw.WriteLine(",,,,,");
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