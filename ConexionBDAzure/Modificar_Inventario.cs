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
    public partial class ModInv : Form
    {
        private Mod_Inventario Inv;
        private MainBD main = new MainBD();
        private DAO_Inventario dao = new DAO_Inventario();

        public ModInv(Mod_Inventario InventarioModificar)
        {
            InitializeComponent();
            this.Inv = InventarioModificar;

            cmbArea.DataSource = new DAO_Area().GetAll();
            cmbArea.DisplayMember = "Nombre";
            CargarInfo();
        }

        private void CargarInfo() {
            txtid.Text = Inv.IdInventario + "";
            txtColor.Text = Inv.Color;
            txtDescripcion.Text = Inv.Descripcion;
            txtNombreCorto.Text = Inv.NombreCorto;
            txtObservaciones.Text = Inv.observaciones;
            txtSerie.Text = Inv.Serie;
            dtpFechaAdquisicion.Value = Convert.ToDateTime(Inv.FechaAdquision);
            cmbArea.SelectedItem = Inv.Area_id;
            cmbTipoAdquisicion.SelectedItem = Inv.TipoAdquision;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtColor.Text == "" || txtDescripcion.Text == "" || txtNombreCorto.Text == "" || txtObservaciones.Text == "" || txtSerie.Text == "")
            {
                MessageBox.Show("Favor de llenar todos los campos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else{
                    Inv.NombreCorto = txtNombreCorto.Text;
                    Inv.Descripcion = txtDescripcion.Text;
                    Inv.Serie = txtSerie.Text;
                    Inv.Color = txtColor.Text;
                    Inv.FechaAdquision = dtpFechaAdquisicion.Value.ToString("yyyy-MM-dd");
                    Inv.TipoAdquision = cmbTipoAdquisicion.SelectedItem.ToString();
                    Inv.observaciones = txtObservaciones.Text;
                    Inv.Area_id = cmbArea.SelectedIndex + 1;
                if (dao.Actualizar(Inv))
                {
                    MessageBox.Show("Se ha actualizo correctamente", "EXITO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Hide();
                    main.Show();

                }
                else
                {
                    MessageBox.Show("Algo salio mal al actualizar, revisa tu conexion de internet.","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("¿Estas seguro de cancelar la modificacion?", "CANCELAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                this.Hide();
                main.Show();
            }
            else if (dialogResult == DialogResult.No)
            {}
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
            { }
        }
    }
}
