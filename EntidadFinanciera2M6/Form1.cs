using EntidadFinanciera2M6.Data;
using Microsoft.EntityFrameworkCore;
using EntidadFinanciera2M6.Models;
using EntidadFinanciera2M6.Services;

namespace EntidadFinanciera2M6
{
    public partial class Form1 : Form
    {
        private EntidadFinancieraContext _db = new EntidadFinancieraContext();
        private CuentaService _CS;
        public Form1()
        {
            InitializeComponent();
            _CS = new CuentaService(_db);
            CargarDatos();
        }

        private void CargarDatos()
        {
            dgvClientes.DataSource = _CS.ObtenerClientesConCuentas();
            dgvCuentas.DataSource = _CS.ObtenerCuentasActivas();
        }

        private void btnAgregarCliente_Click(object sender, EventArgs e)
        {
            var form = new AgregarClienteForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _CS.AgregarCliente(form.NuevoCliente);
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar cliente: {ex.Message}");
                }
            }
        }

        private void btnAgregarCuenta_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un cliente primero.");
                return;
            }

            int clienteId = (int)dgvClientes.SelectedRows[0].Cells["ClienteId"].Value;
            var form = new AgregarCuetasForm(clienteId);

            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _CS.AgregarCuenta(form.NuevaCuenta);
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al agregar cuenta: {ex.Message}");
                }
            }
        }

        private void btnDesctivarCuenta_Click(object sender, EventArgs e)
        {
            if (dgvCuentas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una cuenta para desactivar.");
                return;
            }

            int cuentaId = (int)dgvCuentas.SelectedRows[0].Cells["CuentaId"].Value;

            try
            {
                _CS.DesactivarCuenta(cuentaId);
                CargarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al desactivar cuenta: {ex.Message}");
            }
        }

        private void btnTransferencia_Click(object sender, EventArgs e)
        {
            if (dgvCuentas.SelectedRows.Count != 2)
            {
                MessageBox.Show("Seleccione exactamente 2 cuentas.");
                return;
            }

            int cuentaOrigenId = (int)dgvCuentas.SelectedRows[0].Cells["CuentaId"].Value;
            int cuentaDestinoId = (int)dgvCuentas.SelectedRows[1].Cells["CuentaId"].Value;

            var form = new TransferenciaForms(_CS,cuentaOrigenId, cuentaDestinoId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _CS.RealizarTransferencia(cuentaOrigenId, cuentaDestinoId, form.Monto);
                    MessageBox.Show("Transferencia realizada con éxito.");
                    CargarDatos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la transferencia: {ex.Message}");
                }
            }
        }

        private void btnHistorialTransferencias_Click(object sender, EventArgs e)
        {
            var form = new Transacciones();
            form.ShowDialog();
        }
    }
}
