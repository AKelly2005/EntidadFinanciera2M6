using EntidadFinanciera2M6.Models;
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
    //Formulario para agregar una nueva cuenta asociada a un cliente.
    public partial class AgregarCuentasForm : Form
    {
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private readonly int _clienteId;

        //Cuenta creada tras aceptar el formulario.
        public Cuenta NuevaCuenta { get; private set; }
        private int _cienteId;
        public AgregarCuentasForm(int clienteId)
        {
            InitializeComponent();
            _cienteId = clienteId;
            _errorProvider.ContainerControl = this;
        }

        // Valida los campos requeridos del formulario.
        private bool ValidarCampos()
        {
            bool esValido = true;
            _errorProvider.Clear();

            var numero = txtNumCuenta.Text?.Trim();
            if (string.IsNullOrEmpty(numero))
            {
                _errorProvider.SetError(txtNumCuenta, "El número de cuenta es obligatorio.");
                esValido = false;
            }

            return esValido;
        }

        // Crea una instancia de cuenta con los valores del formulario.
        private void CrearCuenta()
        {
            NuevaCuenta = new Cuenta
            {
                NumeroCuenta = txtNumCuenta.Text.Trim(),
                Saldo = numSaldoInicial.Value,
                ClienteId = _clienteId,
                Activa = true
            };
        }

        // Evento clic en Aceptar: valida y crea la cuenta.
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            CrearCuenta();

            DialogResult = DialogResult.OK;
            Close();
        }

       
        //Cancela la operación.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel; 
            Close();
        }
    }

}

