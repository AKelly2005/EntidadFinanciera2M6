using EntidadFinanciera2M6.Data;
using EntidadFinanciera2M6.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntidadFinanciera2M6
{
    //Formulario para solicitar el monto de una transferencia entre dos cuentas.
    public partial class TransferenciaForms : Form
    {
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private readonly CuentaService _CS;
        private readonly int _cuentaOrigenId;
        private readonly int _cuentaDestinoId;

        // Monto ingresado por el usuario para la transferencia.
        public decimal Monto { get; private set; }

        // Inicializa el formulario de transferencia inyectando el servicio de cuentas.
        public TransferenciaForms(
            CuentaService cuentaService,
            int cuentaOrigenId,
            int cuentaDestinoId)
        {
            InitializeComponent();

            _CS = cuentaService ?? throw new ArgumentNullException(nameof(cuentaService));
            _cuentaOrigenId = cuentaOrigenId;
            _cuentaDestinoId = cuentaDestinoId;

            _errorProvider.ContainerControl = this;

            CargarCuentas();
        }
        private void CargarCuentas()
        {
            var origen = _CS.ObtenerCuentaPorId(_cuentaOrigenId);
            var destino = _CS.ObtenerCuentaPorId(_cuentaDestinoId);

            lblCuentaOrigen.Text = $"Origen: {origen.Cliente.Nombre} - {origen.NumeroCuenta}";
            lblCuentaDestino.Text = $"Destino: {destino.Cliente.Nombre} - {destino.NumeroCuenta}";
            lblSaldo.Text = $"Saldo Disponible: {origen.Saldo:C}";
        }

        // Valida el monto ingresado y marca error si no es válido.
        private bool ValidarMonto()
        {
            _errorProvider.Clear();
            if (numMonto.Value <= 0)
            {
                _errorProvider.SetError(numMonto, "Debe ingresar un monto mayor que cero.");
                return false;
            }
            return true;
        }

        // Evento clic en “Aceptar”: valida el monto y cierra el formulario con OK.
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarMonto())
                return;

            Monto = numMonto.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        // Evento clic en “Cancelar”: cierra el formulario sin acción.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
