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
    public partial class MainBD : Form
    {
        public static List<Mod_Inventario> InvProduc;
        private Mod_Inventario InventData;
        public MainBD()
        {
            if (ConexionBD.Conectar() == false)
            {
                MessageBox.Show("Error de conexion, ¿deseas reintentar?");
            }
            else
            {
                InitializeComponent();
                ObtenerDatos();
                dgvInventario.DataSource = InvProduc;
                dgvInventario.Columns["IdInventario"].Visible = false;
                dgvInventario.Columns["Area_id"].Visible = false;
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAgregar(object sender, EventArgs e)
        {
            Agregar_Inventario inventario = new Agregar_Inventario();
            inventario.Show();
            this.Hide();
        }

        public void ObtenerDatos()
        {
            ConexionBD con = new ConexionBD();
            InvProduc = new DAO_Inventario().GetAll();
            InvProduc = InvProduc.OrderBy(Ip => Ip.NombreCorto).ToList();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try {
                DialogResult dialogResult = MessageBox.Show("¿Estas seguro de eliminar?", "ELIMINAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    int IDInv = Convert.ToInt32(dgvInventario.SelectedRows[0].Cells[0].Value.ToString());
                    new DAO_Inventario().Borrar(IDInv);
                    ObtenerDatos();
                    dgvInventario.DataSource = InvProduc;
                    MessageBox.Show("Eliminacion Completada", "ELIMINAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Eliminacion Cancelada", "ELIMINAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Por favor selecciona toda la fila.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MainBD_Load(object sender, EventArgs e)
        {
            ObtenerDatos();
            dgvInventario.DataSource = InvProduc;
        }

        private void btnMod(object sender, EventArgs e)
        {
            try {
                int IDInv = Convert.ToInt32(dgvInventario.SelectedRows[0].Cells[0].Value.ToString());
                DAO_Inventario dao = new DAO_Inventario();
                InventData = dao.BusquedaMod(IDInv);
                ModInv mod = new ModInv(InventData);
                mod.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Por favor selecciona toda la fila.","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
