using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using carShopDllProject;

namespace WindowsFormsAppProject
{
    public partial class frmAggiungiVeicolo : Form
    {
        string color,veicolo;
        BindingList<Veicolo> lista;
        public frmAggiungiVeicolo()
        {
            InitializeComponent();
        }

        public frmAggiungiVeicolo(BindingList<Veicolo> bindListaVeicolo)
        {
            InitializeComponent();
            lista = bindListaVeicolo;
        }

        private void btnAnnulla_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAggiungiVeicolo_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (veicolo != null)
            {
                if (veicolo == "Auto")
                {
                    Auto a = new Auto(txtMarca.Text, txtModello.Text, color, Convert.ToInt32(nupCilindrata.Value), Convert.ToDouble(nupPotenza.Value), dtpDataImmatricolazione.Value, rdbNo.Checked ? false : true, cmbKm0.SelectedIndex == 0 ? true : false, Convert.ToDouble(nupPrezzo.Value) , Convert.ToInt32(nupChilometraggio.Value), Convert.ToInt32(nupAirbag.Value));
                    lista.Add(a);
                    clearForm();
                    caratteristicheVeicolo(cmbVeicolo.Text);
                }
                else
                {
                    Moto m = new Moto(txtMarca.Text, txtModello.Text, color, Convert.ToInt32(nupCilindrata.Value), Convert.ToDouble(nupPotenza.Value), dtpDataImmatricolazione.Value, rdbNo.Checked ? false : true, cmbKm0.SelectedIndex == 0 ? true : false, Convert.ToDouble(nupPrezzo.Value) ,Convert.ToInt32(nupChilometraggio.Value), txtMarcaSella.Text);
                    lista.Add(m);
                    clearForm();
                    caratteristicheVeicolo(cmbVeicolo.Text);
                }
            }
            else
            {
                if (pnlControlli.Visible == true)
                    MessageBox.Show("Compilare i campi.");
                else
                    errorProvider1.SetError(cmbVeicolo, "Selezionare un'opzione.");
            }
        }

        private void btnSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                color = colorDialog1.Color.Name.ToString();
        }

        private void CmbVeicolo_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlControlli.Visible = true;
            clearForm();
            caratteristicheVeicolo(cmbVeicolo.Text);
        }

        private void clearForm()
        {
            txtMarca.Text = "";
            txtModello.Text = "";
            nupCilindrata.Value = 0;
            nupChilometraggio.Value = 0;
            nupPotenza.Value = 0;
            rdbSi.Checked = false;
            rdbNo.Checked = false;
            nupAirbag.Value = 0;
            nupAirbag.Enabled = false;
            txtMarcaSella.Text = "";
            nupPrezzo.Value = 0;
            txtMarcaSella.Enabled = false;
            cmbKm0.SelectedIndex = -1;
            veicolo = "";
        }

        private void rdbNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbNo.Checked)
                cmbKm0.Enabled = true;
            else
                cmbKm0.Enabled = false;
        }

        private void rdbSi_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbSi.Checked)
                cmbKm0.Enabled = false;
            else
                cmbKm0.Enabled = true;
        }

        private void caratteristicheVeicolo(string type)
        {
            switch (type)
            {
                case "Auto":
                    nupAirbag.Enabled = true;
                    veicolo = "Auto";
                    break;
                case "Moto":
                    txtMarcaSella.Enabled = true;
                    veicolo = "Moto";
                    break;
            }
        }
    }
}
