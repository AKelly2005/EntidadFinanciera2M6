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
    /// Formulario para capturar y validar datos de un nuevo cliente.
    public partial class AgregarClienteForm : Form
    {
        private readonly ErrorProvider _errorProvider = new ErrorProvider();
        private const string MensajeRequerido = "Este campo es obligatorio.";

        /// El cliente creado tras la validación exitosa del formulario.
        public Cliente NuevoCliente { get; private set; }
        public AgregarClienteForm()
        {
            InitializeComponent();
            _errorProvider.ContainerControl = this;

        }

        /// Valida los campos del formulario y marca errores si es necesario.
        /// True si todos los campos son válidos; false en caso contrario.
        private bool ValidarCampos()
        {
            bool esValido = true;
            _errorProvider.Clear();

            // Validar Nombre
            var nombre = txtNombre.Text?.Trim();
            if (string.IsNullOrEmpty(nombre))
            {
                _errorProvider.SetError(txtNombre, MensajeRequerido);
                esValido = false;
            }

            // Validar Identificación
            var identificacion = txtIdentificacion.Text?.Trim();
            if (string.IsNullOrEmpty(identificacion))
            {
                _errorProvider.SetError(txtIdentificacion, MensajeRequerido);
                esValido = false;
            }

            return esValido;
        }

        /// Evento clic en “Aceptar”: valida, crea el objeto Cliente y cierra el formulario.
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            NuevoCliente = new Cliente
            {
                Nombre = txtNombre.Text.Trim(),
                Identificacion = txtIdentificacion.Text.Trim()
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        /// Cancela la operación y cierra el formulario sin crear cliente.
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
