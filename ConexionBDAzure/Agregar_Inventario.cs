using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConexionBDAzure
{
    public partial class Agregar_Inventario : Form
    {
        private Mod_Inventario Inv;
        private MainBD main = new MainBD();

        public Agregar_Inventario()
        {
            InitializeComponent();
            cmbArea.DataSource = new DAO_Area().GetAll();
            cmbArea.DisplayMember = "Nombre";
            cmbTipoAdquisicion.SelectedIndex = 0;
            txtid.Text = "NEW";
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtColor.Text == "" || txtDescripcion.Text == "" || txtNombreCorto.Text == "" || txtObservaciones.Text == "" || txtSerie.Text == "")
            {
                MessageBox.Show("Favor de llenar todos los campos","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                DAO_Inventario dao = new DAO_Inventario();
                    Inv = dao.GetAll().Last();
                    Inv.NombreCorto = txtNombreCorto.Text;
                    Inv.Descripcion = txtDescripcion.Text;
                    Inv.Serie = txtSerie.Text;
                    Inv.Color = txtColor.Text;
                    Inv.FechaAdquision = dtpFechaAdquisicion.Value.ToString("yyyy-MM-dd");
                    Inv.TipoAdquision = cmbTipoAdquisicion.SelectedItem.ToString();
                    Inv.observaciones = txtObservaciones.Text;
                    Inv.Area_id = cmbArea.SelectedIndex + 1;
                if (dao.Insert(Inv))
                {
                    MessageBox.Show("Se ha insertado correctamente", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    main.Show();

                }
                else
                {
                    MessageBox.Show("La Incercion Fallo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Estas seguro de Salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                main.Show();
            }
            else if (dialogResult == DialogResult.No)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("¿Estas seguro de Salir?", "SALIR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                main.Show();
            }
            else if (dialogResult == DialogResult.No)
            {}
        }
    }
}
