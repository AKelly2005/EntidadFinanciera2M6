using EntidadFinanciera2M6.Data;
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
using EntidadFinanciera2M6.Models;

namespace EntidadFinanciera2M6
{

    public partial class Transacciones : Form
    {
        private readonly EntidadFinancieraContext ef;
        public Transacciones()
        {
            InitializeComponent();
            ef = new EntidadFinancieraContext();
            CargarTransacciones();
           
        }

        private void CargarTransacciones()
        {
            dgvTransacciones.DataSource = ef.Transacciones.ToList();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvTransacciones.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione al menos una transacción para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show("¿Está seguro que desea eliminar las transacciones seleccionadas?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
                return;

            try
            {
                foreach (DataGridViewRow fila in dgvTransacciones.SelectedRows)
                {
                    int transaccionId = Convert.ToInt32(fila.Cells["TransaccionId"].Value);
                    var transaccion = ef.Transacciones.Find(transaccionId);
                    if (transaccion != null)
                    {
                        ef.Transacciones.Remove(transaccion);
                    }
                }

                ef.SaveChanges();
                MessageBox.Show("Transacciones eliminadas correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarTransacciones();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar transacciones: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

